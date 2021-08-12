using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class StoneworkerController : MonoBehaviour
{
    //[Tooltip("")]
    Build build;

    Animator anim;
    MoneyManager moneyManager;

    public NavMeshAgent navMeshAgent;
    public GameObject pickedUpStone;
    private GameObject dropPoint;

    [SerializeField] GameObject target;
    [SerializeField] GameObject otherObj;
    [SerializeField] bool isPicked = false;

    public int currentCubeNumber;
    public int totalCubeNumber;

    public GameObject waitingSpot;
    public bool isTrigger;

    void Start()
    {
        pickedUpStone.SetActive(false);
        totalCubeNumber = FindObjectOfType<Build>().building.Count;
        build = FindObjectOfType<Build>();
        anim = gameObject.GetComponent<Animator>();
        moneyManager = FindObjectOfType<MoneyManager>();

        currentCubeNumber = 0;
    }

    void Update()
    {
        //for progress bar
        currentCubeNumber = build.itemNum;
        FindObjectOfType<GameUI>().SetProgress(currentCubeNumber /(float) totalCubeNumber);

        if (!isTrigger)
        {
            anim.SetBool("walk", true);
        }
        else
        {
            anim.SetBool("walk", false);
        }

        //if there is no target find stone and set target to this stone
        if (target == null)
        {
            if (GameObject.FindGameObjectWithTag("Pickup"))
            {
                target = GameObject.FindGameObjectWithTag("Pickup");
            }
            else
            {
                waitingSpot = GameObject.FindGameObjectWithTag("WaitingSpot");
                navMeshAgent.SetDestination(waitingSpot.transform.position);

            }
        }
        //movement to the target with navmesh
        else
        {
            navMeshAgent.SetDestination(target.transform.position);
        }
        
    }

    //private void OnCollisionEnter(Collision collision) ???
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != target) return;

        otherObj = other.gameObject;
        switch (otherObj.tag)
        {
            //if stoneworker touches stone object -> then remove stone from the list of stones, destroy targeted stone and change destination to drop point
            case "Pickup":
                if (otherObj == target && isPicked == false)
                {
                    isPicked = true;
                    StoneSpawner.Instance.RemoveStone(otherObj.gameObject);
                    CreateStone();
                    Destroy(target);
                    dropPoint = GameObject.FindGameObjectWithTag("DropPoint");
                    target = dropPoint;
                }
                break;

            //if stoneworker reaches the drop point -> disable stone object in vagoon and change the destination point back to any stone from the list
            case "DropPoint":
                //building
                pickedUpStone.SetActive(false);
                Build.Instance.Activate();
                target = null;
                isPicked = false;
                moneyManager.AddMoney(2);
                break;

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "WaitingSpot")
        {
            isTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "WaitingSpot")
        {
            isTrigger = false;
        }
    }

    //funtion to pop-up stone in the vagoon when stone picked up
    public void CreateStone()
    {
        pickedUpStone.SetActive(true);
    }

}