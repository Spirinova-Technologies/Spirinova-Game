using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class DelphiElMagnifico : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;
    Unit unit;

    public GameObject fireShield; // Attack Buff Animation

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
        unit = GetComponent<Unit>();
        unit.dobleAtaqueDeDemonio = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Delphi, el Magnifico(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.iraMalditaCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            IraMaldita(gm.selectedUnit);
            gm.UpdateStatsPanel();      
        }
    }

    public void IraMaldita(Unit unit)
    {
        if (unit.iraMalditaCast == false)
        {
            photonView.RPC("IraMalditaAnimation", RpcTarget.All);
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.iraMalditaCast = true;
            unit.actionPoints -= 4;
            unit.UpdateActionPointsText();
            unit.attackDamage += 5;
            unit.iraMalditaCast = false;
        }
    }

    [PunRPC]
    public void IraMalditaAnimation()
    {
        Instantiate(fireShield, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}