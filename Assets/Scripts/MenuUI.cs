using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour {


    LocalGameView _localGameView;

	// Use this for initialization
	void Start () {
        _localGameView = FindObjectOfType<LocalGameView>();
	}
	
	public void CreateMatch()
    {
        _localGameView.CreateMatch();   
    }
}
