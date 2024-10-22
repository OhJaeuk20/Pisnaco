using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowDaggers : MonsterSkill
{
    public override void PerformAttack(int skillNum)
    {
        Debug.Log("ThrowingDaggers");
        animator.SetInteger("Skill", skillNum);
    }

    public GameObject arrowPrefab;
    public Transform spawnPoint;
    public float angleOffsetFor4;
    public float angleOffsetFor5;

    public void SpawnFourDaggers()
    {
        // �⺻ ȭ�� ���� (����)
        Vector3 forwardDirection = spawnPoint.forward;

        // ���� �� ���� ȭ�� ����
        Quaternion leftRotation1 = Quaternion.Euler(0, -angleOffsetFor4 * 1.5f, 0);
        Vector3 leftDirection1 = leftRotation1 * forwardDirection;

        Quaternion leftRotation2 = Quaternion.Euler(0, -angleOffsetFor4 * 0.5f, 0);
        Vector3 leftDirection2 = leftRotation2 * forwardDirection;

        // ���� �� ���� ȭ�� ����
        Quaternion rightRotation1 = Quaternion.Euler(0, angleOffsetFor4 * 0.5f, 0);
        Vector3 rightDirection1 = rightRotation1 * forwardDirection;

        Quaternion rightRotation2 = Quaternion.Euler(0, angleOffsetFor4 * 1.5f, 0);
        Vector3 rightDirection2 = rightRotation2 * forwardDirection;

        // ȭ�� ����
        Instantiate(arrowPrefab, spawnPoint.position, Quaternion.LookRotation(leftDirection1));
        Instantiate(arrowPrefab, spawnPoint.position, Quaternion.LookRotation(leftDirection2));
        Instantiate(arrowPrefab, spawnPoint.position, Quaternion.LookRotation(rightDirection1));
        Instantiate(arrowPrefab, spawnPoint.position, Quaternion.LookRotation(rightDirection2));
    }


    public void SpawnFiveDaggers()
    {
        // �⺻ ȭ�� ���� (����)
        Vector3 forwardDirection = spawnPoint.forward;

        // ���� �� ���� ȭ�� ����
        Quaternion leftRotation1 = Quaternion.Euler(0, -angleOffsetFor5 * 1f, 0);  // �� �ָ� ���� ȭ��
        Vector3 leftDirection1 = leftRotation1 * forwardDirection;

        Quaternion leftRotation2 = Quaternion.Euler(0, -angleOffsetFor5 * 0.5f, 0);  // �� ����� ȭ��
        Vector3 leftDirection2 = leftRotation2 * forwardDirection;

        // ���� �� ���� ȭ�� ����
        Quaternion rightRotation1 = Quaternion.Euler(0, angleOffsetFor5 * 0.5f, 0);  // �� ����� ȭ��
        Vector3 rightDirection1 = rightRotation1 * forwardDirection;

        Quaternion rightRotation2 = Quaternion.Euler(0, angleOffsetFor5 * 1f, 0);  // �� �ָ� ���� ȭ��
        Vector3 rightDirection2 = rightRotation2 * forwardDirection;

        // ȭ�� ����
        Instantiate(arrowPrefab, spawnPoint.position, Quaternion.LookRotation(forwardDirection));  
        Instantiate(arrowPrefab, spawnPoint.position, Quaternion.LookRotation(leftDirection1));    
        Instantiate(arrowPrefab, spawnPoint.position, Quaternion.LookRotation(leftDirection2));    
        Instantiate(arrowPrefab, spawnPoint.position, Quaternion.LookRotation(rightDirection1));   
        Instantiate(arrowPrefab, spawnPoint.position, Quaternion.LookRotation(rightDirection2));   
    }

}
