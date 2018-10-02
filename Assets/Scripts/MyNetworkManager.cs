using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MyNetworkManager : NetworkManager
{

    public static event Action<NetworkConnection> onServerConnect;

    private static MyNetworkManager Instance;

    public static MyNetworkManager _instance
    {
        get
        {
            if(Instance == null)
            {
                Instance = FindObjectOfType<MyNetworkManager>();
            }
            return Instance;
        }
    }

    public static NetworkDiscovery Discovery
    {
        get
        {
            return singleton.GetComponent<NetworkDiscovery>();
        }
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnServerConnect(NetworkConnection conn)
    {
        if (conn.address == "localClient")
        {
            return;
        }

        Debug.Log("Client connected! Address: " + conn.address);

        if (onServerConnect != null)
        {
            onServerConnect(conn);
        }
    }

    public void SpawnObject(GameObject obj)
    {
        GameObject o = Instantiate(obj);
        NetworkServer.Spawn(o);
    }
    
}
