using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class Metamami : MonoBehaviourPunCallbacks
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
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Metamami(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.disparoParalizadorCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            DisparoParalizador(gm.selectedUnit);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "Metamami(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.ametralladoraCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Ametralladora(gm.selectedUnit);   
        }
    }

    public void DisparoParalizador(Unit unit)
    {
        if (unit.disparoParalizadorCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.disparoParalizadorCast = true;
            unit.actionPoints -= 4;
        }
    }

    public void Ametralladora(Unit unit)
    {
        if (unit.ametralladoraCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.ametralladoraCast = true;
            unit.actionPoints -= 4;
            unit.UpdateActionPointsText();
        }
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}