using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Image levelSlider;

    public float progress;

    //Level loading bar
    public void SetProgress(float fillAmount)
    {
        levelSlider.fillAmount = fillAmount;
        progress = fillAmount;
    }
}
