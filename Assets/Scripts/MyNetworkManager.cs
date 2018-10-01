using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkManager : NetworkManager
{

    public static event Action<NetworkConnection> onServerConnect;

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

}
