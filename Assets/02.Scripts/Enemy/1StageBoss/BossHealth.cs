using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class BossHealth : MonsterHealth
{
    public enum PHASE {PHASE1, PHASE2, PHASE3}

    // �ǰ� ��ƼŬ ������Ʈ
    [SerializeField] protected ParticleSystem hitParticle;

    [SerializeField] private int phase2Hp;
    [SerializeField] private int phase3Hp;

    private Animator animator;

    public PHASE currentPhase = PHASE.PHASE1;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHp <= phase2Hp && currentPhase != PHASE.PHASE2)
        {
            currentPhase = PHASE.PHASE2;
            animator.SetInteger("Phase", (int)PHASE.PHASE2);
            //controller.TransactionToState(MonsterFSMController.STATE.ACTION);
        }

        if (currentHp <= phase3Hp && currentPhase != PHASE.PHASE3)
        {
            currentPhase = PHASE.PHASE3;
            animator.SetInteger("Phase", (int)PHASE.PHASE3);
        }
    }

    public override void Hit(int damage)
    {
        currentHp -= damage;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);
        hitParticle.Play();

        if (currentHp <= 0)
        {
            // ���� ���·� ��ȯ
            controller.TransactionToState(MonsterFSMController.STATE.DEATH);
        }
    }
}