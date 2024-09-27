using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSkillControler : MonoBehaviour
{

    private MonsterSkill nextSkill = null;

    public List<MonsterSkill> skillVarient = new List<MonsterSkill>();

    public bool skillAlreadyUse;
    public bool isCasting;

    private MonsterBaseStat stat;

    void Start()
    {
        stat = GetComponent<MonsterBaseStat>();
    }

    void Update()
    {
        Debug.Log("isCasting : " + isCasting);
        UpdateSkillState();
        if (isCasting) return;
        CheckNextAttack();
    }

   public void UpdateSkillState()
    {
        foreach (MonsterSkill skill in skillVarient)
        {
            skill.UpdateCooldown(Time.deltaTime);
            skill.InRange();
        }
    }

    public void CheckNextAttack()
    {
        nextSkill = null;
        int highestPriority = int.MaxValue;

        foreach (MonsterSkill skill in skillVarient )
        {
            if (skill.InRange() && skill.IsReady() && skill.priority < highestPriority)
            {
                highestPriority = skill.priority;
                nextSkill = skill;
            }
        }

        if(nextSkill != null)
        {
            SelectSkill(nextSkill);
            nextSkill.currentCooltime = nextSkill.skillCoolTime;
        }
    }

    void SelectSkill(MonsterSkill skill)
    {
        isCasting = true;
        skill.PerformAttack();
    }

    public void DelayCastingEnd()
    {
        StartCoroutine(CastingEnd());
    }

    IEnumerator CastingEnd()
    {
        yield return new WaitForSeconds(nextSkill.skillEndDelayTime);
        isCasting = false;
    }
}
