using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class DelphiArceo : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;

    public GameObject magicAuraDRunic; // Armor Debuff Animation

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Delphi Arceo(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.trifuerzaCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Trifuerza(gm.selectedUnit);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "Delphi Arceo(Clone)" && gm.selectedUnit.actionPoints >= 6 && gm.selectedUnit.demoniosEternosCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            DemoniosEternos(gm.selectedUnit);
        }
    }

    public void Trifuerza(Unit unit)
    {
        if (unit.trifuerzaCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.trifuerzaCast = true;
            unit.actionPoints -= 4;
        }
    }

    public void DemoniosEternos(Unit unit)
    {
        if (unit.demoniosEternosCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.demoniosEternosCast = true;
            unit.actionPoints -= 6;
            photonView.RPC("DemoniosEternosEnemy", RpcTarget.Others);
            unit.demoniosEternosCast = false;
        }
    }

    [PunRPC]
    public void DemoniosEternosEnemy()
    {
        foreach (Unit units in FindObjectsOfType<Unit>())
        {
            units.photonView.RPC("DemoniosEternosAnimation", RpcTarget.All);
            units.physicalArmor -= 1;
            gm.UpdateStatsPanel();
        }
    }

    [PunRPC]
    public void DemoniosEternosAnimation()
    {
        Instantiate(magicAuraDRunic, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}