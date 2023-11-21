using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarracksButton : MonoBehaviour
{
    public GameObject barracksA;
    public GameObject barracksB;
    private bool isBarrackActive = false;

    public void ToggleBarracks()
    {
        if (isBarrackActive == false)
        {
            barracksA.SetActive(true);
            barracksB.SetActive(true);
            isBarrackActive = true;
        } else 
        {
            barracksA.SetActive(false);
            barracksB.SetActive(false);
            isBarrackActive = false;
        }
    }
}
