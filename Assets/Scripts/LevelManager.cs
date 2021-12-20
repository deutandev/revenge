using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class LevelManager : MonoBehaviour
{
	public PostProcessVolume postProcess;
	public Text coinUI;
	public Slider healthBar;
	public RectTransform[] UICanvas;
	public Transform spiders, bats;
	
	private int totalEnemy, currentEnemy;
	
	[HideInInspector]
	public bool isComplete = false, isGameover = false;
	public GameObject gameOverPanel, completePanel;
	
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
        if(isComplete == true)
        {
			star = 1;
			if(confirmStar(twoStar) == true)
			{
				star = 2;
				if(confirmStar(threeStar) == true) star = 3;
			}
			Debug.Log(star);
			completePanel.SetActive(true);
		}
        else if(isGameover == true) gameOverPanel.SetActive(true);
    }
    
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
