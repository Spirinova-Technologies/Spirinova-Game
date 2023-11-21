using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class ElSilenciador : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;

    public GameObject pickupDiamond2; // Paralyze Animation
    public GameObject brokenHeart; // Charm Animation
    public GameObject magicAuraBRunic; // AP Gain Animation

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "El Silenciador(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.ahorcamientoCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Ahorcamiento(gm.selectedUnit);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "El Silenciador(Clone)" && gm.selectedUnit.actionPoints >= 1 && gm.selectedUnit.sigiloCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Sigilo(gm.selectedUnit);
        }
    }

    public void Ahorcamiento(Unit unit)
    {
        if (unit.ahorcamientoCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.ahorcamientoCast = true;
            unit.actionPoints -= 4;
        }
    }

    public void Sigilo(Unit unit)
    {
        if (unit.sigiloCast == false)
        {
            photonView.RPC("SigiloAnimation", RpcTarget.All);
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.sigiloCast = true;
            unit.actionPoints += 1;
            unit.cantMove = true;
            unit.cantAttack = true;
            StartCoroutine(SigiloUncast(gm.selectedUnit, 5f));
        }
    }

    [PunRPC]
    public void SigiloAnimation()
    {
        Instantiate(magicAuraBRunic, this.transform.position, Quaternion.identity);
        Instantiate(pickupDiamond2, this.transform.position, Quaternion.identity);
        Instantiate(brokenHeart, this.transform.position, Quaternion.identity);
    }

    IEnumerator SigiloUncast(Unit unit, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        unit.cantMove = false;
        unit.cantAttack = false;
        unit.sigiloCast = false;
    }
}