using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class MonsterSkillController : MonoBehaviour
{
    [SerializeField]
    private MonsterSkill nextSkill = null;
    public MonsterSkill NextSkill { get => nextSkill; set => nextSkill = value;}

    public List<MonsterSkill> skillVarient = new List<MonsterSkill>();

    private float defaultAttackDistance;

    public bool isCasting = false;

    private MonsterFSMInfo FSMInfo;
    private Animator animator;

    void Start()
    {
        FSMInfo = GetComponent<MonsterFSMInfo>();
        animator = GetComponent<Animator>();
        defaultAttackDistance = FSMInfo.AttackDistance;
    }

    void Update()
    {
        UpdateSkillState();
        if (isCasting) return;
        CheckNextAttack();
        SelectSkill();
    }

   public void UpdateSkillState()
    {
        foreach (MonsterSkill skill in skillVarient)
        {
            skill.UpdateCooldown(Time.deltaTime);
            skill.InRange();
        }
    }

    void CheckNextAttack()
    {
        //nextSkill = null;
        int highestPriority = int.MaxValue;

        foreach (MonsterSkill skill in skillVarient )
        {
            if (skill.InRange() && skill.IsReady() && skill.priority < highestPriority)
            {
                highestPriority = skill.priority;
                nextSkill = skill;
            }
        }
    }

    public void SelectSkill()
    {
        if(nextSkill != null)
        {
            FSMInfo.AttackDistance = nextSkill.skillRange;
            nextSkill.currentCooltime = nextSkill.skillCoolTime;
            nextSkill.PerformAttack(skillVarient.IndexOf(nextSkill));
        }
        else
        {
            animator.SetInteger("Skill", -1);
            ResetAttackDistance();
        }
    }

    public void ResetAttackDistance()
    {
        FSMInfo.AttackDistance = defaultAttackDistance;
    }

    public void SetIsCasting(bool tf)
    {
        isCasting = tf;
        animator.SetBool("IsCasting", isCasting);
    }
}
