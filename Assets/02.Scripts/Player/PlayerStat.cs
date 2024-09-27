using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerStat : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;
    public float speed;
    public float rotateSpeed;
    public bool isKnockDown = false;
    public float scanRadius;

    public LayerMask scanLayer;

    public PWeapon equipWeapon;

    private bool isHit = false;

    void Start()
    {
        equipWeapon = GetComponentInChildren<PWeapon>();
    }

    void Update()
    {
        ScanTimeObject();
    }

    private void OnTriggerEnter(Collider coll)
    {
        if ((coll.gameObject.CompareTag("DAMAGE1") || coll.gameObject.CompareTag("KNOCKBACKDAMAGE1")) && !isHit)
        {
            curHealth -= 1;
            Debug.Log("HEALTH -1");

            StartCoroutine(HitChk());
        }
    }

    IEnumerator HitChk()
    {
        isHit = true;
        yield return new WaitForSeconds(1.0f);
        isHit = false;
    }

    private void ScanTimeObject()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("!!!");
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, scanRadius, scanLayer);

            foreach (Collider collider in colliders)
            {
                collider.GetComponent<TimeObjController>().Recover(); 
            }
        }
    }
}