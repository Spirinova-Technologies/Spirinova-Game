using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class AlianzaDelphi : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;

    public GameObject fireShield; // Attack Buff Animation
    public GameObject pickupHeart; // Armor Buff
    public GameObject magicCircleN; // Range Buff Animation

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Alianza Delphi(Clone)" && gm.selectedUnit.actionPoints >= 2 && gm.selectedUnit.motivacionCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Motivacion(gm.selectedUnit);
            gm.UpdateStatsPanel();      
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "Alianza Delphi(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.posicionDeDefensaCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            PosicionDeDefensa(gm.selectedUnit);
            gm.UpdateStatsPanel();      
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && gm.selectedUnit != null && gm.selectedUnit.name == "Alianza Delphi(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.manejoDeArmasCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            ManejoDeArmas(gm.selectedUnit);  
        }
    }

    public void Motivacion(Unit unit)
    {
        if (unit.motivacionCast == false)
        {
            photonView.RPC("MotivacionAnimation", RpcTarget.All);
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.motivacionCast = true;
            unit.actionPoints -= 2;
            unit.UpdateActionPointsText();
            unit.attackDamage += 1;
            unit.motivacionCast = false;
        }
    }

    [PunRPC]
    public void MotivacionAnimation()
    {
        Instantiate(fireShield, this.transform.position, Quaternion.identity);
    }

    public void PosicionDeDefensa(Unit unit)
    {
        if (unit.posicionDeDefensaCast == false)
        {
            photonView.RPC("PosicionDeDefensaAnimation", RpcTarget.All);
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.posicionDeDefensaCast = true;
            unit.actionPoints -= 4;
            unit.UpdateActionPointsText();
            unit.physicalArmor += 1;
            unit.defenseDamage += 1;
            unit.posicionDeDefensaCast = false;
        }
    }

    [PunRPC]
    void PosicionDeDefensaAnimation ()
    {   
        Instantiate(pickupHeart, this.transform.position, Quaternion.identity);
    }

    public void ManejoDeArmas(Unit unit)
    {
        if (unit.manejoDeArmasCast == false)
        {
            photonView.RPC("ManejoDeArmasAnimation", RpcTarget.All);
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.manejoDeArmasCast = true;
            unit.actionPoints -= 4;
            unit.UpdateActionPointsText();
            unit.maxAttackRange += 1;
            unit.manejoDeArmasCast = false;
            gm.UpdateStatsPanel();
            StartCoroutine(ManejoDeArmasUncast(gm.selectedUnit, 15f));
            gm.UpdateStatsPanel();
        }
    }

    [PunRPC]
    void ManejoDeArmasAnimation ()
    {   
        Instantiate(magicCircleN, this.transform.position, Quaternion.identity);
    }

    IEnumerator ManejoDeArmasUncast(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        unit.maxAttackRange -= 1;
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}