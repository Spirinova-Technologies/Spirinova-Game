//13
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEditor;
using TMPro;

public class GameMaster : MonoBehaviourPun
{
    public Unit selectedUnit;
    public Spacebase selectedSpacebase;

    //public int playerTurn = 1; NO TURNS CHECKPOINT

    public GameObject selectedUnitSquare;

    public GameObject statsPanel;
    public Vector2 statsPanelShift;
    public Unit viewedUnit;
    public Spacebase viewedSpacebase;

    public Text healthText;
    public Text attackDamageText;
    public Text defenseDamageText;
    public Text physicalArmorText;
    public Text hollyArmorText;
    public Text demonArmorText;
    public Text minAttackRangeText;
    public Text maxAttackRangeText;

    public GameObject abilitiesPanel;
    public TextMeshProUGUI abilitiesText;

    public static GameMaster gm;

    public static GameMaster instance;

    void Awake ()
    {
        instance = this;
    }

    public void ToggleStatsPanel(Unit unit)
    {
        if (unit.Equals(viewedUnit) == false)
        {
            statsPanel.SetActive(true);
            statsPanel.transform.position = (Vector2)unit.transform.position + statsPanelShift;
            viewedUnit = unit;
            UpdateStatsPanel();
        } else {
            statsPanel.SetActive(false);
            viewedUnit = null;
        }
    }

    public void ToggleSpacebaseStatsPanel(Spacebase spacebase)
    {
        if (spacebase.Equals(viewedSpacebase) == false)
        {
            statsPanel.SetActive(true);
            statsPanel.transform.position = (Vector2)spacebase.transform.position + statsPanelShift;
            viewedSpacebase = spacebase;
            UpdateBaseStatsPanel();
        } else {
            statsPanel.SetActive(false);
            viewedSpacebase = null;
        }
    }

    public void UpdateStatsPanel()
    {
        if (viewedUnit != null)
        {
            // healthText.text = viewedUnit.health.ToString();
            healthText.text = viewedUnit.GetComponent<Unit2>().curHp.ToString();
            attackDamageText.text = viewedUnit.attackDamage.ToString();
            defenseDamageText.text = viewedUnit.defenseDamage.ToString();
            physicalArmorText.text = viewedUnit.physicalArmor.ToString();
            hollyArmorText.text = viewedUnit.hollyArmor.ToString();
            demonArmorText.text = viewedUnit.demonArmor.ToString();
            minAttackRangeText.text = viewedUnit.minAttackRange.ToString();
            maxAttackRangeText.text = viewedUnit.maxAttackRange.ToString();

            abilitiesText.text = viewedUnit.abilitiesDescription;
        }
    }

    public void UpdateBaseStatsPanel()
    {
        if (viewedSpacebase != null)
        {
            healthText.text = viewedSpacebase.health.ToString();
            attackDamageText.text = viewedSpacebase.attackDamage.ToString();
            defenseDamageText.text = viewedSpacebase.defenseDamage.ToString();
            physicalArmorText.text = viewedSpacebase.physicalArmor.ToString();
            hollyArmorText.text = viewedSpacebase.hollyArmor.ToString();
            demonArmorText.text = viewedSpacebase.demonArmor.ToString();
            minAttackRangeText.text = viewedSpacebase.minAttackRange.ToString();
            maxAttackRangeText.text = viewedSpacebase.maxAttackRange.ToString();

            abilitiesText.text = viewedSpacebase.abilitiesDescription;
        }
    }

    public void MoveStatsPanel(Unit unit)
    {
        if (unit.Equals(viewedUnit))
        {
            statsPanel.transform.position = (Vector2)unit.transform.position + statsPanelShift;
        }
    }

    public void MoveSpacebaseStatsPanel(Spacebase spacebase)
    {
        if (spacebase.Equals(viewedSpacebase))
        {
            statsPanel.transform.position = (Vector2)spacebase.transform.position + statsPanelShift;
        }
    }

    public void RemoveStatsPanel(Unit unit)
    {
        if (unit.Equals(viewedUnit))
        {
            statsPanel.SetActive(false);
            viewedUnit = null;
        }
    }

    public void RemoveSpacebaseStatsPanel(Spacebase spacebase)
    {
        if (spacebase.Equals(viewedSpacebase))
        {
            statsPanel.SetActive(false);
            viewedSpacebase = null;
        }
    }

    public void ResetTiles()
    {
        foreach (Tile tile in FindObjectsOfType<Tile>())
        {
            tile.Reset();
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            abilitiesPanel.SetActive(true);
        }
        if(Input.GetKeyUp(KeyCode.LeftShift)){
            abilitiesPanel.SetActive(false);
        }
/* NO TURNS CHECKPOINT
        if (Input.GetKeyDown(KeyCode.Space) && GameManager.instance.curPlayer == PlayerController.me)
        {
            photonView.RPC("EndTurn", RpcTarget.All);
            GameUI.instance.OnEndTurnButton();
        }

        if (Input.GetKeyDown(KeyCode.K) && GameManager.instance.curPlayer == PlayerController.me)
        {
            if (selectedUnit.isKing == true)
            {
                selectedUnit.victoryPanel.SetActive(true);
            }
            
            Instantiate(selectedUnit.deathEffect, selectedUnit.transform.position, Quaternion.identity);
            ResetTiles();
            RemoveStatsPanel(selectedUnit);
            Destroy(selectedUnit);
        }
*/ //NO TURNS CHECKPOINT
        /*
        if (Input.GetKeyDown(KeyCode.Alpha1) && GameManager.instance.curPlayer == PlayerController.me)
        {
            if (selectedUnit.name == "1. Alianza Delphi(Clone)" && selectedUnit.hasAttacked == false)
            {
                Debug.Log("CastMotivation");
                CastMotivation(selectedUnit);
                selectedUnit.hasAttacked = true;
            }
        }
        */

        if (selectedUnit != null)
        {
            selectedUnitSquare.SetActive(true);
            selectedUnitSquare.transform.position = selectedUnit.transform.position;
        } else
        {
            selectedUnitSquare.SetActive(false);
        }
    }

/* //NO TURNS CHECKPOINT
    [PunRPC]
    public void EndTurn()
    {
        if (playerTurn == 1)
        {
            playerTurn = 2;
        } else if (playerTurn == 2)
        {
            playerTurn = 1;
        }

        if (selectedUnit != null)
        {
            selectedUnit.selected = false;
            selectedUnit = null;
        }

        ResetTiles();

        foreach (Unit unit in FindObjectsOfType<Unit>())
        {
            unit.hasMoved = false;
            unit.weaponIcon.SetActive(false);
            unit.hasAttacked = false;
        }
    }

    [PunRPC]
    void ChangeGameMasterInstancePlayerTurn()
    {
        if (playerTurn == 1)
        {
        playerTurn = 2;
        } else if (playerTurn == 2)
        {
        playerTurn = 1;
        }
    }
*/ //NO TURNS CHECKPOINT
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

/*
    void CastMotivation(Unit unit)
    {
        unit.attackDamage += 1;
    }
*/
}
