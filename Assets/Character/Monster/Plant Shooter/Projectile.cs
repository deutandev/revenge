using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public float speed = 20f, lifetime = 5f;
	private Rigidbody rb;
	
    // Start is called before the first frame update
    void Start()
    {
		rb = GetComponent<Rigidbody>();
        Invoke("DestroyBullet", lifetime);
        
    }

    // Update is called once per frame
    void Update()
    {
		if(transform.rotation == Quaternion.Euler(0, 90, 0))
		{
			transform.Translate(transform.right * speed * Time.deltaTime * -1);
		}
		else
		{
			transform.Translate(transform.right * speed * Time.deltaTime);
		}
    }
	
	void DestroyBullet()
	{
		Destroy(gameObject);
	}
}
