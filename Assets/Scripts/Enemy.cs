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
	
	public AudioClip[] hitSound = new AudioClip[2];
	
	private AudioSource audio;
	
	private void Start() 
	{
		SetIdle();
	}
    
    public void TakeDamage(float damage)
	{
		int x = Random.Range(0, 2);
		int y = PlayerPrefs.GetInt("music");
		
		audio = GetComponent<AudioSource>();
		if(PlayerPrefs.GetInt("sfx") == 0) audio.mute = true;
		else if(PlayerPrefs.GetInt("sfx") == 1) audio.mute = false;
		audio.PlayOneShot(hitSound[x], 0.4f);	
			
		Invoke("SetDamaged", 0.1f);
		health = health - damage;
		Invoke("SetIdle", 0.2f);
		if (health <= 0 && state != EnemyState.Dead)
		{
			state = EnemyState.Dead;
			Instantiate(DestroyedVersion, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
	
	private void SetDamaged() {state = EnemyState.Damaged;}
	private void SetIdle() {state = EnemyState.Idle;}
}
