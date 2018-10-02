using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Enabler : NetworkBehaviour
{

    public GameObject BoardController;
    public GameObject LevelController; 
	// Use this for initialization
	void Start () {
        if (!isServer)
            return;

        BoardController.SetActive(true);
        LevelController.SetActive(true);
        MyNetworkManager._instance.SpawnObject(BoardController);
        MyNetworkManager._instance.SpawnObject(LevelController);
    }
	

}
