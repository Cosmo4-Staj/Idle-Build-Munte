using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DiggerController : MonoBehaviour
{
    bool check = false;
    bool check2 = false;

    public NavMeshAgent navMeshAgent;
    public Transform target;
    public GameObject stonePrefab;
    Animator anim;
    StoneSpawner stoneSpawner;

    void Start()
    {
        navMeshAgent.SetDestination(target.position);
        stoneSpawner = FindObjectOfType<StoneSpawner>();
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject otherObj = other.gameObject;

        switch (otherObj.tag)
        {
            case "BigStone":
                if (check == false)
                {
                    check = true;
                    DiggingZone diggingZone = otherObj.GetComponent<DiggingZone>();

                    for (int i = 0; i <= diggingZone.digPointIdentifiers.Count; i++)
                    {
                        //TODO: yer kalmadiginda bastan baslat 

                        if (diggingZone.digPointIdentifiers[i].isEmpty)
                        {
                            // nav mesh diggingpoints[i] ye gidecek
                            navMeshAgent.SetDestination(diggingZone.diggerPoints[i].position);

                            diggingZone.digPointIdentifiers[i].isEmpty = false;
                            break;
                        }
                    }
                }
                break;

            case "Pos":
                if (check2 == false)
                {
                    check2 = true;
                    anim = GetComponent<Animator>();
                    GetComponent<CapsuleCollider>().enabled = false;
                    anim.SetBool("dig", true);
                    StartCoroutine(Spawn(GameManager.instance.spawnStartTime));
                    //InvokeRepeating("Spawn", GameManager.instance.spawnStartTime, GameManager.instance.spawnRepeatTime);
                }
                break;
        }
    }

    IEnumerator Spawn(float time)
    {
        yield return new WaitForSeconds(time);
        while (true)
        {
            GameObject stone = Instantiate(stonePrefab, stoneSpawner.transform.position, Quaternion.identity, stoneSpawner.transform);
            stoneSpawner.stones.Add(stone);
            yield return new WaitForSeconds(GameManager.instance.spawnRepeatTime);
        }

    }

    /*    public void Spawn()
    {
        GameObject stone = Instantiate(stonePrefab, stoneSpawner.transform.position, Quaternion.identity, stoneSpawner.transform);
        stoneSpawner.stones.Add(stone);
    }*/

}
