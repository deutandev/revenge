using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cutscene : MonoBehaviour
{
	[System.Serializable]
	public struct Plot
	{
		public Sprite avatar;
		public string name;
		[TextArea(5, 10)]
		public string dialogue;
	}
	
	public Plot[] plot;
	
	public Text dialogueText, charaName;
	public Image avatar;
	
	private int index = 0;
	
	[HideInInspector]
	public bool end = false; 
	
    // Start is called before the first frame update
    void Start()
    {
        string x = plot[index].dialogue;
		StartCoroutine(ShowText(x));
    }

    // Update is called once per frame
    public void ShowPlot() 
    {
		index++;
		if (index == plot.Length) end = true;
		else if(end == false)
		{
			Debug.Log(index);
			string x = plot[index].dialogue;
			StartCoroutine(ShowText(x));
			
			if(plot[index].name != "0")
			{
				charaName.text = plot[index].name;
				avatar.sprite = plot[index].avatar;	
			}
		
			if(index == plot.Length) end = true;
		}
	}
    
    IEnumerator ShowText(string text)
	{
		dialogueText.text = "";
		foreach (char c in text) 
		{
			dialogueText.text += c;
			yield return new WaitForSeconds (0.01f);
		}
	}
}
