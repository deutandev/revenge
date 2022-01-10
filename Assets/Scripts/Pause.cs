using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
	public Animator pause, mission;
	
    public void PauseGame () 
    {
		Time.timeScale = 0;
		pause.SetTrigger("open");
	}

	public void ResumeGame () 
	{
		Time.timeScale = 1;
		pause.SetTrigger("close");
	}
	
	public void OpenMissionList()
	{
		Time.timeScale = 0;
		mission.SetTrigger("open");
	}
	
	public void CloseMissionList()
	{
		Time.timeScale = 1;
		mission.SetTrigger("close");
	}
	
	public void ShowConfirmation(Animator anim) {anim.SetTrigger("open");}
	public void CloseConfirmation(Animator anim) {anim.SetTrigger("close");}
}
