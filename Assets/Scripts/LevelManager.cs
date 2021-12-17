using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	[HideInInspector]
	public float enemySlain, currentCoin;
	[HideInInspector]
	public float currentHealth, currentTime;
	
	[HideInInspector]
	public bool isComplete = false, isGameover = false;
	public GameObject gameOverPanel, completePanel;
	
	private int star = 0;
	
	public enum CriterionTypes {enemy, health, coin, time};
	[System.Serializable]
	public struct Criterion
	{
		public CriterionTypes criterionTypes;
		public float criterionMinValue;
	}
	[SerializeField]
	public Criterion[] twoStar, threeStar;
	
    // Start is called before the first frame update
    private void Start()
    {
        isComplete = false;
        isGameover = false;
    }

    // Update is called once per frame
    private void Update()
    {
		
        if(isComplete == true)
        {
			star = 1;
			if(confirmStar(twoStar) == true) star = 2;
			if(confirmStar(threeStar) == true) star = 3;
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
					if(minValue <= enemySlain) temp++;
					break;
				case CriterionTypes.health:
					if(minValue <= currentHealth) temp++;
					break;
				case CriterionTypes.coin:
					if(minValue <= currentCoin) temp++;
					break;
				case CriterionTypes.time:
					if(minValue <= currentTime) temp++;
					break;
				default:
					break;
			}
		}
		
		if(temp == criterion.Length) result = true;
		else result = false;
		
		return result;
	}
}
