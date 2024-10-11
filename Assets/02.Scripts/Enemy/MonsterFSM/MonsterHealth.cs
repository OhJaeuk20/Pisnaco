using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ���� ü��
public class MonsterHealth : MonoBehaviour, IDamagable
{
    // �ִ� ü��
    [SerializeField] protected int maxHp;
    // ���� ü��
    [SerializeField] protected int currentHp;
    // ü�¹�
    [SerializeField] protected Image hpBarImage;

    // FSM ��Ʈ�ѷ�
    protected MonsterFSMController controller;

    // �ǰ� ����
    private bool isHit = false;
    public bool IsHit { get => isHit; set => isHit = value; }

    private void Awake()
    {
        controller = GetComponent<MonsterFSMController>();
    }

    private void Start()
    {
        currentHp = maxHp;
    }

    // �ǰ� ó��
    public virtual void Hit(int damage)
    {
        // ü�� ���� ó��
        currentHp -= damage;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);

        if (currentHp <= 0)
        {
            // ���� ���·� ��ȯ
            controller.TransactionToState(MonsterFSMController.STATE.DEATH);
        }
        else
        {
            // �ǰ� ���·� ��ȯ
            controller.TransactionToState(MonsterFSMController.STATE.HIT);
        }
    }

    // ü�¹� ������Ʈ
    private void UpdateHpBar()
    {
        hpBarImage.fillAmount = (float)currentHp / maxHp;
    }
}
