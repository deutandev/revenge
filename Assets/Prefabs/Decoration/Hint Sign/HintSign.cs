using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HintSign : MonoBehaviour
{
	public RectTransform hintMsg;
	private Animator UIAnim;
	public TextMeshProUGUI txt;
	private string story;
	private bool opened = false;
	
    // Start is called before the first frame update
    void Start()
    {
        UIAnim = hintMsg.GetComponent<Animator>();
        story = txt.text;
    }

    void OnTriggerEnter(Collider collision)
    {
		hintMsg.localScale = new Vector3(1,1,1);
		if (collision.gameObject.tag == "Player" && opened == false)
		{
			opened = true;
			StartCoroutine("showHintMsg");
		}
	}
	
	void OnTriggerExit(Collider collision)
    {	
		if (collision.gameObject.tag == "Player")
		{
			StartCoroutine(hideHintMsg());
		}
	}
	
	IEnumerator showHintMsg()
	{
		txt.text = "";
		UIAnim.Play("showHint", 0, 0.0f);
		yield return new WaitForSeconds (0.8f);
		StartCoroutine(PlayText(0.01f));
	}
	
	IEnumerator hideHintMsg()
	{
		yield return new WaitForSeconds (5f);
		opened = false;
		UIAnim.Play("hideHint", 0, 0.0f);
	}
	
	IEnumerator PlayText(float duration)
	{
		txt.text = "";
		foreach (char c in story) 
		{
			txt.text += c;
			yield return new WaitForSeconds (duration);
		}
	}

}
