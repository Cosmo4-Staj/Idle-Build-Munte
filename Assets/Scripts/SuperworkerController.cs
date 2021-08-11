using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SuperworkerController : MonoBehaviour
{
    bool check = false;
    bool check2 = false;

    public NavMeshAgent navMeshAgent;
    public Transform target;
    public GameObject stonePrefab;
    Animator anim;
    StoneSpawner stoneSpawner;

    public float superworkerTime = 4f;
    [SerializeField] int superStoneCount = 3;

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
                        if (diggingZone.digPointIdentifiers[i].isEmpty)
                        {
                            Debug.Log("entered big stone");
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
                    Debug.Log("dig point");
                    anim = GetComponent<Animator>();
                    GetComponent<CapsuleCollider>().enabled = false;
                    anim.SetBool("dig", true);
                    StartCoroutine(Spawn(superworkerTime));
                }
                break;
        }
    }

    IEnumerator Spawn(float time)
    {
        yield return new WaitForSeconds(time);
        while (true)
        {
            for(int i = 0; i < superStoneCount; i++)
            {
                GameObject stone = Instantiate(stonePrefab, stoneSpawner.transform.position, Quaternion.identity, stoneSpawner.transform);
                stoneSpawner.stones.Add(stone);
            }
            
            yield return new WaitForSeconds(superworkerTime);
        }

    }
}
