using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeObjController : MonoBehaviour
{
    public float stayTimer = 5.0f;
    public AudioSource audioSource;
    public AudioClip readySound, buildSound, destroySound;

    private float currentCoolTime = 0;
    public float coolTime = 15.0f;
    private Animator anim;
    public BoxCollider boxCollider;
    private bool isReady;
    public bool isActive;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (isReady) return;

        if (isActive)
        {
            return;
        }
        else
        {
            StartCoolTimer();
        }
        
    }

    public void Recover()
    {
        if (!isReady) return;
        isReady = false;
        currentCoolTime = coolTime;
        anim.SetTrigger("Recover");
    }

    public void DelayDestroyObject()
    {
        StartCoroutine("DestroyObject");
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(stayTimer);
        anim.SetTrigger("Destroy");
    }

    public void StartCoolTimer()
    {
        if (currentCoolTime < 0) isReady = true;
        currentCoolTime -= Time.deltaTime;
    }

    public void DestroyInstant()
    {
        StopCoroutine(DestroyObject());
        anim.SetTrigger("Destroy");
    }
}
