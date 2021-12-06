using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alfonsoRagdoll : MonoBehaviour
{
	public GameObject body;
	private Rigidbody rb;
	
    // Start is called before the first frame update
    void Start()
    {
		rb = body.GetComponent<Rigidbody>();
		
		int falDir = 0;
		if(transform.rotation.y == 1) falDir = -1;
		else falDir = 1;
			
		rb.AddForce(800f * falDir, 2000f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
