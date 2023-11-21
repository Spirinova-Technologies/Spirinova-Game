using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class DelphitaLaPinga : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;
    Unit unit;
    List<Unit> enemiesInAOERange = new List<Unit>();

    public GameObject magicAuraBRunic; // AP Gain Animation

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
        unit = GetComponent<Unit>();
        unit.chirrinChinChin = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Delphita, la Pinga(Clone)" && gm.selectedUnit.actionPoints >= 8 && gm.selectedUnit.hacerleComoGatitoCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            HacerleComoGatito(gm.selectedUnit);     
        }
    }

    public void HacerleComoGatito(Unit unit)
    {
        if (unit.hacerleComoGatitoCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            photonView.RPC("HacerleComoGatitoAnimation", RpcTarget.All);
            unit.hacerleComoGatitoCast = true;
            unit.actionPoints -= 8;
            unit.actionPoints = unit.actionPoints * 2;
            enemiesInAOERange.Clear();
            
            foreach (Unit unitInRange in FindObjectsOfType<Unit>())
            {
            if ((Mathf.Abs(transform.position.x - unitInRange.transform.position.x) + Mathf.Abs(transform.position.y - unitInRange.transform.position.y) <= 2))
            {
                if (unit.playerNumber != unitInRange.playerNumber)
                {
                    this.enemiesInAOERange.Add(unitInRange);
                    if (this.enemiesInAOERange.Contains(unitInRange))
                    {
                        gm.selectedUnit.AttackUE(unitInRange, "hacerleComoGatitoEnemy");
                    }
                }
            }
            }
            unit.hacerleComoGatitoCast = false;
        }
    }

    [PunRPC]
    public void HacerleComoGatitoAnimation()
    {
        Instantiate(magicAuraBRunic, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}