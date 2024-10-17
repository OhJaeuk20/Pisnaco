using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class AroundAOEAttack : MonsterSkill
{
    public override void PerformAttack(int skillNum)
    {
        Debug.Log("AroundAOE!!!!");
        animator.SetInteger("Skill", skillNum);
        animator.SetBool("IsChanneling", true);
    }

    public GameObject explosionPrefab;  // ���� ������
    public float explosionDistance = 2.0f;  // ���� ���� �Ÿ� ����
    public int explosionDirections = 8;   // ���� ���� ���� (8����)
    public float delayTime;

    // ���� �ִϸ��̼� �ǰ� �̺�Ʈ
    public void AroundAOEAtkAnimationEvent()
    {
        StartCoroutine(TriggerExplosions());
    }

    private IEnumerator TriggerExplosions()
    {
        yield return new WaitForSeconds(delayTime);  // ��� ������ ���ÿ� �Ͼ�Ƿ� ��ٸ��� �ʰ� ��� ����
        float angleStep = 360f / explosionDirections; // �� ���� ������ ���� ���

        for (int j = 0; j < explosionDirections; j++)
        {
            // ���� ���
            float angle = j * angleStep;
            // ���� ���� ��� (������ �������� ��ȯ �� ���� ����)
            Vector3 direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad));

            // ���� ��ġ ��� (������ ���� ���� ���Ϳ� �Ÿ� ���� ���)
            Vector3 explosionPosition = transform.position + direction * explosionDistance;

            // ���� �������� ����
            Instantiate(explosionPrefab, explosionPosition, Quaternion.identity);
        }

        animator.SetBool("IsChanneling", false);
    }
}