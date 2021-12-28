using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class LevelManager : MonoBehaviour
{
	public Animator vignette, blur;
	public Text coinUI;
	public Slider healthBar;
	public RectTransform[] UICanvas;
	public Transform spiders, bats;
	
	private int totalEnemy, currentEnemy;
	
	[HideInInspector]
	public bool isComplete = false, isGameover = false, end = false;
	public GameObject gameOverPanel, completePanel;
	public GameObject[] MarkImage = new GameObject[3];
	public GameObject[] MarkText = new GameObject[3];
	
	private int star = 0;
	
	public enum CriterionTypes {enemy, health, coin, time};
	[System.Serializable]
	public struct Criterion
	{
		public CriterionTypes criterionTypes;
		public int criterionMinValue;
	}
	[SerializeField]
	public Criterion[] twoStar, threeStar;
	
    // Start is called before the first frame update
    private void Start()
    {
		totalEnemy = spiders.childCount + bats.childCount;
        isComplete = false;
        isGameover = false;
    }

    // Update is called once per frame
    private void Update()
    {
		currentEnemy = spiders.childCount + bats.childCount;
        if(isComplete == true && end == false)
        {
			end = true;
			StartCoroutine("LevelComplete");
		}
        else if(isGameover == true)
        {
			StartCoroutine("GameOver");
		}
    }
    
    IEnumerator GameOver()
    {
		vignette.SetTrigger("gameOver");
		foreach (RectTransform ui in UICanvas)
		{
			ui.localScale = new Vector3(0, 0, 0);
		}
		gameOverPanel.transform.localScale = new Vector3(1, 1, 1);
		
		yield return new WaitForSeconds(2f);
		
		Animator anim = gameOverPanel.GetComponent<Animator>();
		anim.SetTrigger("open");
	}
	
	IEnumerator LevelComplete()
	{
		foreach (RectTransform ui in UICanvas)
		{
			ui.localScale = new Vector3(0, 0, 0);
		}
		star = 1;
		if(confirmStar(twoStar) == true) star++;
		if(confirmStar(threeStar) == true) star++;
		
		for(int i=0; i<3; i++)
		{
			if(i < star)
			{
				GameObject completedImg = MarkImage[i].transform.Find("Completed Image").gameObject;
				completedImg.SetActive(true);
				
				GameObject completedTxt = MarkText[i].transform.Find("Completed Text").gameObject;
				completedTxt.SetActive(true);
			}
			else
			{
				GameObject failedImg = MarkImage[i].transform.Find("Failed Image").gameObject;
				failedImg.SetActive(true);
				
				GameObject failedTxt = MarkText[i].transform.Find("Failed Text").gameObject;
				failedTxt.SetActive(true);
			}
		}
			
		completePanel.transform.localScale = new Vector3(1, 1, 1);
			
		yield return new WaitForSeconds(0.1f);
			
		Animator anim = completePanel.GetComponent<Animator>();
		anim.SetTrigger("open");
	}
	
	IEnumerator GameResult()
	{
		Animator anim = completePanel.GetComponent<Animator>();
		anim.SetTrigger("close");
		
		yield return new WaitForSeconds(0.4f);
		
		blur.SetTrigger("blurIn");
		
		yield return new WaitForSeconds(0.1f);
		
		anim.SetTrigger("openResult");
		
		yield return new WaitForSeconds(1.2f);
		
		anim.SetInteger("star", star);
	}
	
	public void openPanel(GameObject panel) {panel.transform.localScale = new Vector3(1, 1, 1);}
	
	public void closePanel(GameObject panel) {panel.transform.localScale = new Vector3(0, 0, 0);}
	
	public void showGameResult() {StartCoroutine("GameResult");}
    
    private bool confirmStar(Criterion[] criterion)
    {
		bool result;
		int temp = 0;
		for(int i=0; i<criterion.Length; i++)
		{
			float minValue = criterion[i].criterionMinValue;
			switch (criterion[i].criterionTypes)
			{
				case CriterionTypes.enemy:
					int enemySlain = totalEnemy - currentEnemy;
					if(minValue <= enemySlain) temp++;
					break;
				case CriterionTypes.health:
					int currentHealth = (int) Math.Round(healthBar.value);
					if(minValue <= currentHealth) temp++;
					break;
				case CriterionTypes.coin:
					int currentCoin = int.Parse(coinUI.text);
					if(minValue <= currentCoin) temp++;
					break;
				//case CriterionTypes.time:
					//if(minValue <= currentTime) temp++;
					//break;
				default:
					break;
			}
		}
		
		if(temp == criterion.Length) result = true;
		else result = false;
		
		return result;
	}
}
