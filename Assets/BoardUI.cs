using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardUI : MonoBehaviour {

    public Sprite _circleSprite;
    public Sprite _crossSprite;
    private GameObject[] squares = new GameObject[9];

    void Start () {
		
	}
	
	void Update () {
		
	}

    private void GetSquares()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            squares[i] = transform.GetChild(i).gameObject;
        }
    }

    public void SetSquare(int pos, Symbol symbol)
    {
        squares[pos].GetComponent<SpriteRenderer>().sprite = symbol == Symbol.CIRCLE ? _circleSprite : _crossSprite;
    }
}
