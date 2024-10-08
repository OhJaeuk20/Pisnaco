using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.VFX;

public class PlayerStat : MonoBehaviour
{
    static public bool playerDie = false;

    public int maxHp;
    public int currentHp;
    public float speed;
    public float rotateSpeed;
    public bool isKnockDown = false;
    public float scanRadius;

    public LayerMask scanLayer;

    private Animator animator;
    public PWeapon equipWeapon;

    void Start()
    {
        animator = GetComponent<Animator>();
        equipWeapon = GetComponentInChildren<PWeapon>();
        currentHp = maxHp;
    }

    void Update()
    {
        ScanTimeObject();
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

    public void Hit(int damage)
    {
        // ü�� ���� ó��
        currentHp -= damage;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);

        if (currentHp <= 0)
        {
            // �ǰ� ���·� ��ȯ
            playerDie = true;
            animator.SetTrigger("Die");
            Debug.Log("�÷��̾ �׾����ϴ�.");
        }
    }
}