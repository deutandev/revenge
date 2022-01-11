﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantShooter : Enemy
{
	private Material[] defaultMaterial;
	public Material hitEffect;
	public GameObject projectile;
	private Material[] hitMaterial = new Material[5];
	private SkinnedMeshRenderer model;
	
	private ParticleSystem slashEffect;
	private GameObject projectileSpawner;
	private Animator anim;
	private Vector3 defaultPos;
	
    // Start is called before the first frame update
    void Start()
    {
		defaultPos = transform.position;
		anim = GetComponent<Animator>();
		
        model = transform.Find("KEPALA").gameObject.GetComponent<SkinnedMeshRenderer>();
        defaultMaterial = model.materials;
        
        slashEffect = transform.Find("Hit Effect").gameObject.GetComponent<ParticleSystem>();
        
        projectileSpawner = transform.Find("Projectile Spawner").gameObject;
        
        for(int i=0; i<5; i++) hitMaterial[i] = hitEffect;
        
        float x = Random.Range(1f, 3f);
		StartCoroutine(shoot(x));
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
    }
    
    IEnumerator shoot(float delayTime)
    {
		anim.SetTrigger("shoot");
		yield return new WaitForSeconds(0.8f);
		
		GameObject y = Instantiate(projectile, projectileSpawner.transform.position, transform.rotation);
		//transform.position = defaultPos;
		
		yield return new WaitForSeconds(delayTime);
		
		float x = Random.Range(1f, 6f);
		StartCoroutine(shoot(x));
	}
}
