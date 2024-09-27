using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonChargeAttack : MonsterSkill
{
    [SerializeField] private float ChargeDuration;
    public SphereCollider sphereCollider;
    public float chargeSpeed;

    public override void PerformAttack()
    {
        animator.SetTrigger("ChargeAttack");        
    }

    public void StartChargeAttack()
    {
        StartCoroutine("DelayChargeEnd");
    }

    public void Charge()
    {
        stat.rb.velocity = transform.forward * chargeSpeed;
    }

    IEnumerator DelayChargeEnd()
    {
        yield return new WaitForSeconds(ChargeDuration);
        animator.SetTrigger("ChargeAttackEnd");
        Debug.Log("ChargeEnd");
    }
}
