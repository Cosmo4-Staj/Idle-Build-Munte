using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingZone : MonoBehaviour
{
    public GameObject moneyImage;
    public AudioClip coinSound;

    private void Start()
    {
        moneyImage.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject otherObj = other.gameObject;
        switch (otherObj.tag)
        {
            case "Stoneworker":
                AudioSource.PlayClipAtPoint(coinSound, transform.position);
                moneyImage.SetActive(true);
                break;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        moneyImage.SetActive(false);
    }
}
