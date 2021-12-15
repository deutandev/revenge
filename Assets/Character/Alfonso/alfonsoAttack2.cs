using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alfonsoAttack2 : StateMachineBehaviour
{
    [HideInInspector] public bool attack = false;
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
        attack = true;
	}
}
