using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBtn : MonoBehaviour
{
	private GameObject off;
	private string sound;
	public AudioManager.SoundType soundType;
	
    // Start is called before the first frame update
    void Start()
    {
		sound = soundType.ToString();
		off = transform.Find("off").gameObject;
		
        if(PlayerPrefs.GetInt(sound) == 1) off.SetActive(true);
		else if(PlayerPrefs.GetInt(sound) == 0) off.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt(sound) == 1) off.SetActive(true);
		else if(PlayerPrefs.GetInt(sound) == 0) off.SetActive(false);
    }
}
