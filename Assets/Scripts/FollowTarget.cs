using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
	public GameObject target;
	[HideInInspector] public bool inZoomArea = false;
	[HideInInspector] public float zoomInFOV, zoomInOffset;
	public float maxWorldRange, minWorldRange;
	private float yTargetPosition;
	private float defaultFOV, currentFOV;
	public Vector3 offset;
	[Range(1, 10)]
	public float smoothFactor;
	public float smoothZoom;
    // Start is called before the first frame update
    private void Start()
    {
        yTargetPosition = target.transform.position.y;
        defaultFOV = Camera.main.fieldOfView;
        currentFOV = defaultFOV;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {	
		Vector3 targetPosition = target.transform.position + offset;
		
		if(target.transform.position.x <= minWorldRange) targetPosition.x = transform.position.x;
		
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor*Time.fixedDeltaTime);
		transform.position = smoothedPosition;
		
		if(inZoomArea == true) zoomOut();
		else if (inZoomArea == false) zoomIn();
    }
    
    public void zoomOut()
    {
		if(currentFOV < zoomInFOV)
		{
			currentFOV += ((zoomInFOV * 0.3f) * Time.deltaTime);
			Camera.main.fieldOfView = currentFOV;
		}
	}
	
	public void zoomIn()
	{
		if(currentFOV > defaultFOV)
		{
			currentFOV -= (smoothZoom * Time.deltaTime);
			Camera.main.fieldOfView = currentFOV;
		}
	}
}
