using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class ElInquisidor : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;
    Unit unit;

    public GameObject magicCircleN; // Range Buff Animation

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
        unit = GetComponent<Unit>();
        unit.lanzaGloriosa = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "El Inquisidor(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.manejoDeArmasCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            ManejoDeArmas(gm.selectedUnit);  
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

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}