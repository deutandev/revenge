using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bat : MonoBehaviour
{
	private Rigidbody batRigidbody;
	private Transform batTransform;
	public float xAxis = 4, yAxis = 6, speed = 0.5f;
	private float timeCounter = 0;
	
    // Start is called before the first frame update
    void Start()
    {
		batRigidbody = GetComponent<Rigidbody>();
		batTransform = GetComponent<Transform>();   
    }

    // Update is called once per frame
    void Update()
    {
		timeCounter += Time.deltaTime * speed;
		
		float frequency = xAxis;
		float amplitude = yAxis;
		
		float x = Mathf.Cos(timeCounter * frequency) * amplitude;
		float y = Mathf.Sin(timeCounter * frequency) * amplitude;
		float z = batTransform.position.z;
		batTransform.position = new Vector3(x, y, z);
    }
    
    void fixedUpdate() 
    {
	}
}
