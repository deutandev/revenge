using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantShooterAnimation : StateMachineBehaviour
{
    [HideInInspector] public bool shoot = false;
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
        shoot = true;
	}
}
