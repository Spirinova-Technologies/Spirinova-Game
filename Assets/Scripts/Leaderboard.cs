using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    // instance
    public static Leaderboard leaderboard;

    public GameObject rowPrefab;
    public Transform rowsParent;

    void Awake ()
    {
        leaderboard = this;
    }
    
    public void SendLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "Season",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successfull leaderboard sent");
    }
    
    void OnError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }

    public void GetLeaderboard()
    {
        //Leaderboard.SetActive(true);
        //leaderboard.SetActive(true);
        //Leaderboard.instance.SetActive(true);
        //leaderboard.gameObject.SetActive(true);
        //Leaderboard.gameObject.SetActive(true);
        //leaderboard.gameObject.SetActive(true);
        
        var request = new GetLeaderboardRequest
        {
            StatisticName = "Season",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
        leaderboard.gameObject.SetActive(true);
    }

    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        foreach (Transform item in rowsParent)
        {
            Destroy(item.gameObject);
        }
        
        foreach (var item in result.Leaderboard)
        {
            GameObject newGo = Instantiate(rowPrefab, rowsParent);
            Text[] texts = newGo.GetComponentsInChildren<Text>();
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.PlayFabId;
            texts[2].text = item.StatValue.ToString();
            
            Debug.Log(string.Format("Ranking: {0} | Name: {1} | Score: {2}",
                item.Position, item.PlayFabId, item.StatValue));
        }
    }
}
