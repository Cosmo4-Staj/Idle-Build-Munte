using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggerSpawner : MonoBehaviour
{
    public GameObject diggerPrefab;

    public List<GameObject> diggers;

    MoneyManager moneyManager;

    void Start()
    {
        moneyManager = FindObjectOfType<MoneyManager>();
    }

    public void DiggerSpawn()
    {
        if (diggers.Count == 17)
        {
            Debug.Log("not enough space for diggers");
            moneyManager.isFull = true;
        }
        GameObject digger = Instantiate(diggerPrefab,transform.position,Quaternion.identity);
        diggers.Add(digger);
        
    }
}
