using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public abstract class MonsterSkill : MonoBehaviour
{
    public int phaseLevel;
    public int priority;
    public float skillRange;
    public float skillCoolTime;
    public float currentCooltime;

    public float skillStartDelayTime;
    public float skillEndDelayTime;

    private GameObject target;
    protected Animator animator;

    public LayerMask targetLayer;           // ������ ����� ���̾� (�÷��̾� ���̾� ����)
    public LayerMask obstacleLayer;         // ��ֹ� ���̾�

    private void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player");
    }

    public void UpdateCooldown(float deltaTime)
    {
        if (currentCooltime > 0)
        {
            currentCooltime -= deltaTime;
        }
    }

    public bool InRange()
    {
        if (target == null)
            return false;
        float distance = (target.transform.position - transform.position).magnitude;
        return distance < skillRange;
    }

    public bool IsReady()
    {
        return currentCooltime <= 0;
    }

    public abstract void PerformAttack(int skillNum);
}
