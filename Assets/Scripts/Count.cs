using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Count : MonoBehaviour
{
    StoneworkerSpawner stoneworkerSpawner;

    public TextMeshProUGUI stoneworkerText;

    // Start is called before the first frame update
    void Start()
    {
        stoneworkerSpawner = FindObjectOfType<StoneworkerSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        stoneworkerText.text = stoneworkerSpawner.stoneworkers.Count.ToString();
    }
}
