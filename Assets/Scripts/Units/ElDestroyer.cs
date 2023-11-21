using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class ElDestroyer : MonoBehaviourPunCallbacks
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
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "El Destroyer(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.garraDemoniacaCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            GarraDemoniaca(gm.selectedUnit);
            gm.UpdateStatsPanel();      
        }
    }

    public void GarraDemoniaca(Unit unit)
    {
        if (unit.garraDemoniacaCast == false)
        {
            photonView.RPC("GarraDemoniacaAnimation", RpcTarget.All);
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.garraDemoniacaCast = true;
            unit.actionPoints -= 4;
            unit.UpdateActionPointsText();
            unit.attackDamage += 2;
            unit.garraDemoniacaCast = false;
        }
    }

    [PunRPC]
    public void GarraDemoniacaAnimation()
    {
        Instantiate(fireShield, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}