using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class alfonso : MonoBehaviour
{
	private Rigidbody playerRigidbody;
	private Animator anim;
	public Animator hitEffectAnimator;
	public Slider healthBar;
	public float velocity = 5f;
	public Transform groundCheck, hitBox;
	public LayerMask whatIsGround, whatIsEnemy;
	public float landRadius, attackRange;
	Vector3 movement;
	private Transform playerTransform;
	//private float attackRate = 2f, nextAttackTime = 0f;
	private int jumpCount, attackCount;
	private bool move = true;
	private float health = 100;
	
    // Start is called before the first frame update
    void Start()
    {
		jumpCount = 0;
		attackCount = 0;
        playerRigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        playerTransform = GetComponent<Transform>();
		healthBar.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
		{
			anim.SetTrigger("attack");
			anim.SetTrigger("attack2");
			
			Collider[] hitEnemies = Physics.OverlapSphere(hitBox.position, attackRange, whatIsEnemy);
			foreach(Collider enemy in hitEnemies)
			{
				enemy.GetComponent<Enemy>().TakeDamage(15f);
			}
			
			//nextAttackTime = Time.time + 1f / attackRate;
			StartCoroutine(DontMove(0.77f));
		}
		
		if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 1 && move == true)
		{
			jumpCount++;
			playerRigidbody.AddForce(Vector2.up * 1800f);
		}
    }
    
    private void FixedUpdate()
    {
		bool isGrounded = Physics.CheckSphere(groundCheck.position, landRadius, whatIsGround);
		if(isGrounded == true)
		{
			jumpCount = 0;
			anim.SetBool("isJump", false);
		} 
		else 
		{
			anim.SetBool("isJump", true);
		} 
		
		if(move == true) 
		{
			//Nilai Input Horizontal (-1,0,1)
			float h = Input.GetAxisRaw("Horizontal");
			if(h == 1) playerTransform.rotation = Quaternion.Euler(0, 180, 0);
			else if (h == -1) playerTransform.rotation = Quaternion.Euler(0, 0, 0);
			Move(h);
			Animating(h);	
		}
    }
    
    public void Move(float h)
    {
        //Set nilai vector 'movement' x dan y
        movement.Set(h, 0f, 0f);
        
        //Menormalisasi nilai vector agar total panjang dari vector adalah 1
        movement = movement.normalized * velocity * Time.deltaTime;
        
        //Move to position
        playerRigidbody.MovePosition(transform.position + movement);
    }
    
    public void Animating(float h)
    {
        bool walking = h != 0f;
        anim.SetBool("isRun", walking);
    }
    
    void OnCollisionEnter(Collision collision)
    {   
        if (collision.gameObject.tag == "Spider")
        {
			hitEffectAnimator.SetTrigger("hit"); 
			health -= 20;
			healthBar.value = health;
			float collisionPosX = collision.gameObject.transform.position.x;
			if (playerTransform.position.x > collisionPosX)
			{
				playerRigidbody.AddForce(600f, 1600f, 0);	
			}
			else
			{
				playerRigidbody.AddForce(-600f, 1600f, 0);	
			}
			StartCoroutine(DontMove(1f));
		}
    }
    
    IEnumerator DontMove(float duration)
	{
		move = false;
		yield return new WaitForSeconds(duration);
		move = true;
	}
    
    void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(groundCheck.position, landRadius);
		Gizmos.DrawWireSphere(hitBox.position, attackRange);
	}
}
