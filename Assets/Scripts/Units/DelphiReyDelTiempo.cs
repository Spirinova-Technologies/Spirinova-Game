using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Assets.HeroEditor.Common.CharacterScripts;

public class DelphiReyDelTiempo : MonoBehaviourPunCallbacks
{
    GameMaster gm;

    public Character character;

    public GameObject magicCircleO;
    public GameObject magicAuraBRunic;
    public GameObject fireShield;
    public GameObject batsCloud; // Fin del Tiempo Animation
    public GameObject disruptiveForce;

    void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && gm.selectedUnit != null && gm.selectedUnit.name == "Delphi, Rey del Tiempo(Clone)" && gm.selectedUnit.actionPoints >= 6 && gm.selectedUnit.liberacionCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            Liberacion(gm.selectedUnit);   
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && gm.selectedUnit != null && gm.selectedUnit.name == "Delphi, Rey del Tiempo(Clone)" && gm.selectedUnit.actionPoints >= 6 && gm.selectedUnit.declaracionDeGuerraCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            DeclaracionDeGuerra(gm.selectedUnit);
            gm.UpdateStatsPanel();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && gm.selectedUnit != null && gm.selectedUnit.name == "Delphi, Rey del Tiempo(Clone)" && gm.selectedUnit.actionPoints >= 6 && gm.selectedUnit.finDelTiempoCast == false && gm.selectedUnit.transform.position == this.transform.position)
        {
            FinDelTiempo(gm.selectedUnit);
        }
    }

    public void Liberacion(Unit unit)
    {
        if (unit.liberacionCast == false)
        {
            photonView.RPC("LiberacionAnimation", RpcTarget.All);
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.liberacionCast = true;
            unit.actionPoints -= 6;
            foreach (Unit units in FindObjectsOfType<Unit>())
            {
                units.photonView.RPC("LiberacionBuffAnimation", RpcTarget.All);
                units.actionPoints = units.actionPoints * 2;
            }
        }
    }

    [PunRPC]
    public void LiberacionAnimation()
    {
        Instantiate(magicCircleO, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    public void LiberacionBuffAnimation()
    {
        Instantiate(magicAuraBRunic, this.transform.position, Quaternion.identity);
    }

    public void DeclaracionDeGuerra(Unit unit)
    {
        if (unit.declaracionDeGuerraCast == false)
        {
            photonView.RPC("VictoryAnim", RpcTarget.All);
            unit.declaracionDeGuerraCast = true;
            unit.actionPoints -= 6;
            foreach (Unit units in FindObjectsOfType<Unit>())
            {
                units.photonView.RPC("DeclaracionDeGuerraAnimation", RpcTarget.All);
                units.attackDamage += 1;
            }
            photonView.RPC("DeclaracionDeGuerraEnemy", RpcTarget.Others);
            unit.declaracionDeGuerraCast = false;
        }
    }

    [PunRPC]
    public void DeclaracionDeGuerraAnimation()
    {
        Instantiate(fireShield, this.transform.position, Quaternion.identity);
    }

    [PunRPC]
    public void DeclaracionDeGuerraEnemy()
    {
        foreach (Unit units in FindObjectsOfType<Unit>())
        {
            units.photonView.RPC("DeclaracionDeGuerraEnemyAnimation", RpcTarget.All);
            units.actionPoints -= 2;
        }
    }

    [PunRPC]
    public void DeclaracionDeGuerraEnemyAnimation()
    {
        Instantiate(disruptiveForce, this.transform.position, Quaternion.identity);
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