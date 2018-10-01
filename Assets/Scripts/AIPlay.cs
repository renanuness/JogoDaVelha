using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlay : MonoBehaviour {

    BoardController boardController;
    static AIPlay instance;

    public static AIPlay Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<AIPlay>();
            }
            return instance;
        }
    }

    private void Start()
    {
        boardController = BoardController.Instance;
        instance = this;
    }

    public int FindBestMove(int[] board, Symbol symbol)
    {
        int bestScore = -999;
        int move = -1;
        for ( int i = 0; i < board.Length; i++)
        {
            if(board[i] == 0)
            {
                board[i] = (int)symbol;
                int score = Minimax(board, -999, 999, symbol, false);
                board[i] = 0;
                if (score > bestScore)
                {
                    bestScore = score;
                    move = i;
                }
            }
        }
        return move;
    }

    public int Minimax(int[] board, int alpha, int beta, Symbol symbol,bool maximize)
    {
        //Ver se alguem ganhou ou o jogo acabou empatado e retornar o valor
        // +10 se for o jogador a jogar -10 se for o outro e 0 para o empate

        //se maximizador == false 
        //bestScore = +999
        //bestScore = Mathf.Min(bestScore, Minimax())
        // retorna bestscore
        Symbol winner;
        
        if(boardController.CheckWin(board, out winner))
        {
            if(winner == symbol)
            {
                return 10;
            }
            else
            {
                return -10;
            }
        }

        if(boardController.GetEmptys(board) == 0)
        {
            return 0;
        }

        if(!maximize)
        {
            int bestScore = 999;
            for(int i = 0; i < board.Length; i++)
            {
                if(board[i] == 0)
                {
                    board[i] = GetOtherSymbol(symbol);
                    int score = Minimax(board, alpha, beta, symbol, !maximize);
                    board[i] = 0;
                    if (score < bestScore)
                    {
                        bestScore = score;
                    }
                    alpha = Mathf.Max(alpha, bestScore);
                    if(beta <= alpha) { break; }
                }
            }
            return bestScore;
        }
        else
        {
            int bestScore = -999;
            for (int i = 0; i < board.Length; i++)
            {
                if (board[i] == 0)
                {
                    board[i] = (int)symbol;
                    int score = Minimax(board, alpha, beta, symbol, !maximize );
                    board[i] = 0;
                    if (score > bestScore)
                    {
                        bestScore = score;
                    }

                    beta = Mathf.Max(beta, bestScore);
                    if (beta <= alpha) { break; }
                }
            }
            return bestScore;
        }
    }

    private int GetOtherSymbol(Symbol symbol)
    {
        if(symbol == Symbol.CIRCLE)
        {
            return 1;
        }
        else if(symbol == Symbol.CROSS)
        {
            return 2;
        }
        return 0;
    }
}
