using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSpawner : MonoBehaviour
{
    public static StoneSpawner Instance;
    public List<GameObject> stones;

    public GameObject minerImage;
    public AudioSource minerCoinSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        minerImage.SetActive(false);
    }
    public void ActivateImage()
    {
        minerImage.SetActive(true);
        minerCoinSound.pitch = (Random.Range(.5f, 1.5f));
        minerCoinSound.Play();
        StartCoroutine(Deactivate());

    }

    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(1f);
        minerImage.SetActive(false);
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
