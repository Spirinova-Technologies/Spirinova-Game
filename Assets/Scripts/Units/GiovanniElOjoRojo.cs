using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class GiovanniElOjoRojo : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;
    Unit unit;

    public GameObject pickupDiamond2; // Paralyze Animation
    public GameObject brokenHeart; // Charm Animation
    public GameObject magicAuraBRunic; // AP Gain Animation

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
        unit = GetComponent<Unit>();
        unit.letalidad = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "Giovanni, el Ojo Rojo(Clone)" && gm.selectedUnit.actionPoints >= 1 && gm.selectedUnit.sigiloCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Sigilo(gm.selectedUnit);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && gm.selectedUnit != null && gm.selectedUnit.name == "Giovanni, el Ojo Rojo(Clone)" && gm.selectedUnit.actionPoints >= 4 && gm.selectedUnit.disparoParalizadorCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            DisparoParalizador(gm.selectedUnit);
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

    public void DisparoParalizador(Unit unit)
    {
        if (unit.disparoParalizadorCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.disparoParalizadorCast = true;
            unit.actionPoints -= 4;
        }
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}