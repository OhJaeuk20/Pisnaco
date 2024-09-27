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

    public int maxHealth;   // �ִ�ü��
    public int curHealth;   // ���� ü��
    public int power;       // ���ݷ�

    public float normalSpeed = 3.5f;
    public float normalAccel = 8.0f;
    public float detectionDuration = 0.2f; // 0.2�� ���� ����

    public float viewRadius = 10f;          // ���� �þ� �ݰ�
    public float viewAngle = 45f;           // ���� �þ߰�

    public LayerMask targetLayer;          //�÷��̾� ���̾� ����
    public LayerMask obstacleLayer;         // ��ֹ� ���̾�

    public Animator anim;
    public Material mat;
    public GameObject target;
    public Rigidbody rb;

    void Start()
    {
        curHealth = maxHealth;
        mat.color = Color.white;
        currentState = State.Idle; // ���� ���¸� Idle�� ����
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

            // �þ߰� ���� �ִ��� Ȯ��
            float angleBetweenEnemyAndPlayer = Vector3.Angle(transform.forward, directionToTarget);
            if (angleBetweenEnemyAndPlayer < viewAngle / 2)
            {
                // �þ߰� ���� ������ ��ֹ��� ������ Ȯ��
                float distanceToTarget = Vector3.Distance(transform.position, targetTransform.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleLayer))
                {
                    // �÷��̾� �߰�
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
