using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
	public Cutscene[] cutscene;
	public Animator fade;
	public Scene scene;
	private int index = 0;
	
    public void StartChangeCutscene() 
    {
		StartCoroutine(ChangeCutscene());
	}
    
    IEnumerator ChangeCutscene()
    {
		if(cutscene[cutscene.Length-1].end == true)
		{
			scene.loadLevel(1);
		}
		else if (cutscene[index].end == true)
		{
			index++;
			fade.SetTrigger("fadeIn");
			
			yield return new WaitForSeconds(0.5f);
				
			fade.SetTrigger("fadeOut");
			
			yield return new WaitForSeconds(0.1f);
			
			cutscene[index].gameObject.SetActive(true);
		}
	}
}
