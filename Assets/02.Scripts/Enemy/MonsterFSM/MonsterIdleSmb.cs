using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��� �ִϸ��̼� ���� �ӽ�
public class MonsterIdleSmb : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.GetComponent<TrailOnOff>().TrailOff();
    }

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{

	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
