using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public int xIndex;
    public int yIndex;

    Board _board;

    public void Initialize(int newXIndex,int newYIndex,Board newBoard)
    {
        xIndex = newXIndex;
        yIndex = newYIndex;
        _board = newBoard;
    }

    private void OnMouseDown()
    {
        if (_board)
        {
            _board.ClickTile(this);
        }
    }

    private void OnMouseEnter()
    {
        if (_board)
        {
            _board.DragToTile(this);
        }
    }

    private void OnMouseUp()
    {
        if (_board)
        {
            _board.ReleaseTile();

        }
    }
}
