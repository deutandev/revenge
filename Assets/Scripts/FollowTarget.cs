using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
	public GameObject target;
	public float maxWorldRange, minWorldRange;
	private float yTargetPosition;
	public Vector3 offset;
	[Range(1, 10)]
	public float smoothFactor;
    // Start is called before the first frame update
    void Start()
    {
        yTargetPosition = target.transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		if(target == null) target = GameObject.Find("Alfonso Mcgreedy (ragdoll version)(Clone)");
		
		Vector3 targetPosition = target.transform.position + offset;
		
		if(target.transform.position.x <= minWorldRange) targetPosition.x = transform.position.x;
		
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor*Time.fixedDeltaTime);
		transform.position = smoothedPosition;
    }
}
