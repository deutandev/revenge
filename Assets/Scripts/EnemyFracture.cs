using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFracture : MonoBehaviour
{
	public AudioClip deathSound;
	private AudioSource audio;
	
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroy", 7f);
        
        audio = GetComponent<AudioSource>();
		
		if (PlayerPrefs.GetInt("sfx") == 0) audio.mute = true;
		audio.PlayOneShot(deathSound, 0.6f);
    }
    
    private void Destroy() {Destroy(gameObject);}
}
