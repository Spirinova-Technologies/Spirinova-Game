using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class DelphiumElCreadorDelTiempo : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;

    public GameObject magicAuraBRunic; // AP Gain Animation
    public GameObject batsCloud; // Fin del Tiempo Animation
    public GameObject disruptiveForce; // AP Loss Animation

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Delphium, el Creador del Tiempo(Clone)" && gm.selectedUnit.actionPoints >= 6 && gm.selectedUnit.amoDelUniversoCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            AmoDelUniverso(gm.selectedUnit);   
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "Delphium, el Creador del Tiempo(Clone)" && gm.selectedUnit.actionPoints >= 6 && gm.selectedUnit.finDelTiempoCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            FinDelTiempo(gm.selectedUnit);
        }
    }

    public void AmoDelUniverso(Unit unit)
    {
        if (unit.amoDelUniversoCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.amoDelUniversoCast = true;
            unit.actionPoints -= 6;
            foreach (Unit units in FindObjectsOfType<Unit>())
            {
                units.photonView.RPC("AmoDelUniversoAnimation", RpcTarget.All);
                units.actionPoints += 2;
            }
            unit.amoDelUniversoCast = false;
        }
    }

    [PunRPC]
    public void AmoDelUniversoAnimation()
    {
        Instantiate(magicAuraBRunic, this.transform.position, Quaternion.identity);
    }

    public void FinDelTiempo(Unit unit)
    {
        if (unit.finDelTiempoCast == false)
        {
            photonView.RPC("FinDelTiempoAnimation", RpcTarget.All);
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.finDelTiempoCast = true;
            unit.actionPoints -= 6;
            photonView.RPC("FinDelTiempoEnemy", RpcTarget.Others);
        }
    }

    [PunRPC]
    public void FinDelTiempoEnemy()
    {
        foreach (Unit units in FindObjectsOfType<Unit>())
        {
            units.photonView.RPC("FinDelTiempoEnemyAnimation", RpcTarget.All);
            units.actionPoints = 0;
        }
    }

    [PunRPC]
    public void FinDelTiempoAnimation()
    {
        Instantiate(batsCloud, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    public void FinDelTiempoEnemyAnimation()
    {
        Instantiate(disruptiveForce, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    public void VictoryAnim()
    {
        character.Animator.SetTrigger("Victory Trigger");
    }
}