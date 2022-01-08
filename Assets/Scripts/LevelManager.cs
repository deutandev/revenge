using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class LevelManager : MonoBehaviour
{
	[Header("Sound")]
	public AudioSource bgMusic;
	public AudioSource soundEffect;
	
	public AudioClip gameoverMusic, completeMusic;
	
	public AudioClip transitionSound;
	public AudioClip[] failStarSound = new AudioClip[2];
	public AudioClip[] starSound = new AudioClip[3];
	
	[Header("Animator")]
	public Animator vignette, blur;
	
	[Header("Enemy List")]
	public Transform spiders, bats;
	
	private int totalEnemy, currentEnemy;
	
	[HideInInspector]
	public bool isComplete = false, isGameover = false, end = false;
	
	[Header("User Interface")]
	public Text coinUI;
	public Slider healthBar;
	public RectTransform[] UICanvas;
	
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
	
	[Header("Criterion Settings")]
	
	[SerializeField]
	public Criterion[] twoStar;
	
	[SerializeField]
	public Criterion[] threeStar;
	
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
        else if(isGameover == true && end == false)
        {
			end = true;
			StartCoroutine("GameOver");
		}
    }
    
    IEnumerator GameOver()
    {
		bgMusic.Stop();
		vignette.SetTrigger("gameOver");
		foreach (RectTransform ui in UICanvas)
		{
			ui.localScale = new Vector3(0, 0, 0);
		}
		gameOverPanel.transform.localScale = new Vector3(1, 1, 1);
		
		yield return new WaitForSeconds(0.5f);
		
		bgMusic.loop = false;
		bgMusic.volume = 0.8f;
		bgMusic.clip = gameoverMusic;
		bgMusic.Play();
		
		yield return new WaitForSeconds(1.5f);
		
		Animator anim = gameOverPanel.GetComponent<Animator>();
		anim.SetTrigger("open");
	}
	
	IEnumerator LevelComplete()
	{
		bgMusic.Stop();
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
		
		yield return new WaitForSeconds(1f);
		bgMusic.loop = false;
		bgMusic.clip = completeMusic;
		bgMusic.Play();
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
		
		if (star == 1)
		{
			soundEffect.PlayOneShot(starSound[0], 0.5f);
			yield return new WaitForSeconds(0.8f);
			soundEffect.PlayOneShot(failStarSound[0], 0.5f);
			yield return new WaitForSeconds(0.6f);
			soundEffect.PlayOneShot(failStarSound[1], 0.5f);
		}
		else if (star == 2)
		{
			soundEffect.PlayOneShot(starSound[0], 0.5f);
			yield return new WaitForSeconds(0.75f);
			soundEffect.PlayOneShot(starSound[1], 0.5f);
			yield return new WaitForSeconds(0.75f);
			soundEffect.PlayOneShot(failStarSound[1], 0.5f);
		}
		else if (star == 3)
		{
			soundEffect.PlayOneShot(starSound[0], 0.5f);
			yield return new WaitForSeconds(0.75f);
			soundEffect.PlayOneShot(starSound[1], 0.5f);
			yield return new WaitForSeconds(0.75f);
			soundEffect.PlayOneShot(starSound[2], 0.5f);
		}
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
