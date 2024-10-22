using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class VanishAndBlink : MonsterSkill
{
    private Renderer[] renderers;

    [Header("Skill Settings")]
    public float disappearDuration = 1.0f;
    public float distanceBehindPlayer = 2.0f;

    [Header("Particle Effects")]
    public GameObject vanishParticlePrefab;
    public Transform paricleTr;

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>(true);
    }

    public override void PerformAttack(int skillNum)
    {
        Debug.Log("Blink Skill Performed");
        animator.SetInteger("Skill", skillNum); // �ִϸ��̼� ����
        animator.SetBool("IsChanneling", true);
    }

    // ���� �ִϸ��̼ǿ��� ȣ��� �̺�Ʈ
    public void BlinkAnimationEvent()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
        StartCoroutine(TeleportToPlayer(target.transform));
    }

    // �ڷ���Ʈ �ڷ�ƾ
    IEnumerator TeleportToPlayer(Transform player)
    {
        // �÷��̾� ���� ��ǥ ��ġ ���
        Vector3 behindPlayerPosition = player.position - player.forward * distanceBehindPlayer;

        // NavMesh ���� ��ȿ�� ��ġ�� ã��
        if (NavMesh.SamplePosition(behindPlayerPosition, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {
            
            SetRenderersActive(false);
            Instantiate(vanishParticlePrefab, paricleTr.position, Quaternion.identity);

            // 2. NavMesh ���� �� ��ġ�� ����
            navMeshAgent.Warp(hit.position);

            // 3. ���� �ð� ���� ���
            yield return new WaitForSeconds(disappearDuration);

            // 4. ��� �������� �ٽ� Ȱ��ȭ (���� ��Ÿ��)
            Instantiate(vanishParticlePrefab, paricleTr.position, Quaternion.identity);
            SetRenderersActive(true);
            gameObject.layer = LayerMask.NameToLayer("Monster");
            animator.SetBool("IsChanneling", false);
            Debug.Log("Monster teleported behind the player and reappeared!");
        }
        else
        {
            Debug.LogWarning("No valid NavMesh position found behind the player.");
        }
    }

    // ��� �������� Ȱ��ȭ �Ǵ� ��Ȱ��ȭ�ϴ� �Լ�
    private void SetRenderersActive(bool isActive)
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = isActive;
        }
    }
}