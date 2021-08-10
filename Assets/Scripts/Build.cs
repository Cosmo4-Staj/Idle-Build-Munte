using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Build : MonoBehaviour
{
    public static Build Instance;
    public List<GameObject> building;
    public List<GameObject> sortedBuilding = new List<GameObject>();
    int itemNum;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        foreach(GameObject g in building)
        {
            g.SetActive(false);
        }

        //sortedBuilding = building.OrderBy(x => Vector2.Distance(transform.position, x.transform.position)).ToList();
        sortedBuilding = building.OrderBy(platform => platform.transform.position.y).ToList();

    }

    public void Activate()
    {
        itemNum++;
        if (itemNum == sortedBuilding.Count)
        {
            itemNum = 0;
            GameManager.instance.OnLevelCompleted();
        }
        sortedBuilding[itemNum].SetActive(true);
    }
}
