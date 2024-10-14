using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Arrow : MonoBehaviour
{
    public int power;
    public float length;
    public float speed;
    public LayerMask targetLayer;

    public float lifeTime = 10.0f;
    private GameObject target;

    // Update is called once per frame
    void Update()
    {
        HitScan();
        Move();
    }

    private void HitScan()
    {
        Debug.DrawRay(transform.position, transform.forward * length, Color.yellow);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, length, targetLayer))
        {
            target = hit.transform.gameObject;
            if (target.GetComponent<PlayerStat>() != null)
            {
                target.GetComponent<PlayerStat>().Hit(power);
            }
            Destroy(gameObject);
        }
        else if (lifeTime < 0)
        {
            Destroy(gameObject);
        }

        lifeTime -= Time.deltaTime;
    }

    private void Move()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
