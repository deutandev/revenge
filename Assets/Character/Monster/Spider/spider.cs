using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spider : Enemy
{
	public Material[] defaultMaterial;
	public Material hitEffect;
	public ParticleSystem slashEffect;
	private Material[] hitMaterial = new Material[6];
	public SkinnedMeshRenderer model;
	string direction;
	public float speed = 120;
	private Rigidbody enemyRigidbody;
	private float startPos;
	public float widthArea = 3;
	
    // Start is called before the first frame update
    void Start()
    {
		direction = "right";
        enemyRigidbody = GetComponent<Rigidbody>();
        startPos = enemyRigidbody.position.x;
        
        for(int i=0; i<6; i++) hitMaterial[i] = hitEffect;
    }

    // Update is called once per frame
    void Update()
    {
		if(State == EnemyState.Damaged) 
		{
			model.materials = hitMaterial;
			slashEffect.Play();
		}
		else if (State == EnemyState.Idle) model.materials = defaultMaterial;
		
        if (enemyRigidbody.position.x >= startPos + widthArea)
		{
			direction = "left";
			transform.rotation = Quaternion.Euler(0, 270, 0);
		}
		else if (enemyRigidbody.position.x <= startPos - widthArea)
		{
			direction = "right";
			
			transform.rotation = Quaternion.Euler(0, 90, 0);
		}
	}
	
	void FixedUpdate()
    {
		if (direction == "right") 
		{
			enemyRigidbody.velocity = new Vector3(Time.deltaTime * speed, enemyRigidbody.velocity.y, 0);
		}
		else if (direction == "left")
		{
			enemyRigidbody.velocity = new Vector3(Time.deltaTime * speed * -1, enemyRigidbody.velocity.y, 0);
		}
    }
}
