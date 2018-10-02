using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour
{
    private static GameManager _instance;

    public GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if(_instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
        }
    }

    public void StartGame()
    {
        if (isServer)
        {
            MyNetworkManager.singleton.ServerChangeScene(ScenesList.SP_Game);
        }
        
    }
    
    public void BackMainMenu()
    {
        
        SceneManager.LoadScene(ScenesList.MainMenu);
    }

    //Commands

    [Command]
    void CmdStartGame()
    {
        string errorMSG = PlayersInfo.ValidatePlayerInfo();
        if (errorMSG.Length > 2)
        {
            Debug.Log("Show Error");
            return;
        }
        SceneManager.LoadScene(ScenesList.SP_Game);
    }

    [ClientRpc]
    void RpcStartGame()
    {
        string errorMSG = PlayersInfo.ValidatePlayerInfo();
        if (errorMSG.Length > 2)
        {
            Debug.Log("Show Error");
            return;
        }
        SceneManager.LoadScene(ScenesList.SP_Game);
    }
}
