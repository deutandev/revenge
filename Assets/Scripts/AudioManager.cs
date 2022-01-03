using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public AudioSource[] musicAudio, sfxAudio;
	public enum SoundType {music, sfx};
	public enum Volume {on, off};
	
	private string  music, sfx;
	
    // Start is called before the first frame update
    void Start()
    {
		music = SoundType.music.ToString();
		sfx = SoundType.sfx.ToString();
		
        if (PlayerPrefs.HasKey(music) == false)  PlayerPrefs.SetInt(music, 1);
        if (PlayerPrefs.HasKey(sfx) == false)  PlayerPrefs.SetInt(sfx, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt(music) == 1)
        {
			foreach (AudioSource audio in musicAudio)
			{
				if(audio != null) audio.mute = true;
			}
		}
		else if(PlayerPrefs.GetInt(music) == 0)
        {
			foreach (AudioSource audio in musicAudio)
			{
				if(audio != null) audio.mute = false;
			}
		}
		
		if(PlayerPrefs.GetInt(sfx) == 1)
        {
			foreach (AudioSource audio in sfxAudio)
			{
				if(audio != null) audio.mute = true;
			}
		}
		else if(PlayerPrefs.GetInt(sfx) == 0)
        {
			foreach (AudioSource audio in sfxAudio)
			{
				if(audio != null) audio.mute = false;
			}
		}
    }
    
    public void ToggleMusicVolume() 
    {
		string music = SoundType.music.ToString();
		if(PlayerPrefs.GetInt(music) == 0) PlayerPrefs.SetInt(music, 1);
		else if(PlayerPrefs.GetInt(music) == 1) PlayerPrefs.SetInt(music, 0);
	}
	
	public void ToggleSfxVolume() 
    {
		string sfx = SoundType.sfx.ToString();
		if(PlayerPrefs.GetInt(sfx) == 0) PlayerPrefs.SetInt(sfx, 1);
		else if(PlayerPrefs.GetInt(sfx) == 1) PlayerPrefs.SetInt(sfx, 0);
	}
}
