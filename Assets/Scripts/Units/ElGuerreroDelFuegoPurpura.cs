using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class ElGuerreroDelFuegoPurpura : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;
    Unit unit;

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
        unit = GetComponent<Unit>();
        unit.espadaDeFuego = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "El Guerrero del Fuego Purpura(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.espadaDeLaMuerteCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            EspadaDeLaMuerte(gm.selectedUnit);
        }
    }

    public void EspadaDeLaMuerte(Unit unit)
    {
        if (unit.espadaDeLaMuerteCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.espadaDeLaMuerteCast = true;
            unit.actionPoints -= 4;
        }
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}