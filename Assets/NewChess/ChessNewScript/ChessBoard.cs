using UnityEngine;
using System;
using System.Collections.Generic;

public class ChessBoard : MonoBehaviour
{
    private const int tileCountX = 6;
    private const int tileCountY = 6;

    private GameObject[,] tiles;
    private Camera currentCamera;

    [Header("Sprites")]
    public Vector2 offsetForChessPieces;
    public float deadPieceSize = 0.3f;
    public Sprite whiteTile;
    public Sprite blackTile;


    private Vector2Int currentHover;
    RaycastHit2D lastInfo;

    Vector2Int hitPosition;

    [Header("Prefabs&Materials")]
    [SerializeField] GameObject[] prefabW;
    [SerializeField] GameObject[] prefabB;
    private GameObject[][] prefabs;

    [Header("Logic")]

    private ChessPiece[,] chessPieces;
    private ChessPiece currentlyDragging;


    private List<Vector2Int> availableMoves = new List<Vector2Int>();
    private List<ChessPiece> deadWhites = new List<ChessPiece>();
    private List<ChessPiece> deadBlacks = new List<ChessPiece>();


    private void Awake()
    {
        prefabs = new GameObject[2][] { prefabW, prefabB};
        GenerateAllTiles(1, tileCountX, tileCountY);
        SpawnAllPieces();
        PositionAllPieces();
    }

    private void Update()
    {
        if (!currentCamera)
        {
            currentCamera = Camera.main;
            currentCamera.transform.position = new Vector3(-0.5f, -0.5f, -10);
            return;
        }


        RaycastHit2D info = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition),10, LayerMask.GetMask("Tile", "Hover","Highlight"));
        if (info.collider)
        {

            // Get the indexes of the tile i've hit

            hitPosition = LookupTileIndex(info.transform.gameObject);

            // If we're hovering a tile after not hovering any tiles
            if (currentHover == -Vector2Int.one)
            {
                currentHover = hitPosition;
                tiles[hitPosition.x, hitPosition.y].layer = LayerMask.NameToLayer("Hover");
                Debug.Log(tiles[hitPosition.x, hitPosition.y] + " " + tiles[hitPosition.x, hitPosition.y].layer);
            }

            // If we were already hovering a tile, change the previous one
            if (currentHover != hitPosition)
            {
                tiles[currentHover.x, currentHover.y].layer = (ContainsValidMove(ref availableMoves, currentHover)) ? LayerMask.NameToLayer("Highlight") : LayerMask.NameToLayer("Tile");
                currentHover = hitPosition;
                tiles[hitPosition.x, hitPosition.y].layer = LayerMask.NameToLayer("Hover");
            }

            if(Input.GetMouseButtonDown(0))
            {
                if(chessPieces[hitPosition.x,hitPosition.y] != null)
                {
                    if(true)
                    {
                        currentlyDragging = chessPieces[hitPosition.x,hitPosition.y];

                        availableMoves = currentlyDragging.GetAvailableMoves(ref chessPieces, tileCountX, tileCountY);
                        HighlightTiles();
                    }
                }
            }

            if (currentlyDragging != null && Input.GetMouseButtonUp(0))
            {
                Vector2Int previousPosition = new Vector2Int(currentlyDragging.currentX,currentlyDragging.currentY);

                bool validMove = MoveTo(currentlyDragging, hitPosition.x, hitPosition.y);
                if(!validMove)
                    currentlyDragging.SetPosition(GetTileCenter(previousPosition.x, previousPosition.y));
                currentlyDragging = null;
                RemoveHighlightTiles();
            }

        }
        else
        {
            if (currentHover != -Vector2Int.one)
            {
                tiles[currentHover.x, currentHover.y].layer = (ContainsValidMove(ref availableMoves, currentHover)) ? LayerMask.NameToLayer("Highlight") : LayerMask.NameToLayer("Tile");
                currentHover = -Vector2Int.one;
            }   
            if (currentlyDragging && Input.GetMouseButtonUp(0))
            {
                currentlyDragging.SetPosition(GetTileCenter(currentlyDragging.currentX, currentlyDragging.currentY));
                currentlyDragging = null;
                RemoveHighlightTiles();
            }
        }
        if(currentlyDragging)
        {
            currentlyDragging.SetPosition(new Vector3(
                Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 
                Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 
                0));
        }
    }

    private bool MoveTo(ChessPiece cp, int x, int y)
    {
        if(!ContainsValidMove(ref availableMoves, new Vector2(x, y)))
            return false;

        Vector2Int previousPosition = new Vector2Int(cp.currentX, cp.currentY);

        if(chessPieces[x,y] != null)
        {
            ChessPiece otherChessPiece = chessPieces[x, y];

            if(otherChessPiece.team == cp.team)
            {
                return false;
            }

            //敵の駒
            if(otherChessPiece.team == 0)
            {
                deadWhites.Add(otherChessPiece);
                otherChessPiece.SetScale(deadPieceSize * Vector3.one);
                otherChessPiece.SetPosition(new Vector3(-tileCountX * 0.5f + deadWhites.Count - 1, +tileCountY - 3, 0), true);
            }
            else
            {
                deadBlacks.Add(otherChessPiece);
                otherChessPiece.SetScale(deadPieceSize * Vector3.one);
                otherChessPiece.SetPosition(new Vector3(tileCountX * 0.5f - deadBlacks.Count, -tileCountY + 2, 0), true);
            }

        }

        chessPieces[x, y] = cp;
        chessPieces[previousPosition.x, previousPosition.y] = null;

        PositionSinglePiece(x, y);
        return true;
    }

    //作成
    private void GenerateAllTiles(float tileSize, int tileCountX, int tileCountY)
    {
        tiles = new GameObject[tileCountX, tileCountY];
        for (int i = 0; i < tileCountX; i++)
            for (int j = 0; j < tileCountY; j++)
            {
                tiles[i,j] = GenerateSingleTile(tileSize, i, j);
            }    
    }
    private GameObject GenerateSingleTile(float tileSize, int x, int y)
    {
        GameObject tileObject = new GameObject(string.Format("X:{0}, Y:{1}", x, y));
        tileObject.transform.parent = transform;
        tileObject.transform.position = new Vector3(-tileCountX*0.5f + x, -tileCountY * 0.5f +y, 0);
        //tileObject.transform.position = new Vector3( x, y, 0);
        if ((x + y) % 2 == 0)
            tileObject.AddComponent<SpriteRenderer>().sprite = whiteTile;
        else tileObject.AddComponent<SpriteRenderer>().sprite = blackTile;
        tileObject.layer = LayerMask.NameToLayer("Tile");
        tileObject.AddComponent<BoxCollider2D>();

        return tileObject;
    }

    private void SpawnAllPieces()
    {
        chessPieces = new ChessPiece[tileCountX, tileCountY];
        int whiteTeam = 0, blackTeam = 1;
        //下のコードは「仮」！
        chessPieces[0, 0] = SpawnSinglePiece(ChessPieceType.Rook, whiteTeam);
        chessPieces[1, 0] = SpawnSinglePiece(ChessPieceType.Bishop, whiteTeam);
        chessPieces[2, 0] = SpawnSinglePiece(ChessPieceType.Queen, whiteTeam);
        chessPieces[3, 0] = SpawnSinglePiece(ChessPieceType.King, whiteTeam);
        chessPieces[4, 0] = SpawnSinglePiece(ChessPieceType.Bishop, whiteTeam);
        chessPieces[5, 0] = SpawnSinglePiece(ChessPieceType.Rook, whiteTeam);

        chessPieces[0, 5] = SpawnSinglePiece(ChessPieceType.Rook, blackTeam);
        chessPieces[1, 5] = SpawnSinglePiece(ChessPieceType.Bishop, blackTeam);
        chessPieces[2, 5] = SpawnSinglePiece(ChessPieceType.Queen, blackTeam);
        chessPieces[3, 5] = SpawnSinglePiece(ChessPieceType.King, blackTeam);
        chessPieces[4, 5] = SpawnSinglePiece(ChessPieceType.Bishop, blackTeam);
        chessPieces[5, 5] = SpawnSinglePiece(ChessPieceType.Rook, blackTeam);
    }
    private ChessPiece SpawnSinglePiece(ChessPieceType type, int team)
    {
        ChessPiece cp = Instantiate(prefabs[team][(int)type - 1], transform).GetComponent<ChessPiece>();

        cp.type = type;
        cp.team = team;
        return cp; 

    }

    private void PositionAllPieces()
    {
        for (int x = 0; x < tileCountX; x++)
            for (int y = 0; y < tileCountY; y++)
                if (chessPieces[x, y] != null)
                    PositionSinglePiece(x, y, true);
    }

    private void PositionSinglePiece(int x, int y, bool force = false)
    {
        chessPieces[x, y].currentX = x;
        chessPieces[x, y].currentY = y;
        chessPieces[x, y].GetComponent<SpriteRenderer>().sortingOrder = 1;
        chessPieces[x, y].SetPosition(GetTileCenter(x, y), force);
    }

    private void HighlightTiles()
    {
        for(int i = 0; i< availableMoves.Count; i++)
        {
            tiles[availableMoves[i].x, availableMoves[i].y].layer = LayerMask.NameToLayer("Highlight");
        }
    }

    private void RemoveHighlightTiles()
    {
        for (int i = 0; i < availableMoves.Count; i++)
        {
            tiles[availableMoves[i].x, availableMoves[i].y].layer = LayerMask.NameToLayer("Tile");
        }
        availableMoves.Clear();
    }

    private bool ContainsValidMove(ref List<Vector2Int> moves, Vector2 pos)
    {
        for (int i = 0; i < moves.Count; i++)
            if(moves[i].x == pos.x && moves[i].y == pos.y)
                return true;
        return false;
    }

    //検索
    private Vector2Int LookupTileIndex(GameObject hitInfo)
    {

        for (int x = 0; x < tileCountX; x++)
            for (int y = 0; y < tileCountY; y++) 
                if (tiles[x, y] == hitInfo)
                    return new Vector2Int(x, y);

        return -Vector2Int.one;
    }

    private Vector3 GetTileCenter(int x, int y)
    {
        return new Vector3(-tileCountX * 0.5f + x, -tileCountY * 0.5f + y);
    }

}
