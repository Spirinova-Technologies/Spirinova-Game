using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Timer : MonoBehaviourPun
{
    public float leftTimeRemaining;
    public float rightTimeRemaining;
    public bool leftTimerIsRunning = false;
    public bool rightTimerIsRunning = false;
    public Text leftTimeText;
    public Text rightTimeText;

    // instance
    public static Timer instance;

    void Awake ()
    {
        instance = this;
    }
    
    private void Start()
    {
        // Starts the timer automatically
        leftTimerIsRunning = true;
    }
    
    void Update()
    {
        if (leftTimerIsRunning)
        {
            if (leftTimeRemaining > 0)
            {
            leftTimeRemaining -= Time.deltaTime;
            leftDisplayTime(leftTimeRemaining);
            } else
            {
                GameManager.instance.photonView.RPC("WinGame", RpcTarget.All, PlayerController.enemy == GameManager.instance.leftPlayer ? 0 : 1);
                leftTimeRemaining = 0;
                leftTimerIsRunning = false;
            }
        }

        if (rightTimerIsRunning)
        {
            if (rightTimeRemaining > 0)
            {
            rightTimeRemaining -= Time.deltaTime;
            rightDisplayTime(rightTimeRemaining);
            } else
            {
                //GameManager.instance.photonView.RPC("WinGame", RpcTarget.All, PlayerController.enemy == GameManager.instance.leftPlayer ? 0 : 1);
                GameManager.instance.photonView.RPC("WinGame", RpcTarget.All, PlayerController.enemy == GameManager.instance.rightPlayer ? 0 : 1);
                rightTimeRemaining = 0;
                rightTimerIsRunning = false;
            }
        }
    }

    void leftDisplayTime(float leftTimeToDisplay)
    {
        leftTimeToDisplay += 1;
        
        float minutes = Mathf.FloorToInt(leftTimeToDisplay / 60);
        float seconds = Mathf.FloorToInt(leftTimeToDisplay % 60);

        leftTimeText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    void rightDisplayTime(float rightTimeToDisplay)
    {
        rightTimeToDisplay += 1;
        
        float minutes = Mathf.FloorToInt(rightTimeToDisplay / 60);
        float seconds = Mathf.FloorToInt(rightTimeToDisplay % 60);

        rightTimeText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    [PunRPC]
    public void ChangeTimer()
    {
        if (leftTimerIsRunning == true)
        {
            leftTimerIsRunning = false;
        } else {
            leftTimerIsRunning = true;
        }
        if (rightTimerIsRunning == false)
        {
            rightTimerIsRunning = true;
        } else {
            rightTimerIsRunning = false;
        }
    }

    [PunRPC]
    public void ChangeTimerTextColors()
    {
        if (leftTimeText.color == Color.black)
        {
            leftTimeText.color = Color.white;
        } else {
            leftTimeText.color = Color.black;
        }
        if (rightTimeText.color == Color.white)
        {
            rightTimeText.color = Color.black;
        } else {
            rightTimeText.color = Color.white;
        }
    }
}
