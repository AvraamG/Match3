using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiece : MonoBehaviour
{
    public int xIndex;
    public int yIndex;


    public void SetCoordinates(int xCoordinate,int yCoordinate)
    {
        xIndex = xCoordinate;
        yIndex = yCoordinate;
    }
}
