using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SummonWeapon : MonoBehaviour
{
    public List<GameObject> weaponList = new List<GameObject>();

    void Start()
    {
        Unsummon();    
    }

    public void SummonSword()
    {
        weaponList[0].SetActive(true);
    }

    public void SummonDaggers()
    {
        weaponList[1].SetActive(true);
        weaponList[2].SetActive(true);
    }

    public void SummonSpear()
    {
        weaponList[3].SetActive(true);
    }
    
    public void Unsummon()
    {
        foreach (var weapon in weaponList)
        {
            weapon.SetActive(false);
        }
    }

    List<List<string>> education = new List<List<string>>()
    {
        new List<string> { "asdf","asdf"},
    };
    
}
