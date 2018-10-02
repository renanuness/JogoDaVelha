using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enabler : MonoBehaviour
{
    public GameObject BoardController;
    public GameObject LevelController; 
	// Use this for initialization
	void Start () {
        BoardController.SetActive(true);
        LevelController.SetActive(true);
	}
	

}
