using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class ElPatriota : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;
    Unit unit;

    public GameObject pickupHeart;

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
        unit = GetComponent<Unit>();
        unit.lanzaDelAmor = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "El Patriota(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.resistenciaCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Resistencia(gm.selectedUnit);
        }
    }
    
    public void Resistencia(Unit unit)
    {
        if (unit.resistenciaCast == false)
        {
            photonView.RPC("ResistenciaAnimation", RpcTarget.All);
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.resistenciaCast = true;
            unit.actionPoints -= 4;
            unit.physicalArmor += 1;
            unit.resistenciaCast = false;
        }
    }
    
    [PunRPC]
    public void ResistenciaAnimation()
    {
        Instantiate(pickupHeart, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}