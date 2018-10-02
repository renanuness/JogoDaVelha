using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerPrefab : NetworkBehaviour
{
    public Player _player;
    private LevelController _levelController;

	// Use this for initialization
	void Start () {
        if (isServer)
        {
            _player = Players.PlayerOne;
            Debug.Log(Players.PlayerOne);
        }
        else
        {
            _player = Players.PlayerTwo;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(_levelController == null)
        {
            FindLevelController();
        }
        if (_player == null)
            return;
        if (!hasAuthority)
            return;
        if(_player.Symbol == _levelController._playerToPlay.Symbol)
        {
            _player.Play();
        }
        
	}

    void FindLevelController()
    {
        LevelController[] controllers = FindObjectsOfType<LevelController>();
        foreach(var c in controllers)
        {
            if (c.GetComponent<NetworkIdentity>().localPlayerAuthority)
            {
                _levelController = c;
                return;
            }
        }
    }
}
