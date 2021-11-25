using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
	public float maxXWorldRange, minXWorldRange;
	private float yTargetPosition;
	public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        yTargetPosition = target.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
		float x = target.transform.position.x;
		float y = transform.position.y;
		//float y = transform.position.y + (target.transform.position.y - yTargetPosition);
		float z = transform.position.z;
		
		/*
		if(target.transform.position.y - yTargetPosition > 0) y += 0.4f;
		else if(target.transform.position.y - yTargetPosition < 0) y-= 0.4f; */
		
        if ( target.transform.position.x > minXWorldRange &&
			target.transform.position.x < maxXWorldRange)
		{
			transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, z), 0.1f);
		}
		
		/*
		if(target.transform.position.y > -4.6f) 
		{
			transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, y, z), 0.1f);
		}
		
		yTargetPosition = target.transform.position.y; */
    }
}
