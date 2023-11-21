using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnselectFromBarracks : MonoBehaviour
{
    private GameObject unitToUnselect;

    public void Unselect()
    {
        unitToUnselect = GameObject.Find("AlianzaDelphiButton");
        unitToUnselect.GetComponent<Button>().interactable = true;
        Destroy(gameObject);
    }
}
