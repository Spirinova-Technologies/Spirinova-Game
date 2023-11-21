using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class Tutankabron : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;

    public GameObject fireShield; // Attack Buff Animation

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Tutankabron(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.furiaInfernalCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            FuriaInfernal(gm.selectedUnit);
            gm.UpdateStatsPanel();      
        }
    }

    public void FuriaInfernal(Unit unit)
    {
        if (unit.furiaInfernalCast == false)
        {
            photonView.RPC("FuriaInfernalAnimation", RpcTarget.All);
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.furiaInfernalCast = true;
            unit.actionPoints -= 4;
            unit.UpdateActionPointsText();
            unit.attackDamage += 3;
            unit.furiaInfernalCast = false;
        }
    }

    [PunRPC]
    public void FuriaInfernalAnimation()
    {
        Instantiate(fireShield, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}