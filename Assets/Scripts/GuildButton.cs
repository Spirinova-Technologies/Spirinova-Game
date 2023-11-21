using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuildButton : MonoBehaviour
{
    public GameObject guildUI;
    public GameObject guildFunctionalitiesUI;
    private bool isGuildUIActive = false;

    public void ToggleGuildUI()
    {
        if (isGuildUIActive == false)
        {
            guildUI.SetActive(true);
            isGuildUIActive = true;
        } else 
        {
            guildUI.SetActive(false);
            guildFunctionalitiesUI.SetActive(false);
            isGuildUIActive = false;
        }
    }
}
