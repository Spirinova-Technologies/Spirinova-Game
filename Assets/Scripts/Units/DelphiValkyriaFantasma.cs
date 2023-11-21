using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class DelphiValkyriaFantasma : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;
    Unit unit;

    public GameObject fireShield;
    public GameObject pickupHeart;
    public GameObject magicAuraBRunic; // AP Gain Animation
    public GameObject brokenHeart; // Charm Animation

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
        unit = GetComponent<Unit>();
        unit.golpeLetal = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Delphi, Valkyria Fantasma(Clone)" && gm.selectedUnit.actionPoints >= 6 && gm.selectedUnit.ordenRealCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            OrdenReal(gm.selectedUnit);
            gm.UpdateStatsPanel();      
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "Delphi, Valkyria Fantasma(Clone)" && gm.selectedUnit.actionPoints >= 2 && gm.selectedUnit.exploracionCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Exploracion(gm.selectedUnit);   
        }
    }

    public void OrdenReal(Unit unit)
    {
        if (unit.ordenRealCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.ordenRealCast = true;
            unit.actionPoints -= 6;
            foreach (Unit units in FindObjectsOfType<Unit>())
            {
                units.photonView.RPC("OrdenRealAnimation", RpcTarget.All);
                units.attackDamage += 1;
                units.physicalArmor += 1;
                units.defenseDamage += 1;
            }
            gm.UpdateStatsPanel();
            unit.ordenRealCast = false;
        }
    }

    [PunRPC]
    public void OrdenRealAnimation()
    {
        Instantiate(fireShield, this.transform.position, Quaternion.identity);
        Instantiate(pickupHeart, this.transform.position, Quaternion.identity);
    }

    public void Exploracion(Unit unit)
    {
        if (unit.exploracionCast == false)
        {
            photonView.RPC("ExploracionAnimation", RpcTarget.All);
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.exploracionCast = true;
            unit.actionPoints -= 2;
            unit.actionPoints = unit.actionPoints * 2;
            unit.cantAttack = true;
            StartCoroutine(ExploracionUncast(gm.selectedUnit, 10f));
        }
    }

    [PunRPC]
    public void ExploracionAnimation()
    {
        Instantiate(magicAuraBRunic, this.transform.position, Quaternion.identity);
        Instantiate(brokenHeart, this.transform.position, Quaternion.identity);
    }

    IEnumerator ExploracionUncast(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        unit.cantAttack = false;
        unit.exploracionCast = false;
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}