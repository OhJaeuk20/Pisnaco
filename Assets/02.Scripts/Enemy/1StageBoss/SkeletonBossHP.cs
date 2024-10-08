using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBossHP : MonsterHealth
{
    public enum PHASE {PHASE1, PHASE2}

    public PHASE currentPhase = PHASE.PHASE1;

    // Update is called once per frame
    void Update()
    {
        if (currentHp <= maxHp * 0.4f && currentPhase != PHASE.PHASE2)
        {
            currentPhase = PHASE.PHASE2;
        }
    }
}
