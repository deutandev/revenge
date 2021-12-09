using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	public bool isComplete = false, isGameover = false;
	public GameObject gameOverPanel, completePanel;
	
    // Start is called before the first frame update
    private void Start()
    {
        isComplete = false;
        isGameover = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if(isComplete == true) completePanel.SetActive(true);
        else if(isGameover == true) gameOverPanel.SetActive(true);
    }
}
