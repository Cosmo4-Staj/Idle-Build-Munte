using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Count : MonoBehaviour
{
    StoneworkerSpawner stoneworkerSpawner;

    public TextMeshProUGUI stoneworkerText;


    void Start()
    {
        stoneworkerSpawner = FindObjectOfType<StoneworkerSpawner>();
    }

    void Update()
    {
        stoneworkerText.text = stoneworkerSpawner.stoneworkers.Count.ToString();
    }
}
