using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class ElHechizeroDelHielo : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "El Hechizero del Hielo(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.avalanchaCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Avalancha(gm.selectedUnit);   
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "El Hechizero del Hielo(Clone)" && gm.selectedUnit.actionPoints >= 2 && gm.selectedUnit.bolaDeNieveCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            BolaDeNieve(gm.selectedUnit);   
        }
    }

    public void Avalancha(Unit unit)
    {
        if (unit.avalanchaCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.avalanchaCast = true;
            unit.actionPoints -= 4;
            unit.UpdateActionPointsText();
        }
    }

    public void BolaDeNieve(Unit unit)
    {
        if (unit.bolaDeNieveCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.bolaDeNieveCast = true;
            unit.actionPoints -= 2;
            unit.UpdateActionPointsText();
        }
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}