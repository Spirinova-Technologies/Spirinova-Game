using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class ElReyJurgentzu : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;
    Unit unit;
    
    public GameObject magicCircleN; // Range Buff Animation

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
        unit = GetComponent<Unit>();
        unit.jineteLegendario = true;
        unit.lanzaDelAmor = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "El Rey Jurgentzu(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.apuntarLanzaCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            ApuntarLanza(gm.selectedUnit);
            gm.UpdateStatsPanel();      
        }
    }

    public void ApuntarLanza(Unit unit)
    {
        if (unit.apuntarLanzaCast == false)
        {
            photonView.RPC("ApuntarLanzaAnimation", RpcTarget.All);
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.apuntarLanzaCast = true;
            unit.actionPoints -= 4;
            unit.UpdateActionPointsText();
            unit.maxAttackRange += 1;
            unit.apuntarLanzaCast = false;
        }
    }

    [PunRPC]
    void ApuntarLanzaAnimation ()
    {   
        Instantiate(magicCircleN, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}