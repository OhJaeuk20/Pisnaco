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

    [Header("��ƼŬ ����")]
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

        // �ǰݵ� ���� ��  ������ ���� �ȿ��ִ� ����� Ÿ����
        foreach (Collider hit in hits)
        {
            if (hit.GetComponent<PlayerStat>())
            {
                Debug.Log($"{hit.name} �÷��̾ ����Ŵ");
                hit.GetComponent<PlayerStat>().Hit(10);
            }
                
        }
    }

    void SetPrefabRotation()
    {
        // �θ��� ȸ����
        Quaternion parentRotation = atkTr.rotation;

        // �������� ���� ȸ���� ���ʹϾ����� ��ȯ
        Quaternion prefabRotation = atkFx.transform.rotation;

        // �θ� ȸ���� ������ ȸ���� ���Ͽ� ���� ȸ������ ���
        finalRotation = parentRotation * prefabRotation;
    }
}