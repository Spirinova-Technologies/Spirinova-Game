using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class PiumPium : MonoBehaviourPunCallbacks
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
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Pium Pium(Clone)" && gm.selectedUnit.actionPoints >= 1 && gm.selectedUnit.disparoRapidoCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            DisparoRapido(gm.selectedUnit);   
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "Pium Pium(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.disparoParalizadorCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            DisparoParalizador(gm.selectedUnit);
        }
    }

    public void DisparoRapido(Unit unit)
    {
        if (unit.disparoRapidoCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.disparoRapidoCast = true;
            unit.actionPoints -= 1;
            unit.UpdateActionPointsText();
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

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}
