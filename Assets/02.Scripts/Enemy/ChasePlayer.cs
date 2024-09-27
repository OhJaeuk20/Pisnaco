using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class ChasePlayer : MonoBehaviour
{
    private MonsterBaseStat stat;
    private Animator animator;
    private MonsterSkillControler skillControler;

    private bool fixRotation = false;

    public NavMeshAgent nvAgent;

    void Start()
    {
        stat = GetComponent<MonsterBaseStat>();
        nvAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        skillControler = GetComponent<MonsterSkillControler>();
    }

    void Update()
    {
        RotateSmooth();
        if (stat.isStun || skillControler.isCasting) return;
        Chase();
        
    }

    public void RotateSmooth()
    {
        if (!fixRotation)
        {
            stat.moveDirection = (stat.target.transform.position - transform.position).normalized;
            Quaternion rot = Quaternion.LookRotation(stat.moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * stat.rotateSpeed);
        }
    }

    public void Chase()
    {
        if (stat.target != null)
        {
            animator.SetTrigger("Move");
            stat.currentState = MonsterBaseStat.State.Chase;
            nvAgent.SetDestination(stat.target.transform.position);
        }
        else
        {
            Debug.Log("Set Idle");
            animator.SetTrigger("Idle");
            stat.currentState = MonsterBaseStat.State.Idle;
        }
    }

    public void FixRotationSwitch()
    {
        fixRotation = !fixRotation;
    }
}
