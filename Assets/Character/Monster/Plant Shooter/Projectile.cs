using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public float speed = 20f, lifetime = 5f;
	private Rigidbody rb;
	private AudioSource audio;
	public AudioClip bounceSound;
	
    // Start is called before the first frame update
    void Start()
    {
		audio = GetComponent<AudioSource>();
		rb = GetComponent<Rigidbody>();
        Invoke("DestroyBullet", lifetime);
    }

    // Update is called once per frame
    void Update()
    {
		if(transform.rotation == Quaternion.Euler(0, 90, 0))
		{
			rb.velocity = new Vector3(28f, 0, 0);
			//transform.Translate(transform.right * speed * Time.deltaTime * -1);
		}
		else
		{
			rb.velocity = new Vector3(-28f, 0, 0);
			//transform.Translate(transform.right * speed * Time.deltaTime);
		}
    }
    
    public void BounceProjectile()
    {
		if (PlayerPrefs.GetInt("sfx") == 0) audio.mute = true;
		else if (PlayerPrefs.GetInt("sfx") == 1) audio.mute = false;
		
		audio.PlayOneShot(bounceSound, 0.5f);
		
		if (transform.rotation == Quaternion.Euler(0, 90, 0))
		{
			transform.rotation = Quaternion.Euler(0, 270, 0);
		}
		else
		{
			transform.rotation = Quaternion.Euler(0, 90, 0);
		}
	}
	
	void OnCollisionEnter(Collision collision)
    {   
        if (collision.gameObject.tag == "Platform" || collision.gameObject.tag == "Projectile")
        {
			Destroy(gameObject);
		}
	}
	
	void DestroyBullet()
	{
		Destroy(gameObject);
	}
}
