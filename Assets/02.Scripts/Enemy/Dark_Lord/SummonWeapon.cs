using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SummonWeapon : MonoBehaviour
{
    public List<GameObject> weaponList = new List<GameObject>();
    public GameObject summonEffect;

    void Start()
    {
        Unsummon();    
    }

    public void SummonSword()
    {
        Instantiate(summonEffect, weaponList[0].transform.position, Quaternion.identity);
        weaponList[0].SetActive(true);
    }

    public void SummonDaggers()
    {
        Instantiate(summonEffect, weaponList[1].transform.position, Quaternion.identity);
        Instantiate(summonEffect, weaponList[2].transform.position, Quaternion.identity);
        weaponList[1].SetActive(true);
        weaponList[2].SetActive(true);
    }

    public void SummonSpear()
    {
        Instantiate(summonEffect, weaponList[3].transform.position, Quaternion.identity);
        weaponList[3].SetActive(true);
    }
    
    public void Unsummon()
    {
        foreach (var weapon in weaponList)
        {
            if (weapon.gameObject.activeSelf)
            {
                Instantiate(summonEffect, weapon.transform.position, Quaternion.identity);
                weapon.SetActive(false);
            }          
        }
    }

    List<List<string>> education = new List<List<string>>()
    {
        new List<string> { "asdf","asdf"},
    };
    
}
