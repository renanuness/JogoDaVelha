using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum Symbol
{
    NONE = 0,
    CROSS = 1,
    CIRCLE = 2
}

public enum Commander
{
    HUMAN,
    AI
}

public interface IPlayer
{
     void Play();
}

public abstract class Player : IPlayer
{
    protected LevelController _owner;
    private Symbol symbol;
    protected Commander _commander;
    private string name;

    public Player(LevelController owner, Symbol symbol, Commander commander, string name)
    {
        _owner = owner;
        Symbol = symbol;
        _commander = commander;
        Name = name;
    }

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    public Symbol Symbol
    {
        get
        {
            return symbol;
        }

        set
        {
            symbol = value;
        }
    }

    public virtual void Play() {
    }

}

public class AIPlayer : Player
{
    public AIPlay aIPlay;

    private float TIME_TO_PLAY = 0.7f;
    private float timeToPlay;

    public AIPlayer(LevelController owner, Symbol symbol, Commander commander, string name) : base (owner,symbol,commander, name)
    {
        aIPlay = AIPlay.Instance;
        timeToPlay = TIME_TO_PLAY;
    }

    public override void Play()
    {
        if (_owner.isGameOver) { return; }

        if (BoardController.Instance.GetEmptys() == 0) {
            BoardController.Instance.EndGame();

            return;
        }
        timeToPlay -= Time.deltaTime;

       
        if(timeToPlay > 0)
        {
            return;
        }
        int move = aIPlay.FindBestMove(BoardController.Instance.board, Symbol);

        if (BoardController.Instance.MakeMove(move, Symbol))
        {
            Symbol winner;
            if (BoardController.Instance.CheckWin(BoardController.Instance.board, out winner))
            {
                BoardController.Instance.EndGame(this);
                _owner.isGameOver = true;

            }
            _owner.ChangePlayer();
        }
        timeToPlay = TIME_TO_PLAY;

    }
}

public class HumanPlayer : Player
{
    PlayerPrefab _playerOwner { get; set; }

    public HumanPlayer(LevelController owner, Symbol symbol, Commander commander, string name) : base(owner, symbol, commander, name)
    {

    }

    public override void Play()
    {
        //GetCLick
        
        if (_owner.isGameOver) { return;  }
        if(BoardController.Instance.GetEmptys() == 0){
            BoardController.Instance.EndGame();
            return;
        }
        if(Input.GetMouseButtonDown(0))
        {
            if(Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Linecast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Input.mousePosition);
                if(hit.collider.gameObject != null)
                {
                    if(BoardController.Instance.MakeMove(BoardController.Instance.SquareToPosition(hit.collider.gameObject), Symbol))
                    {
                        Symbol winner;
                        if(BoardController.Instance.CheckWin(BoardController.Instance.board, out winner))
                        {
                            //win;
                            BoardController.Instance.EndGame(this);
                            _owner.isGameOver = true;
                        }
                        _owner.ChangePlayer();
                    }
                }
            }
        }
    }
}

public static class Players
{
    public static Player PlayerOne;
    public static Player PlayerTwo;
}

public static class Factory
{
    public static Player CreatePlayer(LevelController levelController, Commander commander, Symbol symbol, string name)
    {
        return Create(levelController, commander, symbol, name);
    }

    public static Player Create(LevelController levelController, Commander commander, Symbol symbol, string name)
    {
        switch (commander)
        {
            case Commander.AI:
                return new AIPlayer(levelController, symbol,commander, name);
            case Commander.HUMAN:
                return new HumanPlayer(levelController, symbol, Commander.HUMAN, name);
        }
        return null;
    }
}

public class LevelController : NetworkBehaviour {

    
    public Player _playerToPlay;
    public bool isGameOver;
    public PlayerPrefab _playerPrefab;
    
    private void Start ()
    {
        InstantiatePlayers();
        isGameOver = false;
    }

    private void InstantiatePlayers()
    {
        Players.PlayerOne = Factory.CreatePlayer(this, PlayersInfo.p1Commander, PlayersInfo.p1Symbol, PlayersInfo.p1Name);
        Players.PlayerTwo = Factory.CreatePlayer(this, PlayersInfo.p2Commander, PlayersInfo.p2Symbol, PlayersInfo.p2Name);
        _playerToPlay = Players.PlayerOne;
    }

    private void Update ()
    {
        if(_playerPrefab == null)
        {
            _playerPrefab = FindObjectOfType<PlayerPrefab>();
        }
        
    }

    public void ChangePlayer()
    {
        if (_playerToPlay == Players.PlayerOne)
        {
            _playerToPlay = Players.PlayerTwo;
        }
        else
        {
            _playerToPlay = Players.PlayerOne;
        }
        CmdChangePlayer(_playerToPlay.Symbol);
    }


    //Command
    [Command]
    void CmdChangePlayer(Symbol player){
        _playerToPlay = Players.PlayerOne.Symbol == player ? Players.PlayerOne : Players.PlayerTwo;
        RpcChangePlayer(player);
    }

    [ClientRpc]
    void RpcChangePlayer(Symbol player)
    {
        _playerToPlay = Players.PlayerOne.Symbol == player ? Players.PlayerOne : Players.PlayerTwo;

    }
}
