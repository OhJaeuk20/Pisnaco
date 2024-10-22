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
        // 기본 화살 방향 (전방)
        Vector3 forwardDirection = spawnPoint.forward;

        // 좌측 두 개의 화살 방향
        Quaternion leftRotation1 = Quaternion.Euler(0, -angleOffsetFor4 * 1.5f, 0);
        Vector3 leftDirection1 = leftRotation1 * forwardDirection;

        Quaternion leftRotation2 = Quaternion.Euler(0, -angleOffsetFor4 * 0.5f, 0);
        Vector3 leftDirection2 = leftRotation2 * forwardDirection;

        // 우측 두 개의 화살 방향
        Quaternion rightRotation1 = Quaternion.Euler(0, angleOffsetFor4 * 0.5f, 0);
        Vector3 rightDirection1 = rightRotation1 * forwardDirection;

        Quaternion rightRotation2 = Quaternion.Euler(0, angleOffsetFor4 * 1.5f, 0);
        Vector3 rightDirection2 = rightRotation2 * forwardDirection;

        // 화살 생성
        Instantiate(arrowPrefab, spawnPoint.position, Quaternion.LookRotation(leftDirection1));
        Instantiate(arrowPrefab, spawnPoint.position, Quaternion.LookRotation(leftDirection2));
        Instantiate(arrowPrefab, spawnPoint.position, Quaternion.LookRotation(rightDirection1));
        Instantiate(arrowPrefab, spawnPoint.position, Quaternion.LookRotation(rightDirection2));
    }


    public void SpawnFiveDaggers()
    {
        // 기본 화살 방향 (전방)
        Vector3 forwardDirection = spawnPoint.forward;

        // 좌측 두 개의 화살 방향
        Quaternion leftRotation1 = Quaternion.Euler(0, -angleOffsetFor5 * 1f, 0);  // 더 멀리 퍼진 화살
        Vector3 leftDirection1 = leftRotation1 * forwardDirection;

        Quaternion leftRotation2 = Quaternion.Euler(0, -angleOffsetFor5 * 0.5f, 0);  // 더 가까운 화살
        Vector3 leftDirection2 = leftRotation2 * forwardDirection;

        // 우측 두 개의 화살 방향
        Quaternion rightRotation1 = Quaternion.Euler(0, angleOffsetFor5 * 0.5f, 0);  // 더 가까운 화살
        Vector3 rightDirection1 = rightRotation1 * forwardDirection;

        Quaternion rightRotation2 = Quaternion.Euler(0, angleOffsetFor5 * 1f, 0);  // 더 멀리 퍼진 화살
        Vector3 rightDirection2 = rightRotation2 * forwardDirection;

        // 화살 생성
        Instantiate(arrowPrefab, spawnPoint.position, Quaternion.LookRotation(forwardDirection));  
        Instantiate(arrowPrefab, spawnPoint.position, Quaternion.LookRotation(leftDirection1));    
        Instantiate(arrowPrefab, spawnPoint.position, Quaternion.LookRotation(leftDirection2));    
        Instantiate(arrowPrefab, spawnPoint.position, Quaternion.LookRotation(rightDirection1));   
        Instantiate(arrowPrefab, spawnPoint.position, Quaternion.LookRotation(rightDirection2));   
    }

}
