using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
	public Cutscene[] cutscene;
	public Animator fade;
	private int index = 0;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void StartChangeCutscene() 
    {
		StartCoroutine(ChangeCutscene());
	}
    
    IEnumerator ChangeCutscene()
    {
		if (cutscene[index].end == true)
		{
			index++;
			fade.SetTrigger("fadeIn");
			
			yield return new WaitForSeconds(0.25f);
			
			cutscene[index].gameObject.SetActive(true);
			
			yield return new WaitForSeconds(0.2f);
			
			fade.SetTrigger("fadeOut");
		}
	}
}
