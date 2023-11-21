using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class LaLumbre : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;

    public GameObject magicAuraBRunic; // AP Gain Animation
    public GameObject brokenHeart; // Charm Animation

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "La Lumbre(Clone)" && gm.selectedUnit.actionPoints >= 2 && gm.selectedUnit.exploracionCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Exploracion(gm.selectedUnit);   
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "La Lumbre(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.disparoExplosivoCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            DisparoExplosivo(gm.selectedUnit);   
        }
    }

    public void Exploracion(Unit unit)
    {
        if (unit.exploracionCast == false)
        {
            photonView.RPC("ExploracionAnimation", RpcTarget.All);
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.exploracionCast = true;
            unit.actionPoints -= 2;
            unit.actionPoints = unit.actionPoints * 2;
            unit.cantAttack = true;
            StartCoroutine(ExploracionUncast(gm.selectedUnit, 10f));
        }
    }

    [PunRPC]
    public void ExploracionAnimation()
    {
        Instantiate(magicAuraBRunic, this.transform.position, Quaternion.identity);
        Instantiate(brokenHeart, this.transform.position, Quaternion.identity);
    }

    IEnumerator ExploracionUncast(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        unit.cantAttack = false;
        unit.exploracionCast = false;
    }

    public void DisparoExplosivo(Unit unit)
    {
        if (unit.disparoExplosivoCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.disparoExplosivoCast = true;
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