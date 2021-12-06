using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleDown : MonoBehaviour
{
	private bool start = false;
	private Vector3 scaler;
	
    // Start is called before the first frame update
    void Start()
    {	
		scaler = transform.localScale * 0.002f;
        Invoke("Scale", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(start == true)
        {
			if(transform.localScale.x <= 0.1) Destroy(gameObject);
			transform.localScale -= scaler;
		}
    }
    
    private void Scale() {start = true;}
}
