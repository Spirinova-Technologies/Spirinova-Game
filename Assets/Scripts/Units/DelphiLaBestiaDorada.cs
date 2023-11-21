using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class DelphiLaBestiaDorada : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;
    Unit unit;

    public GameObject fireShield; // Attack Buff Animation
    public GameObject pickupHeart;

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
        unit = GetComponent<Unit>();
        unit.espadaDeFuego = true;
        unit.gloriaEterna = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Delphi, la Bestia Dorada(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.gritoDelDragonCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            GritoDelDragon(gm.selectedUnit);
            gm.UpdateStatsPanel();      
        }
    }

    public void GritoDelDragon(Unit unit)
    {
        if (unit.gritoDelDragonCast == false)
        {
            photonView.RPC("GritoDelDragonAnimation", RpcTarget.All);
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.gritoDelDragonCast = true;
            unit.actionPoints -= 4;
            unit.UpdateActionPointsText();
            unit.attackDamage += 1;
            unit.physicalArmor += 1;
            unit.defenseDamage += 1;
            unit.gritoDelDragonCast = false;
        }
    }

    [PunRPC]
    public void GritoDelDragonAnimation()
    {
        Instantiate(fireShield, this.transform.position, Quaternion.identity);
        Instantiate(pickupHeart, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}