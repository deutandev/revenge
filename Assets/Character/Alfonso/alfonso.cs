using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alfonso : MonoBehaviour
{
	private Rigidbody playerRigidbody;
	private Animator anim;
	public float velocity = 5f;
	Vector3 movement;
	private Transform playerTransform;
	private bool isJump = false;
	
    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        playerTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
			{
				anim.SetTrigger("attack");
			}
		if (Input.GetKeyDown(KeyCode.Space) && isJump == false)
		{
			anim.SetBool("isJump", true);
			isJump = true;
			playerRigidbody.AddForce(Vector2.up * 1000f);
		}
    }
    
    private void FixedUpdate()
    {
        //Nilai Input Horizontal (-1,0,1)
        float h = Input.GetAxisRaw("Horizontal");
        if(h == 1) playerTransform.rotation = Quaternion.Euler(0, 180, 0);
        else if (h == -1) playerTransform.rotation = Quaternion.Euler(0, 0, 0);
        Debug.Log(h);
        Move(h);
        Animating(h);
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
    
    private void OnCollisionEnter(Collision collision)
	{	
		if (collision.transform.tag.Equals("Platform"))
		{
			isJump = false;
			anim.SetBool("isJump", false);
		}
	}
}
