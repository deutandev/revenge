using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bat : Enemy
{
	public Material[] defaultMaterial;
	public Material hitEffect;
	public SkinnedMeshRenderer[] model;
	
	public ParticleSystem slashEffect;
	 
	private Rigidbody batRigidbody;
	private Transform batTransform;
	public float xAxis = 3, yAxis = 5f, speed = 0.5f;
	private float timeCounter = 0;
	private float xPos, yPos;
	
    // Start is called before the first frame update
    void Start()
    {
		batRigidbody = GetComponent<Rigidbody>();
		batTransform = GetComponent<Transform>();
		xPos = batRigidbody.position.x;
		yPos = batRigidbody.position.y;
    }

    // Update is called once per frame
    void Update()
    {
		if(State == EnemyState.Damaged)
		{
			for(int i=0; i<9; i++)
			{
				model[i].material = hitEffect;
			}
			slashEffect.Play();
		} 
		else if (State == EnemyState.Idle)
		{
			for(int i=0; i<9; i++)
			{
				model[i].material = defaultMaterial[i];
			}
		}
    }
    
    void FixedUpdate() 
    {
		timeCounter += Time.fixedDeltaTime * speed;
		
		float frequency = xAxis;
		float amplitude = yAxis;
		
		float x = xPos + (Mathf.Cos(timeCounter * frequency) * amplitude);
		float y = yPos + (Mathf.Sin(timeCounter * frequency) * amplitude);
		float z = batRigidbody.position.z;
		batRigidbody.MovePosition(new Vector3(x, y, z));
	}
}
