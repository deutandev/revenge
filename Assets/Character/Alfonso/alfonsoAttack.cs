using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alfonsoAttack : StateMachineBehaviour
{
	[HideInInspector] public bool attack = false;
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
        attack = true;
	}
}
