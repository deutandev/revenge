using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class alfonso : MonoBehaviour
{
	private Rigidbody playerRigidbody;
	private Transform playerTransform;
	
	public GameObject RagdollVersion;
	
	public Transform groundCheck, hitBox;
	public LayerMask whatIsGround, whatIsEnemy;
	public float landRadius, attackRange;
	Vector3 movement;
	
	//private float attackRate = 2f, nextAttackTime = 0f;
	private int jumpCount, attackCount;
	private bool move = true;
	
	private Animator anim;
	public Animator hitEffectAnimator;
	
	public Text coinUI;
	public Slider healthBar;
	
	public float velocity = 5f;
	private float health = 100;
	private int coin = 0;
	
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
			//anim.SetTrigger("attack2");
			
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
			if(jumpCount == 1) playerRigidbody.velocity = Vector2.up * 18f;
			else if (jumpCount == 2) playerRigidbody.velocity = Vector2.up * 14f;
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
        if (collision.gameObject.tag == "Spider" || collision.gameObject.tag == "Bat")
        {	
			float collisionPosX = collision.gameObject.transform.position.x;
			if (playerTransform.position.x > collisionPosX)
			{
				playerRigidbody.AddForce(600f, 1600f, 0);	
			}
			else
			{
				playerRigidbody.AddForce(-600f, 1600f, 0);	
			}
			
			StartCoroutine(TakeDamage(20f));
			
			StartCoroutine(DontMove(1f));
		}
		
		if (collision.gameObject.tag == "Spike")
		{
			int falDir = 0;
			if(playerTransform.rotation.y == 1) falDir = 1;
			else falDir = -1;
			
			playerRigidbody.AddForce(800f * falDir, 2000f, 0);
			
			StartCoroutine(TakeDamage(20f));
			
			StartCoroutine(DontMove(1f));
		}
    }
    
    void OnTriggerEnter(Collider collision)
    {
		if (collision.gameObject.tag == "Coin")
		{
			coin++;
			coinUI.text = coin.ToString();
			Destroy(collision.gameObject);
		}
		
		if (collision.gameObject.tag == "Diamond")
		{
			coin += 10;
			coinUI.text = coin.ToString();
			Destroy(collision.gameObject);
		}
	}
	
	IEnumerator TakeDamage(float damage)
	{
		hitEffectAnimator.SetTrigger("hit"); 
		health -= damage;
		healthBar.value = health;
		
		yield return new WaitForSeconds(0);
		
		if(health <= 0)
		{
			Instantiate(RagdollVersion, transform.position, transform.rotation);
			Destroy(gameObject);	
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
