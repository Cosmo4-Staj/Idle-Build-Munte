using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Build : MonoBehaviour
{
    public static Build Instance;
    public List<GameObject> building;
    public List<GameObject> sortedBuilding = new List<GameObject>();
    public int itemNum;
    public ParticleSystem smoke;

    [SerializeField] float lifeTime = 3f;


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
        ParticleSystem smokeInstance = Instantiate(smoke, sortedBuilding[itemNum].transform.position, sortedBuilding[itemNum].transform.rotation);
        Destroy(smokeInstance.gameObject, lifeTime);

        sortedBuilding[itemNum].SetActive(true);
        
    }
}
