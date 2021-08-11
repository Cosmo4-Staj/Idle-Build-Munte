using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSpawner : MonoBehaviour
{ 
    public static StoneSpawner Instance;
    public List<GameObject> stones;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void RemoveStone(GameObject obj)
    {
        for (int i = 0; i < stones.Count; i++)
        {
            if (stones[i] == obj)
            {
                stones.RemoveAt(i);
            }
        }
    }
}
