using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerPrefab : NetworkBehaviour
{
    public IPlayer _player;

	// Use this for initialization
	void Start () {
        if (isServer)
        {
            _player = Players.PlayerOne;
        }
        else
        {
            _player = Players.PlayerTwo;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
