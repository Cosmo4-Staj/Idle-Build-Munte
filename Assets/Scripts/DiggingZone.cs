using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggingZone : MonoBehaviour
{
    public static DiggingZone instance;

    public List<Transform> diggerPoints = new List<Transform>();

    public List<DigPointIdentifier> digPointIdentifiers;

}
