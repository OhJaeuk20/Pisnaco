using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeStunCheck : MonoBehaviour
{
    private MonsterBaseStat stat;
    // Start is called before the first frame update
    void Start()
    {
        stat = GetComponentInParent<MonsterBaseStat>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("STUNOBJ"))
        {
            stat.isStun = true;
            stat.anim.SetTrigger("Stun");
        }
        if(other.CompareTag("WALL"))
        {
            Debug.Log("WallTouch");
            stat.anim.SetTrigger("ChargeAttackEnd");
        }
    }
}