using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int power;
    public float length;
    public float forceAmount;
    public LayerMask targetLayer;

    public float lifeTime = 10.0f;
    private bool isActivate = false;
    private GameObject target;

    // Update is called once per frame
    void Update()
    {
        HitScan();
    }

    private void HitScan()
    {
        Debug.DrawRay(transform.position, transform.forward * length, Color.yellow);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, length, targetLayer))
        {
            isActivate = true;
            target = hit.transform.gameObject;
            if (target.GetComponent<PlayerStat>() != null)
            {
                //target.GetComponent<PlayerStat>().OnDamage(power);
            }
            Destroy(gameObject);
        }
        else if (lifeTime < 0)
        {
            Destroy(gameObject);
        }

        lifeTime -= Time.deltaTime;
    }
}
