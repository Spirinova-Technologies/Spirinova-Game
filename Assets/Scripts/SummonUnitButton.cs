using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonUnitButton : MonoBehaviour
{
    public GameObject summonUnitUI;
    private bool isSummonUnitUIActive = false;

    public void ToggleSummonUnitUI()
    {
        if (isSummonUnitUIActive == false)
        {
            summonUnitUI.SetActive(true);
            isSummonUnitUIActive = true;
        } else 
        {
            summonUnitUI.SetActive(false);
            isSummonUnitUIActive = false;
        }
    }
}