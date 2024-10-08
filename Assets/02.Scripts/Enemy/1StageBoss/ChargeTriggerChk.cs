using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeTriggerChk : MonoBehaviour
{
    private SkeletonChargeAttack parentScript;

    void Start()
    {
        parentScript = GetComponentInParent<SkeletonChargeAttack>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (parentScript != null)
        {
            parentScript.ChargeTriggerEnter(other);
        }
    }
}
