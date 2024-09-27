using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashCheck : MonoBehaviour
{

    private TimeObjController timeObjController;

    void Start()
    {
        timeObjController = GetComponentInParent<TimeObjController>();    
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("KNOCKBACKDAMAGE1"))
        {
            timeObjController.DestroyInstant();
        }
    }
}
