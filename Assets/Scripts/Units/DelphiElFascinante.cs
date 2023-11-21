using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class DelphiElFascinante : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;

    public GameObject magicCircleF; // Furia Eterna Animation
    public GameObject pickupDiamond2; // Paralyze Animation

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Delphi, el Fascinante(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.trifuerzaCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Trifuerza(gm.selectedUnit);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "Delphi, el Fascinante(Clone)" && gm.selectedUnit.actionPoints >= 6 && gm.selectedUnit.furiaEternaCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            FuriaEterna(gm.selectedUnit);
        }
    }

    public void Trifuerza(Unit unit)
    {
        if (unit.trifuerzaCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.trifuerzaCast = true;
            unit.actionPoints -= 4;
        }
    }

    public void FuriaEterna(Unit unit)
    {
        if (unit.furiaEternaCast == false)
        {
            photonView.RPC("FuriaEternaAnimation", RpcTarget.All);
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.furiaEternaCast = true;
            unit.actionPoints -= 6;
            photonView.RPC("FuriaEternaEnemy", RpcTarget.Others);
            unit.furiaEternaCast = false;
        }
    }

    [PunRPC]
    public void FuriaEternaEnemy()
    {
        foreach (Unit units in FindObjectsOfType<Unit>())
        {
            units.photonView.RPC("FuriaEternaDebuffAnimation", RpcTarget.All);
            units.cantMove = true;
            units.StartCoroutine(FuriaEternaUncast(units, 10f));
        }
    }

    IEnumerator FuriaEternaUncast(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        unit.cantMove = false;
    }

    [PunRPC]
    public void FuriaEternaAnimation()
    {
        Instantiate(magicCircleF, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    public void FuriaEternaDebuffAnimation()
    {
        Instantiate(pickupDiamond2, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}