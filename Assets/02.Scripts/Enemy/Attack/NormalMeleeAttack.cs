using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public class NormalMeleeAttack : MonsterSkill
{
    public SphereCollider sphereCollider;

    public override void PerformAttack()
    {
        Debug.Log("NormalMeleeAttack!!!!");
        animator.SetTrigger("NormalMeleeAttack");
    }
}

