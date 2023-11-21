using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class LaQuimeraDelphi : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;
    List<Unit> enemiesInAOERange = new List<Unit>();

    public GameObject pickupDiamond2; // Paralyze Animation
    public GameObject brokenHeart; // Charm Animation
    public GameObject fireShield; // Attack Buff Animation
    public GameObject pickupHeart;

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "La Quimera Delphi(Clone)" && gm.selectedUnit.actionPoints >= 8 && gm.selectedUnit.karmaCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Karma(gm.selectedUnit);
            gm.UpdateStatsPanel();      
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "La Quimera Delphi(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.ataqueSonicoCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            AtaqueSonico(gm.selectedUnit);     
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3) && gm.selectedUnit != null && gm.selectedUnit.name == "La Quimera Delphi(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.meditacionCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Meditacion(gm.selectedUnit);
            gm.UpdateStatsPanel();      
        }
    }

    public void Karma(Unit unit)
    {
        if (unit.karmaCast == false)
        {
            photonView.RPC("KarmaAnimation", RpcTarget.All);
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.karmaCast = true;
            unit.actionPoints -= 8;
            unit.UpdateActionPointsText();
            Unit2 unit2 = GetComponent<Unit2>();
            unit.attackDamage += (unit2.maxHp - unit2.curHp);
            unit.cantMove = true;
            unit.cantAttack = true;
            StartCoroutine(KarmaUncast(gm.selectedUnit, 5f));
        }
    }

    [PunRPC]
    public void KarmaAnimation()
    {
        Instantiate(fireShield, this.transform.position, Quaternion.identity);
        Instantiate(pickupDiamond2, this.transform.position, Quaternion.identity);
        Instantiate(brokenHeart, this.transform.position, Quaternion.identity);
    }

    IEnumerator KarmaUncast(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        unit.cantMove = false;
        unit.cantAttack = false;
        unit.karmaCast = false;
    }

    public void AtaqueSonico(Unit unit)
    {
        if (unit.ataqueSonicoCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.ataqueSonicoCast = true;
            unit.actionPoints -= 4;
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
                        gm.selectedUnit.AttackUE(unitInRange, "ataque sonico");
                    }
                }
            }
            }
            unit.ataqueSonicoCast = false;
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