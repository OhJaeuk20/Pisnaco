using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MonsterBaseStat : MonoBehaviour
{
    public enum State
    {
        Idle,
        Chase,
        Attack,
        Die
    }

    public float rotateSpeed = 15.0f;

    public State currentState;

    public bool isStun = false;
    protected bool isAttack = false;
    public Vector3 moveDirection;

    public int maxHealth;   // 최대체력
    public int curHealth;   // 현재 체력
    public int power;       // 공격력

    public float normalSpeed = 3.5f;
    public float normalAccel = 8.0f;
    public float detectionDuration = 0.2f; // 0.2초 동안 감지

    public float viewRadius = 10f;          // 몬스터 시야 반경
    public float viewAngle = 45f;           // 몬스터 시야각

    public LayerMask targetLayer;          //플레이어 레이어 설정
    public LayerMask obstacleLayer;         // 장애물 레이어

    public Animator anim;
    public Material mat;
    public GameObject target;
    public Rigidbody rb;

    void Start()
    {
        curHealth = maxHealth;
        mat.color = Color.white;
        currentState = State.Idle; // 시작 상태를 Idle로 설정
        target = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Debug.Log($"{currentState}");
    }

    private bool SearchForPlayerInView()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetLayer);

        foreach (Collider target in targetsInViewRadius)
        {
            Transform targetTransform = target.transform;
            Vector3 directionToTarget = (targetTransform.position - transform.position).normalized;

            // 시야각 내에 있는지 확인
            float angleBetweenEnemyAndPlayer = Vector3.Angle(transform.forward, directionToTarget);
            if (angleBetweenEnemyAndPlayer < viewAngle / 2)
            {
                // 시야각 내에 있지만 장애물이 없는지 확인
                float distanceToTarget = Vector3.Distance(transform.position, targetTransform.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleLayer))
                {
                    // 플레이어 발견
                    this.target = targetTransform.gameObject;
                    return true;
                }
            }
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (curHealth == 0)
            return;
        if (other.tag == "MELEE")
        {
            Debug.Log("!!!");
            PWeapon weapon = other.GetComponent<PWeapon>();
            curHealth -= weapon.damage;
            StartCoroutine(OnDamage());
            Debug.Log("Melee : " + curHealth);
        }
        else if (other.tag == "SMELEE")
        {
            PWeapon weapon = other.GetComponent<PWeapon>();
            curHealth -= weapon.smashDamage;
            StartCoroutine(OnDamage());
            Debug.Log("Smash : " + curHealth);
        }
    }

    public IEnumerator OnDamage()
    {
        mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        if (curHealth > 0)
        {
            mat.color = Color.white;
        }
        else
        {
            currentState = State.Die;
            anim.SetTrigger("isDie");
            mat.color = Color.gray;
            Destroy(gameObject, 4);
        }
    }
}
