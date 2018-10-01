using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {


    public void LoadSinglePlayer()
    {
        SceneManager.LoadScene(ScenesList.SP_Menu);
    }

    public void LoadMultiPlayer()
    {
        SceneManager.LoadScene(ScenesList.MP_Menu);
    }
}
