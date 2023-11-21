using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class ElSoldaditoMojoncio : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;

    public GameObject magicCircleN; // Range Buff Animation

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "El Soldadito Mojoncio(Clone)" && gm.selectedUnit.actionPoints >= 1 && gm.selectedUnit.disparoRapidoCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            DisparoRapido(gm.selectedUnit);   
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "El Soldadito Mojoncio(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.manejoDeArmasCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            ManejoDeArmas(gm.selectedUnit);  
        }
    }

    public void DisparoRapido(Unit unit)
    {
        if (unit.disparoRapidoCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.disparoRapidoCast = true;
            unit.actionPoints -= 1;
            unit.UpdateActionPointsText();
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
