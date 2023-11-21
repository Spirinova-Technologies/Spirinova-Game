using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class TolynLaPluma : MonoBehaviourPunCallbacks
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
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Tolyn, la Pluma(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.degollarCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Degollar(gm.selectedUnit);   
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "Tolyn, la Pluma(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.desmembrarCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Desmembrar(gm.selectedUnit);   
        }
    }

    public void Degollar(Unit unit)
    {
        if (unit.degollarCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.degollarCast = true;
            unit.actionPoints -= 4;
            unit.UpdateActionPointsText();
        }
    }

    public void Desmembrar(Unit unit)
    {
        if (unit.desmembrarCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.desmembrarCast = true;
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