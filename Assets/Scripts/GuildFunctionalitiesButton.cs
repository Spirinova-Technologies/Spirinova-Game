using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuildFunctionalitiesButton : MonoBehaviour
{
    public GameObject guildFunctionalitiesUI;
    private bool isGuildFunctionalitiesUIActive = false;

    public void ToggleGuildFunctionalitiesUI()
    {
        if (isGuildFunctionalitiesUIActive == false)
        {
            guildFunctionalitiesUI.SetActive(true);
            isGuildFunctionalitiesUIActive = true;
        } else 
        {
            guildFunctionalitiesUI.SetActive(false);
            isGuildFunctionalitiesUIActive = false;
        }
    }
}
