using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class PlayersInfoController : NetworkBehaviour
{
    public Dropdown p1Commander;
    public Dropdown p2Commander;
    public Image p1SymbolImage;
    public Image p2SymbolImage;
    public Sprite crossSprite;
    public Sprite circleSprite;
    public InputField p1InputField;
    public InputField p2InputField;

    private string p1AIName = "AI1";
    private string p2AIName = "AI2";

    private void Start()
    {
        DontDestroyOnLoad(this);
        PlayersInfo.p1Commander = Commander.HUMAN;
        PlayersInfo.p2Commander = Commander.HUMAN;
        PlayersInfo.p1Symbol = Symbol.CIRCLE;
        PlayersInfo.p2Symbol = Symbol.CROSS;
    }

    public void SetPlayerOneCommander()
    {
        if (p1Commander.value == 0)
        {
            PlayersInfo.p1Commander = Commander.HUMAN;
            p1InputField.readOnly = false;
            p1InputField.text = "";
            CleanName(ref PlayersInfo.p1Name);


        }
        else
        {
            PlayersInfo.p1Commander = Commander.AI;
            p1InputField.readOnly = true;
            p1InputField.text = p1AIName;
            SetPlayOneName();
        }
    }

    public void SetPlayerTwoCommander()
    {
        if (p2Commander.value == 0)
        {
            PlayersInfo.p2Commander = Commander.HUMAN;
            p2InputField.readOnly = false;
            p2InputField.text = "";
            CleanName(ref PlayersInfo.p2Name);
        }
        else
        {
            PlayersInfo.p2Commander = Commander.AI;
            p2InputField.readOnly = true;
            p2InputField.text = p2AIName;
            SetPlayTwoName();
        }
    }

    public void ToggleSymbol()
    {
        if (isLocalPlayer)
        {
            CmdUpdateUIData();
        }
        if (isServer)
        {
            RpcUpdateUIData();
        }
    }

    public void SetPlayOneName()
    {
        PlayersInfo.p1Name = p1InputField.text;
    }

    public void SetPlayTwoName()
    {
        PlayersInfo.p2Name = p2InputField.text;
    }

    private void CleanName(ref string name)
    {
        name = null;
    }


    //COMMANDS

    [Command]
    void CmdUpdateUIData()
    {
        if (PlayersInfo.p1Symbol == Symbol.CIRCLE)
        {
            PlayersInfo.p1Symbol = Symbol.CROSS;
            PlayersInfo.p2Symbol = Symbol.CIRCLE;
            p1SymbolImage.GetComponent<Image>().sprite = crossSprite;
            p2SymbolImage.GetComponent<Image>().sprite = circleSprite;

        }
        else
        {
            PlayersInfo.p1Symbol = Symbol.CIRCLE;
            PlayersInfo.p2Symbol = Symbol.CROSS;
            p1SymbolImage.GetComponent<Image>().sprite = circleSprite;
            p2SymbolImage.GetComponent<Image>().sprite = crossSprite;

        }
    }

    //RPC
    [ClientRpc]
    void RpcUpdateUIData()
    {
        if (PlayersInfo.p1Symbol == Symbol.CIRCLE)
        {
            PlayersInfo.p1Symbol = Symbol.CROSS;
            PlayersInfo.p2Symbol = Symbol.CIRCLE;
            p1SymbolImage.GetComponent<Image>().sprite = crossSprite;
            p2SymbolImage.GetComponent<Image>().sprite = circleSprite;

        }
        else
        {
            PlayersInfo.p1Symbol = Symbol.CIRCLE;
            PlayersInfo.p2Symbol = Symbol.CROSS;
            p1SymbolImage.GetComponent<Image>().sprite = circleSprite;
            p2SymbolImage.GetComponent<Image>().sprite = crossSprite;

        }
    }
}

public static class PlayersInfo
{

    public static Symbol p1Symbol;
    public static Commander p1Commander;
    public static Symbol p2Symbol;
    public static Commander p2Commander;
    public static string p1Name;
    public static string p2Name;

    public static string ValidatePlayerInfo()
    {
        string erroMSG = "";
        if(p1Name == null || p2Name == null)
        {
            erroMSG += "Adicione nome para os jogadores";
        }
        return erroMSG;
    }


}