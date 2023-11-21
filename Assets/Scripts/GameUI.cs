//4
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class GameUI : MonoBehaviourPun
{
    //public Button endTurnButton; NO TURNS CHECKPOINT
    public TextMeshProUGUI leftPlayerText;
    public TextMeshProUGUI rightPlayerText;
    public TextMeshProUGUI waitingUnitsText;
    public TextMeshProUGUI unitInfoText;
    public TextMeshProUGUI spacebaseInfoText;
    public TextMeshProUGUI winText;
    public AudioSource music;

    // instance
    public static GameUI instance;

    void Awake ()
    {
        instance = this;
        music.mute = PlayerPrefs.GetInt("isMusicMuted", 0)==1;
    }

    void Update(){
        // toggle music mute
        if(Input.GetKeyDown(KeyCode.M)){
            music.mute = !music.mute;
            PlayerPrefs.SetInt("isMusicMuted", music.mute?1:0);
        }
    }


/* //NO TURNS CHECKPOINT
    [PunRPC]
    public void OnEndTurnButton ()
    {
        //GameMaster.instance.photonView.RPC("EndTurn", RpcTarget.All, true);
        //Input.GetKey("space");
        PlayerController.me.EndTurn();
        photonView.RPC("ChangePlayerTextColors", RpcTarget.All);
        Timer.instance.photonView.RPC("ChangeTimer", RpcTarget.All);
        Timer.instance.photonView.RPC("ChangeTimerTextColors", RpcTarget.All);
    }

    public void ToggleEndTurnButton (bool toggle)
    {
        //endTurnButton.interactable = toggle;
        waitingUnitsText.gameObject.SetActive(toggle);
    }
*/ //NO TURNS CHECKPOINT
    public void UpdateWaitingUnitsText (int waitingUnits)
    {
        waitingUnitsText.text = waitingUnits + " Units Waiting";
    }

    public void SetPlayerText (PlayerController player)
    {
        TextMeshProUGUI text = player == GameManager.instance.leftPlayer ? leftPlayerText : rightPlayerText;
        text.text = player.photonPlayer.NickName;
        //leftPlayerText.color = Color.black;
        //Timer.instance.leftTimeText.color = Color.black;
    }

    public void SetUnitInfoText (Unit2 unit)
    {
        unitInfoText.gameObject.SetActive(true);
        unitInfoText.text = "";

        unitInfoText.text += string.Format("<b>HP: </b> {0} / {1}", unit.curHp, unit.maxHp);
        unitInfoText.text += string.Format("\n<b>Armor: </b> {0}", unit.armor);
        unitInfoText.text += string.Format("\n<b>ATK: </b> {0}", unit.damage);
        unitInfoText.text += string.Format("\n<b>Counter: </b> {0}", unit.counter);
        unitInfoText.text += string.Format("\n<b>Range: </b> {0}", unit.maxAttackDistance);
        unitInfoText.text += string.Format("\n<b>Move: </b> {0}", unit.maxMoveDistance);
    }

    public void SetSpacebaseInfoText (Spacebase spacebase)
    {
        spacebaseInfoText.gameObject.SetActive(true);
        spacebaseInfoText.text = "";

        spacebaseInfoText.text += string.Format("<b>HP: </b> {0} / {1}", spacebase.health, spacebase.maxHp);
        spacebaseInfoText.text += string.Format("\n<b>Armor: </b> {0}", spacebase.armor);
        spacebaseInfoText.text += string.Format("\n<b>ATK: </b> {0}", spacebase.damage);
        spacebaseInfoText.text += string.Format("\n<b>Counter: </b> {0}", spacebase.counter);
        spacebaseInfoText.text += string.Format("\n<b>Range: </b> {0}", spacebase.maxAttackDistance);
        spacebaseInfoText.text += string.Format("\n<b>Move: </b> {0}", spacebase.maxMoveDistance);
    }

    public void SetWinText (string winnerName)
    {
        winText.gameObject.SetActive(true);
        winText.text = winnerName + " Wins";
    }

    [PunRPC]
    public void ChangePlayerTextColors()
    {
        if (leftPlayerText.color == Color.black)
        {
            leftPlayerText.color = Color.white;
        } else {
            leftPlayerText.color = Color.black;
        }
        if (rightPlayerText.color == Color.white)
        {
            rightPlayerText.color = Color.black;
        } else {
            rightPlayerText.color = Color.white;
        }
    }
}