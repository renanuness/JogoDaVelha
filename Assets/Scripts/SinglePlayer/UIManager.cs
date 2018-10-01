using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour {


    public Text p1TextName;
    public Text p2TextName;
    public Image p1ImageSymbol;
    public Image p2ImageSymbol;
    public Sprite crossSprite;
    public Sprite circleSprite;

    public Button ButtonRestart;
    public Button ButtonMenu;
    public Text TxtResult;
    public GameObject EndGameUI;

    private void Start ()
    {
        EndGameUI.SetActive(false);
        SetImageSymbols();
        SetTextNames();
	}
	
    private void SetTextNames()
    {
        p1TextName.text = PlayersInfo.p1Name;
        p2TextName.text = PlayersInfo.p2Name;
    }

    private void SetImageSymbols()
    {
        p1ImageSymbol.GetComponent<Image>().sprite = PlayersInfo.p1Symbol == Symbol.CIRCLE ? circleSprite : crossSprite;
        p2ImageSymbol.GetComponent<Image>().sprite = PlayersInfo.p2Symbol == Symbol.CIRCLE ? circleSprite : crossSprite;
    }


    public void EnableFinalPanel(string winner)
    {

        TxtResult.text = winner + " is the winner!";
        StartCoroutine(ShowPanel());
    }
    public void EnableFinalPanel()
    {
        TxtResult.text = " Its a draw!";
        StartCoroutine(ShowPanel());
    }

    private IEnumerator ShowPanel()
    {
        yield return new WaitForSeconds(1);
        EndGameUI.SetActive(true);
    }
    public void DisableFinalPanel()
    {
        EndGameUI.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(ScenesList.SP_Game);
    }

    public void MenuScreen()
    {
        PlayersInfo.p1Name = null;
        PlayersInfo.p2Name = null;
        SceneManager.LoadScene(ScenesList.SP_Menu);
    }
}
