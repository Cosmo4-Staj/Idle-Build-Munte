using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggingZone : MonoBehaviour
{
    public static DiggingZone instance;

    //public GameObject[] diggerPoints;
    public List<Transform> diggerPoints = new List<Transform>();

    public List<DigPointIdentifier> digPointIdentifiers;

    // Start is called before the first frame update
    void Start()
    {
        /*if(diggerPoints == null)
            diggerPoints = GameObject.FindGameObjectsWithTag("Pos");*/

        /*for (int i = 0; i < diggerPoints.Count; i++)
        {
            digPointIdentifiers.Add(diggerPoints[i].gameObject.GetComponent<DigPointIdentifier>());
        }*/

    }
    void Update()
    {
        
    }
}
