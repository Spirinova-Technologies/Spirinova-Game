using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class ElGuerreroDeLaLunaRoja : MonoBehaviourPunCallbacks
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
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "El Guerrero de la Luna Roja(Clone)" && gm.selectedUnit.actionPoints >= 2 && gm.selectedUnit.espadaDelDemonioCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            EspadaDelDemonio(gm.selectedUnit);
        }
    }

    public void EspadaDelDemonio(Unit unit)
    {
        if (unit.espadaDelDemonioCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.espadaDelDemonioCast = true;
            unit.actionPoints -= 2;
        }
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}