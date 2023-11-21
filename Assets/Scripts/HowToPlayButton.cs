using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlayButton : MonoBehaviour
{
    public GameObject howToPlayUI;
    private bool isHowToPlayUIActive = false;

    public void ToggleHowToPlayUI()
    {
        if (isHowToPlayUIActive == false)
        {
            howToPlayUI.SetActive(true);
            isHowToPlayUIActive = true;
        } else 
        {
            howToPlayUI.SetActive(false);
            isHowToPlayUIActive = false;
        }
    }
}