using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public abstract class MonsterSkill : MonoBehaviour
{
    public int phaseLevel;
    public int priority;
    public float minskillRange;
    public float skillRange;
    public float skillCoolTime;
    public float currentCooltime;

    [SerializeField] protected float attackRadius;
    [SerializeField] protected float hitAngle;

    [SerializeField] protected float knockbackForce;
    [SerializeField] protected float knockbackTime;

    public GameObject target;
    protected Animator animator;

    public LayerMask targetLayer;           // 공격할 대상의 레이어 (플레이어 레이어 설정)
    public LayerMask obstacleLayer;         // 장애물 레이어

    private void Awake()
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
        return (minskillRange < distance) && (distance < skillRange);
    }

    public bool IsReady()
    {
        return currentCooltime <= 0;
    }

    public abstract void PerformAttack(int skillNum);
}
