using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
	public Sprite levelBg, mainMenuBg;
	
	private Transform loadingScreen;
	
	private Text levelText;
	private Text progressText;
	private Transform eye;
	private Transform gameName;
	
	private Animator fade;
	
	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}
	
	private void Start()
	{
		loadingScreen = transform.Find("Loading Panel");
		
		levelText = loadingScreen.gameObject.transform.Find("Level Text").GetComponent<Text>();
		progressText = loadingScreen.gameObject.transform.Find("Progress").GetComponent<Text>();
		eye = loadingScreen.gameObject.transform.Find("Eye");
		gameName = loadingScreen.gameObject.transform.Find("Game Name");
		
		fade = transform.Find("Fade").GetComponent<Animator>();
	}
    
    public void loadLevel(int levelIndex)
    {
		string levelName = "level" + levelIndex.ToString();
		levelText.text = "Level " + levelIndex.ToString();
		StartCoroutine(loadAsync(levelName));
	}
	
	public void loadMainMenu()
	{
		StartCoroutine(loadAsync("Main menu", false));
	}
	
    public void GotoScene(string name) {SceneManager.LoadScene(name);}
    
    public void GoToCurrentScene() {SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);}
    
    public void Quit() { Application.Quit();}
    
    IEnumerator loadAsync(string sceneName, bool isLoadLevel=true)
    {
		Time.timeScale = 1;
		fade.SetTrigger("fadeIn");
		yield return new WaitForSeconds(0.5f);
		
		Image img = loadingScreen.GetComponent<Image>();
		if(isLoadLevel == true) img.sprite = levelBg;
		else if(isLoadLevel == false) 
		{
			img.sprite = mainMenuBg;
			gameName.localScale = new Vector3(0, 0, 0);
			levelText.text = "";
			eye.localScale = new Vector3(1f, 1f, 1f);
		}
		
		loadingScreen.localScale = new Vector3(1f, 1f, 1f);
		fade.SetTrigger("fadeOut");
		
		yield return new WaitForSeconds(0.5f);
		
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
		while(!operation.isDone)
		{
			float progress = Mathf.Clamp01(operation.progress/0.9f);
			progress *= 100;
			progressText.text = progress.ToString("0.##") + "%";
			yield return null;
		}
		
		fade.SetTrigger("fadeIn");
		yield return new WaitForSeconds(0.5f);
		
		loadingScreen.transform.localScale = new Vector3(0f, 0f, 0f);
		fade.SetTrigger("fadeOut");
		
		yield return new WaitForSeconds(10f);
		Destroy(gameObject);
	}
}
