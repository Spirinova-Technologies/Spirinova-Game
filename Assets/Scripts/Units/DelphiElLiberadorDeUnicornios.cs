using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class DelphiElLiberadorDeUnicornios : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;

    public GameObject fireShield; // Attack Buff Animation
    public GameObject magicAuraDRunic; // Armor Debuff Animation
    public GameObject pickupHeart;

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Delphi, el Liberador de Unicornios(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.gritoDelDragonCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            GritoDelDragon(gm.selectedUnit);
            gm.UpdateStatsPanel();      
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "Delphi, el Liberador de Unicornios(Clone)" && gm.selectedUnit.actionPoints >= 6 && gm.selectedUnit.dragonSagradoCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            DragonSagrado(gm.selectedUnit);
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

    public void DragonSagrado(Unit unit)
    {
        if (unit.dragonSagradoCast == false)
        {
            photonView.RPC("DragonSagradoAnimation", RpcTarget.All);
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.dragonSagradoCast = true;
            unit.actionPoints -= 6;
            unit.UpdateActionPointsText();
            unit.attackDamage = (int)(Mathf.Round(unit.attackDamage * 1.5f));
            unit.physicalArmor = (int)(Mathf.Round(unit.physicalArmor / 2));
        }
    }

    [PunRPC]
    public void DragonSagradoAnimation()
    {
        Instantiate(fireShield, this.transform.position, Quaternion.identity);
        Instantiate(magicAuraDRunic, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}