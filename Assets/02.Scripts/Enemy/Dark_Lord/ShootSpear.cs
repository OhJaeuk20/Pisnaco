using System.Collections;
using UnityEngine;

public class SpearShooter : MonsterSkill
{
    public override void PerformAttack(int skillNum)
    {
        Debug.Log("Cast Spear Rain");
        animator.SetInteger("Skill", skillNum); // �ִϸ��̼� ����
        animator.SetBool("IsChanneling", true);
    }

    private GameObject fxTemp;

    public GameObject teleFx;
    public Transform teleportTarget;
    public GameObject landRangeFx;
    public GameObject landAtkFx;

    public GameObject spearPrefab;     // â ������
    public Transform spawnPoint;       // â�� ��ȯ�� ��ġ
    public int numberOfSpears;    // �߻��� â�� ����
    public float timeBetweenShots; // â �߻� ����
    public float spearSpeed;     // â�� ���ư��� �ӵ�

    [Header("�ð� ����")]
    public float termForLand;
    public float endDelayTime;

    public void TeleportForSpearshot()
    {
        navMeshAgent.isStopped = true;
        animator.SetBool("IsChanneling", true);

        Instantiate(teleFx, transform.position, transform.rotation);

        navMeshAgent.enabled = false;
        transform.position = teleportTarget.position;

        Instantiate(teleFx, transform.position, transform.rotation);
    }

    public void StartShootSpears()
    {
        // �ڷ�ƾ�� ���� 10���� â�� ���������� �߻�
        StartCoroutine(ShootSpears());
    }

    IEnumerator ShootSpears()
    {
        for (int i = 0; i < numberOfSpears; i++)
        {
            // â�� ��ȯ�ϰ� ������ ����
            ShootSpear();

            // �� â �߻� ����
            yield return new WaitForSeconds(timeBetweenShots);
        }
        StartShowRange();

        yield return new WaitForSeconds(termForLand);
        animator.SetTrigger("ChargingEnd");
    }

    void ShootSpear()
    {
        GameObject spear = Instantiate(spearPrefab, spawnPoint.position, Quaternion.identity);
    }

    private void StartShowRange()
    {
        StartCoroutine(ShowRange());
    }

    IEnumerator ShowRange()
    {
        float time = 0;
 
        fxTemp = Instantiate(landRangeFx, target.transform.position, Quaternion.identity);
        
        while (time < termForLand - 1)
        {
            fxTemp.transform.position = target.transform.position;
            time += Time.deltaTime;
            yield return null;
        }
    }

    public void StartLandAttack()
    {
        StartCoroutine(LandAttack());
    }

    IEnumerator LandAttack()
    {
        transform.position = fxTemp.transform.position;
        Destroy(fxTemp);
        navMeshAgent.enabled = true;
        Instantiate(landAtkFx, transform.position, transform.rotation);
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRadius, targetLayer);

        // �ǰݵ� ���� ��  ������ ���� �ȿ��ִ� ����� Ÿ����
        foreach (Collider hit in hits)
        {
            if (hit.GetComponent<PlayerStat>())
            {
                Debug.Log($"{hit.name} �÷��̾ Ÿ����");
                hit.GetComponent<PlayerStat>().Hit(1);
            }
        }
        yield return new WaitForSeconds(endDelayTime);
        animator.SetBool("IsChanneling", false);
    }
}