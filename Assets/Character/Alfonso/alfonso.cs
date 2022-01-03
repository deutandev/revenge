using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class alfonso : MonoBehaviour
{
	private Rigidbody playerRigidbody;
	private Transform playerTransform;
	
	[Header("Ragdoll Object")]
	public GameObject RagdollVersion;
	
	[Header("Camera Settings")]
	public FollowTarget camera;
	
	[Header("Movement Button")]
    public PlayerButton LeftButton;
    public PlayerButton RightButton;
    
    
    [Header("Level Manager")]
    public LevelManager level;
	
	[Header("Hit Box Settings")]
	public Transform hitBox;
	public LayerMask whatIsEnemy;
	public float attackRange;
	
	[Header("Landing Area Settings")]
	public Transform groundCheck;
	public LayerMask whatIsGround;
	public float landRadius;
	Vector3 movement;
	
	private float attackRate = 2f, nextAttackTime = 0f;
	private int jumpCount, attackCount;
	private bool move = true, isGrounded = true;
	
	private Animator anim;
	
	[Header("Visual Effect")]
	public Animator hitEffectAnimator;
	private ParticleSystem healEffect;
	
	[Header("UI Coin & Health Bar")]
	public Text coinUI;
	public Slider healthBar;
	
	[Header("Alfonso Velocity")]
	public float velocity = 5f;
	private float health = 100;
	private int coin = 0;
	
	[Header("Sound Effect")]
	public AudioClip[] swingSound = new AudioClip[2];
	public AudioClip[] healSound = new AudioClip[3];
	public AudioClip coinSound, diamondSound;
	
	private AudioSource audio;
	
    // Start is called before the first frame update
    void Start()
    {
		jumpCount = 0;
		attackCount = 0;
		
        playerRigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        playerTransform = GetComponent<Transform>();
        
        audio = GetComponent<AudioSource>();
        audio.playOnAwake = false;
        
        healEffect = transform.Find("Heal Effect").gameObject.GetComponent<ParticleSystem>();
        
		healthBar.value = health;
		
		anim.GetBehaviour<alfonsoAttack>().attack = false;
    }

    // Update is called once per frame
    void Update()
    {	
		if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2 && move == true)
		{
			jumpCount++;
			if(jumpCount <= 2) playerRigidbody.velocity = Vector2.up * 18f;
			if(jumpCount == 2) anim.SetTrigger("jump2");
		}
		
		if (Input.GetKeyDown(KeyCode.G))
		{
			AnimateAttack();
		}
    }
    
    private void FixedUpdate()
    {
		isGrounded = Physics.CheckSphere(groundCheck.position, landRadius, whatIsGround);
		if(isGrounded == true)
		{
			jumpCount = 0;
			anim.SetBool("isJump", false);
		} 
		else 
		{
			if(jumpCount == 0) jumpCount = 1;
			anim.SetBool("isJump", true);
		}
		
		
		if(anim.GetBehaviour<alfonsoAttack>().attack == true)
		{
			anim.GetBehaviour<alfonsoAttack>().attack = false;
			Attack();
			audio.PlayOneShot(swingSound[0], 0.4f);
		}
		
		if(anim.GetBehaviour<alfonsoAttack4>().attack == true)
		{
			anim.GetBehaviour<alfonsoAttack4>().attack = false;
			Attack();
			audio.PlayOneShot(swingSound[0], 0.4f);
		}
		
		if(anim.GetBehaviour<alfonsoAttack2>().attack == true)
		{
			anim.GetBehaviour<alfonsoAttack2>().attack = false;
			audio.PlayOneShot(swingSound[1], 0.4f);
			Invoke("Attack", 0.15f);
		}
		
		if(move == true) 
		{
			//Nilai Input Horizontal (-1,0,1)
			float h = Input.GetAxisRaw("Horizontal");
			if( RightButton.IsPressed ) h = 1 ;
			if( LeftButton.IsPressed )  h = -1 ;
			
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
    
    public void Jump()
    {
		if (jumpCount < 2 && move == true)
		{
			jumpCount++;
			if(jumpCount <= 2) playerRigidbody.velocity = Vector2.up * 18f;
			if(jumpCount == 2) anim.SetTrigger("jump2");
		}
	}
	
	public void AnimateAttack()
	{
		if (Time.time >= nextAttackTime)
		{
			if(isGrounded == false) anim.SetTrigger("attack3");
			else 
			{
				move = false;
				attackCount++;
				anim.SetBool("isRun", false);
				if(attackCount == 1) anim.SetTrigger("attack");
				else if(attackCount == 2)
				{
					anim.SetTrigger("attack2");
					attackCount = 0;
				}
						
				nextAttackTime = Time.time + 1f / attackRate;
				StartCoroutine(DontMove(0.5f));
			}	
		}
		else attackCount = 0;
	}
	
	private void Attack()
	{
		Collider[] hitEnemies = Physics.OverlapSphere(hitBox.position, attackRange, whatIsEnemy);
		foreach(Collider enemy in hitEnemies)
		{
			enemy.GetComponent<Enemy>().TakeDamage(15f);
		}
	}
    
    void OnCollisionEnter(Collision collision)
    {   
        if (collision.gameObject.tag == "Spider" || collision.gameObject.tag == "Bat")
        {	
			float collisionPosX = collision.gameObject.transform.position.x;
			if (playerTransform.position.x > collisionPosX)
			{
				playerRigidbody.AddForce(600f, 1000f, 0);	
			}
			else
			{
				playerRigidbody.AddForce(-600f, 1000f, 0);	
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
			audio.PlayOneShot(coinSound, 0.4f);
			coin++;
			coinUI.text = coin.ToString();
			Destroy(collision.gameObject);
		}
		
		if (collision.gameObject.tag == "Diamond")
		{
			audio.PlayOneShot(diamondSound, 0.4f);
			coin += 10;
			coinUI.text = coin.ToString();
			Destroy(collision.gameObject);
		}
		
		if (collision.gameObject.tag == "HealthPotion")
		{
			healEffect.Play();
			
			HealthPotion healPoint = collision.gameObject.GetComponent<HealthPotion>();
			
			if(healPoint.health == 15) audio.PlayOneShot(healSound[0], 0.6f);
			else if(healPoint.health == 25) audio.PlayOneShot(healSound[1], 0.6f);
			else if(healPoint.health == 40) audio.PlayOneShot(healSound[2], 0.6f);
			
			health += healPoint.health;
			if(health > 100) health = 100;
			healthBar.value = health;
			Destroy(collision.gameObject);
		}
		
		if (collision.gameObject.tag == "Zoom")
		{
			ZoomArea zoomArea = collision.gameObject.GetComponent<ZoomArea>();
			if(zoomArea.changeTarget == true) camera.target = zoomArea.zoomTarget;
			camera.inZoomArea = true;
			camera.zoomInFOV = zoomArea.cameraFOV;
		}
		
		if (collision.gameObject.tag == "Finish")
		{
			level.isComplete = true;
		}
	}
	
	void OnTriggerExit(Collider collision)
    {	
		if (collision.gameObject.tag == "Zoom")
		{
			camera.target = gameObject;
			camera.inZoomArea = false;
		}
	}
	
	IEnumerator TakeDamage(float damage)
	{
		health -= damage;
		healthBar.value = health;
		
		yield return new WaitForSeconds(0);
		
		if(health <= 0)
		{
			GameObject deathBody = Instantiate(RagdollVersion, transform.position, transform.rotation);
			camera.target = deathBody.GetComponent<alfonsoRagdoll>().head;
			level.isGameover = true;
			Destroy(gameObject);	
		}
		else hitEffectAnimator.SetTrigger("hit"); 
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
