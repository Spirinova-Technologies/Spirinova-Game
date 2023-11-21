using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class Rimbombante : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;

    public GameObject magicAuraBRunic;

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Rimbombante(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.creacionDeBrujulaCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            CreacionDeBrujula(gm.selectedUnit);   
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "Rimbombante(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.granadaAturdidoraCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            GranadaAturdidora(gm.selectedUnit);   
        }
    }

    public void CreacionDeBrujula(Unit unit)
    {
        if (unit.creacionDeBrujulaCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.creacionDeBrujulaCast = true;
            unit.actionPoints -= 4;
            foreach (Unit units in FindObjectsOfType<Unit>())
            {
                units.photonView.RPC("CreacionDeBrujulaAnimation", RpcTarget.All);
                units.actionPoints += 1;
            }
            unit.creacionDeBrujulaCast = false;
        }
    }

    [PunRPC]
    public void CreacionDeBrujulaAnimation()
    {
        Instantiate(magicAuraBRunic, this.transform.position, Quaternion.identity);
    }

    public void GranadaAturdidora(Unit unit)
    {
        if (unit.granadaAturdidoraCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.granadaAturdidoraCast = true;
            unit.actionPoints -= 4;
            unit.UpdateActionPointsText();
        }
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}