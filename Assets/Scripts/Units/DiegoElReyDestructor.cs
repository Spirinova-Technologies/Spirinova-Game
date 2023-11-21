using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class DiegoElReyDestructor : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;
    Unit unit;

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
        unit = GetComponent<Unit>();
        unit.lanzaInfernal = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Diego, el Rey Destructor(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.degollarCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Degollar(gm.selectedUnit);   
        }
    }

    public void Degollar(Unit unit)
    {
        if (unit.degollarCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.degollarCast = true;
            unit.actionPoints -= 4;
            unit.UpdateActionPointsText();
        }
    }
}