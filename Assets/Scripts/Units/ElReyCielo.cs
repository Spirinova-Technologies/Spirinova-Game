using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class ElReyCielo : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;
    Unit unit;

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
        unit = GetComponent<Unit>();
        unit.gloriaEterna = true;
        unit.lanzaInfernal = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}