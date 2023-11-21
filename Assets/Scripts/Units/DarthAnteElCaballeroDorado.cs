using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class DarthAnteElCaballeroDorado : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;

    public GameObject pickupDiamond2; // Paralyze Animation
    public GameObject brokenHeart; // Charm Animation
    public GameObject fireShield;
    public GameObject pickupHeart;

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Darth Ante, el Caballero Dorado(Clone)" && gm.selectedUnit.actionPoints >= 6 && gm.selectedUnit.conquistaCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Conquista(gm.selectedUnit);
            gm.UpdateStatsPanel();      
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "Darth Ante, el Caballero Dorado(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.meditacionCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Meditacion(gm.selectedUnit);
            gm.UpdateStatsPanel();      
        }
    }

    public void Conquista(Unit unit)
    {
        if (unit.conquistaCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.conquistaCast = true;
            unit.actionPoints -= 6;
            foreach (Unit units in FindObjectsOfType<Unit>())
            {
                units.photonView.RPC("ConquistaAnimation", RpcTarget.All);
                units.attackDamage += 1;
                units.physicalArmor += 1;
                units.defenseDamage += 1;
            }
            gm.UpdateStatsPanel();
            StartCoroutine(ConquistaUncast(gm.selectedUnit, 10f));
            gm.UpdateStatsPanel();
            unit.conquistaCast = false;
        }
    }

    [PunRPC]
    void ConquistaAnimation ()
    {   
        Instantiate(fireShield, this.transform.position, Quaternion.identity);
        Instantiate(pickupHeart, this.transform.position, Quaternion.identity);
    }

    IEnumerator ConquistaUncast(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        foreach (Unit units in FindObjectsOfType<Unit>())
            {
                units.attackDamage = Mathf.Max(0, units.attackDamage - 1);
                units.physicalArmor = Mathf.Max(0, units.physicalArmor  - 1);
                units.defenseDamage = Mathf.Max(0, units.defenseDamage - 1);
            }
    }

    public void Meditacion(Unit unit)
    {
        if (unit.meditacionCast == false)
        {
            photonView.RPC("MeditacionAnimation", RpcTarget.All);
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.meditacionCast = true;
            unit.actionPoints -= 4;
            unit.UpdateActionPointsText();
            unit.attackDamage += 1;
            unit.physicalArmor += 1;
            unit.defenseDamage += 1;
            unit.cantMove = true;
            unit.cantAttack = true;
            StartCoroutine(MeditacionUncast(gm.selectedUnit, 5f));
        }
    }

    [PunRPC]
    public void MeditacionAnimation()
    {
        Instantiate(fireShield, this.transform.position, Quaternion.identity);
        Instantiate(pickupHeart, this.transform.position, Quaternion.identity);
        Instantiate(pickupDiamond2, this.transform.position, Quaternion.identity);
        Instantiate(brokenHeart, this.transform.position, Quaternion.identity);
    }

    IEnumerator MeditacionUncast(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        unit.cantMove = false;
        unit.cantAttack = false;
        unit.meditacionCast = false;
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}