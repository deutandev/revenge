using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public AudioSource[] audioSource;
	public enum SoundType {music, sfx};
	public enum Volume {on, off};
	
    // Start is called before the first frame update
    void Start()
    {
		string music = SoundType.music.ToString();
		string sfx = SoundType.sfx.ToString();
		
        if (PlayerPrefs.HasKey(music) == false)  PlayerPrefs.SetInt(music, 1);
        if (PlayerPrefs.HasKey(sfx) == false)  PlayerPrefs.SetInt(sfx, 1);
    }

    // Update is called once per frame
    void Update()
    {
		/*
        if(PlayerPrefs.GetInt(sound) == 1)
        {
			foreach (AudioSource audio in audioSource)
			{
				audio.mute = true;
			}
		}
		else if(PlayerPrefs.GetInt(sound) == 1)
        {
			foreach (AudioSource audio in audioSource)
			{
				audio.mute = false;
			}
		} */
    }
    
    public void ToggleVolume(SoundType soundType) 
    {
		string sound = soundType.ToString();
		if(PlayerPrefs.GetInt(sound) == 0) PlayerPrefs.SetInt(sound, 1);
		else if(PlayerPrefs.GetInt(sound) == 1) PlayerPrefs.SetInt(sound, 0);
	}
}
