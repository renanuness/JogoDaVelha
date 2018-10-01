using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MP_NetworkManager : NetworkManager
{
    public static event Action<NetworkConnection> onServerConnect;

    public static NetworkDiscovery Discovery
    {
        get
        {
            return singleton.GetComponent<NetworkDiscovery>();
        }
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        if (conn.address == "localClient")
        {
            return;
        }

        if(onServerConnect != null)
        {
            onServerConnect(conn);
        }
    }

    public override void OnClientError(NetworkConnection conn, int errorCode)
    {
        base.OnClientError(conn, errorCode);
    }

}
