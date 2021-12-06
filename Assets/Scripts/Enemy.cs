using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public enum EnemyState {Idle, Damaged, Dead};
	private EnemyState state;
	public EnemyState State {get {return state;}}
	
	public float health = 30;
	public GameObject DestroyedVersion;
	
	void Start() 
	{
		SetIdle();
	}
    
    public void TakeDamage(float damage)
	{
		Invoke("SetDamaged", 0.1f);
		health = health - damage;
		Invoke("SetIdle", 0.2f);
		if (health <= 0)
		{
			state = EnemyState.Dead;
			Instantiate(DestroyedVersion, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
	
	private void SetDamaged() {state = EnemyState.Damaged;}
	private void SetIdle() {state = EnemyState.Idle;}
}
