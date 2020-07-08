using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    GameObject tilePrefab;

    public int width;
    public int height;

    //2D Array that will hold the arrays
    Tile[,] allTiles;
    GamePiece[,] allGamePieces;

  public  GameObject[] gamePiecePrefabs;

    public int margin;


    private Tile _clickedTile;
    private Tile _targetTile;

    private void Start()
    {
        SetupTiles();
        SetupGamePieces();
        //TODO Instead let the tiles know the camera
        SetupCamera();
    }


    private void SetupCamera()
    {
        //Orthographic Size is basically 2x the height camera can see. E.G if its set to 0.5 the camera can see up 1 Unit (1 meter).
        //The width on the other hand is related to the aspect ratio.


        //TODO this I dont understand why is 1.
        float tilePivotX = 1;
        float tilePivotY = 1;

        Camera.main.transform.position = new Vector3((width -tilePivotX) / 2f,( height - tilePivotY) / 2f, -10f);

        float aspectRatio = (float)Screen.width / (float)Screen.height;

        float verticalSize = height / 2f + margin;

        float horizontalSize = (width / 2f + margin) / aspectRatio;

        Camera.main.orthographicSize = Mathf.Max(verticalSize, horizontalSize);
    }

    private void SetupTiles()
    {
        allTiles = new Tile[width, height];

        for (int xIndex = 0; xIndex < width; xIndex++)
        {
            for (int yIndex = 0; yIndex < height; yIndex++)
            {
                //TODO in the future I should check the sprite height and width to the pixel size to determine the position.

                float widthSize = 1;
                float heightSize = 1;
                Vector3 tilePosition = new Vector3(widthSize * xIndex, heightSize * yIndex, 0);

                GameObject tile = Instantiate(tilePrefab, tilePosition, Quaternion.identity);
                tile.name = "Tile "+xIndex+","+yIndex;
                tile.transform.parent = this.transform;

                allTiles[xIndex, yIndex] = tile.GetComponent<Tile>();
                allTiles[xIndex, yIndex].Initialize(xIndex, yIndex, this); 
           
            }
        }
    }

    #region GamePieces
    private void SetupGamePieces()
    {
        allGamePieces = new GamePiece[width, height];
        FillRandom();
    }
    private GameObject GetRandomGamePiece()
    {
        int randomIndex = Random.Range(0, gamePiecePrefabs.Length);

        while (!gamePiecePrefabs[randomIndex])
        {
             randomIndex = Random.Range(0, gamePiecePrefabs.Length);

        }
        return gamePiecePrefabs[randomIndex];
    }

    private void PlaceGamePiece(GamePiece piece, int x, int y)
    {

        piece.transform.position = new Vector3(x, y, 0);
        piece.transform.rotation = Quaternion.identity;
        piece.SetCoordinates(x, y);
    }

    private void FillRandom()
    {
        for (int i = 0; i < width; i++)
        {

            for (int j = 0; j < height; j++)
            {
                GameObject randomPiece = Instantiate(GetRandomGamePiece());
                PlaceGamePiece(randomPiece.GetComponent<GamePiece>(), i, j);
            }

        }
    }
    #endregion

    #region MouseInput

    public void ClickTile(Tile tile)
    {
        if (!_clickedTile)
        {
            _clickedTile = tile;
        }
    }

    public void DragToTile(Tile tile)
    {
        if (_clickedTile)
        {
            _targetTile = tile;
        }
    }

    public void ReleaseTile()
    {
        if (_clickedTile && _targetTile)
        {
            SwitchTiles(_clickedTile, _targetTile);
        }
    }

    void SwitchTiles(Tile clickedTile, Tile targetTile)
    {
        Vector3 clickedtilePos = new Vector3(clickedTile.xIndex, clickedTile.yIndex, 0);

        //Switch them.

        _clickedTile = null;
        _targetTile = null;
    }

#endregion
}
