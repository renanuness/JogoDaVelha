using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class MP_Lobby : NetworkLobbyManager
{

    private void Start()
    {
        MMStart();
        MMListMatches();
    }

    private void MMStart()
    {
        Debug.Log("@ MMStart");

        this.StartMatchMaker();
    }

    private void MMListMatches()
    {
        Debug.Log("@ MMListMatches");

        this.matchMaker.ListMatches(0, 20, "", true, 0, 0, OnMatchList);
    }

    public override void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
    {
        Debug.Log("@ OnMatchList");
        base.OnMatchList(success, extendedInfo, matchList);

        if (!success)
            Debug.Log("List failed:" + extendedInfo);
        else
        {
            if(matchList.Count > 0)
            {
                Debug.Log("Successfully listed matchs. 1st match:" + matchList[0]);
                MMJoinMatch(matchList[0]);
            }
            else
                MMCreateMatch();
        }
    }

    private void MMJoinMatch(MatchInfoSnapshot firstMatch)
    {
        Debug.Log("@ MMJoinMatch");
        matchMaker.JoinMatch(firstMatch.networkId, "", "", "", 0, 0, OnMatchJoined);
    }

    public override void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        Debug.Log("@ MMJoinMatch");
        base.OnMatchJoined(success, extendedInfo, matchInfo);

        if (!success)
            Debug.Log("Failed to join match:" + extendedInfo);
        else
        {
            Debug.Log("Successfully joined match:" + matchInfo.networkId);
        }
    }

    private void MMCreateMatch()
    {
        Debug.Log("@ MMCreateMatch");
        this.matchMaker.CreateMatch("MM", 15, true, "", "", "", 0, 0, OnMatchCreate);
    }

    public override void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        Debug.Log("@ OnCreateMatch");
        base.OnMatchCreate(success, extendedInfo, matchInfo);

        if (!success)
            Debug.Log("Failed to join match:" + extendedInfo);
        else
        {
            Debug.Log("Successfuly created match:" + matchInfo.networkId);
        }
    }
}
