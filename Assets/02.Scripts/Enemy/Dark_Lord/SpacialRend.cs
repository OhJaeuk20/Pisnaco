using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpacialRend : MonsterSkill
{
    public override void PerformAttack(int skillNum)
    {
        Debug.Log("------------SpacialRend------------");
        animator.SetInteger("Skill", skillNum);
        animator.SetBool("IsChanneling", true);
    }

    public Transform teleportTarget;
    public float chargingTime;

    [Header("파티클 세팅")]
    public GameObject teleFx;
    public GameObject chargingFx;
    public GameObject areaFx;
    public Transform fxTr;
    public GameObject atkFx;
    public Transform atkTr;

    private GameObject fxtemp1;
    private GameObject fxtemp2;

    private Quaternion finalRotation;

    public void Teleport()
    {
        navMeshAgent.isStopped = true;

        Instantiate(teleFx, transform.position, transform.rotation);

        navMeshAgent.Warp(teleportTarget.position);

        Instantiate(teleFx, transform.position, transform.rotation);
    }

    public void ChargingStart()
    {
        fxtemp1 = Instantiate(chargingFx, transform.position, transform.rotation);
        fxtemp2 = Instantiate(areaFx, fxTr.position, transform.rotation);
        StartCoroutine(Charging());
    }

    IEnumerator Charging()
    {
        yield return new WaitForSeconds(chargingTime);
        animator.SetTrigger("ChargingEnd");
        Destroy(fxtemp1);
        Destroy(fxtemp2);
    }

    public void SpaceRend()
    {
        SetPrefabRotation();
        Instantiate(atkFx, atkTr.position, finalRotation);
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRadius, targetLayer);

        // 피격된 대상들 중  지정된 각도 안에있는 대상을 타격함
        foreach (Collider hit in hits)
        {
            if (hit.GetComponent<PlayerStat>())
            {
                Debug.Log($"{hit.name} 플레이어를 즉사시킴");
                hit.GetComponent<PlayerStat>().Hit(10);
            }
                
        }
    }

    void SetPrefabRotation()
    {
        // 부모의 회전값
        Quaternion parentRotation = atkTr.rotation;

        // 프리팹의 로컬 회전을 쿼터니언으로 변환
        Quaternion prefabRotation = atkFx.transform.rotation;

        // 부모 회전과 프리팹 회전을 더하여 최종 회전값을 계산
        finalRotation = parentRotation * prefabRotation;
    }
}