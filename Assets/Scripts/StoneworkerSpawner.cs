using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneworkerSpawner : MonoBehaviour
{
    public GameObject stoneworkerPrefab;

    public List<GameObject> stoneworkers;

    public void StoneworkerSpawn()
    {
        GameObject stoneworker = Instantiate(stoneworkerPrefab, transform.position, transform.rotation);
        stoneworkers.Add(stoneworker);
    }
}
