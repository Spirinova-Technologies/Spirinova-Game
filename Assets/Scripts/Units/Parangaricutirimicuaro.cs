using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class Parangaricutirimicuaro : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;

    public GameObject pickupHeart;
    public GameObject fireShield; // Attack Buff Animation
    public GameObject pickupDiamond2; // Paralyze Animation
    public GameObject brokenHeart; // Charm Animation
    public GameObject magicAuraBRunic; // AP Gain Animation

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Parangaricutirimicuaro(Clone)" && gm.selectedUnit.actionPoints >= 6 && gm.selectedUnit.ordenRealCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            OrdenReal(gm.selectedUnit);
            gm.UpdateStatsPanel();      
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "Parangaricutirimicuaro(Clone)" && gm.selectedUnit.actionPoints >= 1 && gm.selectedUnit.sigiloCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Sigilo(gm.selectedUnit);
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

    public void Sigilo(Unit unit)
    {
        if (unit.sigiloCast == false)
        {
            photonView.RPC("SigiloAnimation", RpcTarget.All);
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.sigiloCast = true;
            unit.actionPoints += 1;
            unit.cantMove = true;
            unit.cantAttack = true;
            StartCoroutine(SigiloUncast(gm.selectedUnit, 5f));
        }
    }

    [PunRPC]
    public void SigiloAnimation()
    {
        Instantiate(magicAuraBRunic, this.transform.position, Quaternion.identity);
        Instantiate(pickupDiamond2, this.transform.position, Quaternion.identity);
        Instantiate(brokenHeart, this.transform.position, Quaternion.identity);
    }

    IEnumerator SigiloUncast(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        unit.cantMove = false;
        unit.cantAttack = false;
        unit.sigiloCast = false;
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}