using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperworkerSpawner : MonoBehaviour
{
    public GameObject superworkerPrefab;

    public GameObject superworker;

    public void SuperworkerSpawn()
    {
        superworker = Instantiate(superworkerPrefab, transform.position, transform.rotation);
    }
}
