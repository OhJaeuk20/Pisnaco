using System.Collections;
using UnityEngine;

public class SpearShooter : MonsterSkill
{
    public override void PerformAttack(int skillNum)
    {
        Debug.Log("Cast Spear Rain");
        animator.SetInteger("Skill", skillNum); // 애니메이션 시작
        animator.SetBool("IsChanneling", true);
    }

    private GameObject fxTemp;

    public GameObject teleFx;
    public Transform teleportTarget;
    public GameObject landRangeFx;
    public GameObject landAtkFx;

    public GameObject spearPrefab;     // 창 프리팹
    public Transform spawnPoint;       // 창이 소환될 위치
    public int numberOfSpears;    // 발사할 창의 개수
    public float timeBetweenShots; // 창 발사 간격
    public float spearSpeed;     // 창이 날아가는 속도

    [Header("시간 설정")]
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
        // 코루틴을 통해 10발의 창을 순차적으로 발사
        StartCoroutine(ShootSpears());
    }

    IEnumerator ShootSpears()
    {
        for (int i = 0; i < numberOfSpears; i++)
        {
            // 창을 소환하고 방향을 설정
            ShootSpear();

            // 각 창 발사 간격
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

        // 피격된 대상들 중  지정된 각도 안에있는 대상을 타격함
        foreach (Collider hit in hits)
        {
            if (hit.GetComponent<PlayerStat>())
            {
                Debug.Log($"{hit.name} 플레이어를 타격함");
                hit.GetComponent<PlayerStat>().Hit(1);
            }
        }
        yield return new WaitForSeconds(endDelayTime);
        animator.SetBool("IsChanneling", false);
    }
}