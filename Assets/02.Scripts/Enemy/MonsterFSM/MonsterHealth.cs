using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 몬스터 체력
public class MonsterHealth : MonoBehaviour, IDamagable
{
    // 최대 체력
    [SerializeField] protected int maxHp;
    // 현재 체력
    [SerializeField] protected int currentHp;
    // 체력바
    [SerializeField] protected Image hpBarImage;

    // FSM 컨트롤러
    protected MonsterFSMController controller;

    // 피격 여부
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

    // 피격 처리
    public virtual void Hit(int damage)
    {
        // 체력 감소 처리
        currentHp -= damage;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);

        if (currentHp <= 0)
        {
            // 죽음 상태로 전환
            controller.TransactionToState(MonsterFSMController.STATE.DEATH);
        }
        else
        {
            // 피격 상태로 전환
            controller.TransactionToState(MonsterFSMController.STATE.HIT);
        }
    }

    // 체력바 업데이트
    private void UpdateHpBar()
    {
        hpBarImage.fillAmount = (float)currentHp / maxHp;
    }
}
