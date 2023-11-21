using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class Chispitas : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;
    Unit unit;

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
        unit = GetComponent<Unit>();
        unit.inmortalidad = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Chispitas(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.disparoParalizadorCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            DisparoParalizador(gm.selectedUnit);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "Chispitas(Clone)" && gm.selectedUnit.actionPoints >= 2 && gm.selectedUnit.alquimiaCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Alquimia(gm.selectedUnit);     
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

    public void Alquimia(Unit unit)
    {
        if (unit.alquimiaCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.alquimiaCast = true;
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