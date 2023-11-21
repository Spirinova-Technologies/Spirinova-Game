using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class ElGeneralComando : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;

    public GameObject magicCircleN; // Range Buff Animation
    public GameObject fireShield; // Attack Buff Animation

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "El General Comando(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.manejoDeArmasCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            ManejoDeArmas(gm.selectedUnit);  
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "El General Comando(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.bazookaCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Bazooka(gm.selectedUnit);  
        }
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

    public void Bazooka(Unit unit)
    {
        if (unit.bazookaCast == false)
        {
            photonView.RPC("BazookaAnimation", RpcTarget.All);
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.bazookaCast = true;
            unit.actionPoints -= 4;
            unit.maxAttackRange += 3;
            unit.attackDamage += 2;
            gm.UpdateStatsPanel();
            StartCoroutine(BazookaUncast(gm.selectedUnit, 10f));
            gm.UpdateStatsPanel();
        }
    }

    [PunRPC]
    public void BazookaAnimation()
    {
        Instantiate(fireShield, this.transform.position, Quaternion.identity);
        Instantiate(magicCircleN, this.transform.position, Quaternion.identity);
    }

    IEnumerator BazookaUncast(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        unit.maxAttackRange -= 3;
        unit.attackDamage = Mathf.Max(0, unit.attackDamage - 2);
        unit.bazookaCast = false;
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}