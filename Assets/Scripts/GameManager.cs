using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject digger;
    public Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Digger()
    {
        Instantiate(digger, spawnPoint.transform.position, digger.transform.rotation, spawnPoint);
    }
}
