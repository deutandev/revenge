using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spider : MonoBehaviour
{
	string direction;
	public float speed = 120;
	private Rigidbody enemyRigidbody;
	
    // Start is called before the first frame update
    void Start()
    {
		direction = "right";
        enemyRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
	}
	
	void FixedUpdate()
    {
		if (direction == "right")
		{
			enemyRigidbody.velocity = new Vector3(Time.deltaTime * speed, enemyRigidbody.velocity.y, 0);
			transform.rotation = Quaternion.Euler(0, 270, 0);
		}
		else if (direction == "left")
		{
			enemyRigidbody.velocity = new Vector3(Time.deltaTime * speed * -1, enemyRigidbody.velocity.y, 0);
			transform.rotation = Quaternion.Euler(0, 90, 0);
		}
    }

	private void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.name == "SpiderBorderRight") direction = "left";
		else if (collider.gameObject.name == "SpiderBorderLeft") direction = "right";
	}
}
