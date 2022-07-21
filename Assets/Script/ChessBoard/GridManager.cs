using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : Singleton<GridManager>
{
    private void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }
    }

    [Header("BoardPrperty")]
    [SerializeField] int width;
    [SerializeField] int height;
    public float boardScale;
    [SerializeField] GameObject whiteBoard;
    [SerializeField] GameObject blackBoard;



    public Dictionary<int, GameObject> boardList = new Dictionary<int, GameObject>();
    private int boardNum = 0;
    public Camera cam;
    
    //一マスのタイルの情報
    [System.Serializable]
    public class TileInfo
    {
        public bool isUsed;　        //コマがある
        public bool isSelected;     //選んでいる状態
        public bool isHovered;     //ホーバー状態
        public string pieceName;    //コマの種類

        public Vector2Int Index;       //配列でのIndex

        //オブジェクトではない
        public SpriteRenderer playerA;
        public SpriteRenderer playerB;
        public SpriteRenderer chosen;

        //コンストラクタ
        public TileInfo()
        {
            isUsed = false;
            isSelected = false;
            pieceName = "なし";
        }

        public void clear()
        {
            isUsed = false;
            isSelected = false;
            pieceName = "なし";
        }

    }
    TileInfo[,] board;

    
    public Transform playerARenders;
    public Transform playerBRenders;
    public Transform chosenHighlits;


    public Sprite koma;
    public Sprite highlight;

    TileInfo selectedTile;

    private void Start()
    {
        //配列に子を入れる
        SpriteRenderer[] playerATemp = playerARenders.GetComponentsInChildren<SpriteRenderer>();
        SpriteRenderer[] playerBTemp = playerBRenders.GetComponentsInChildren<SpriteRenderer>();
        SpriteRenderer[] chosenArray =      chosenHighlits.GetComponentsInChildren<SpriteRenderer>();

        board = new TileInfo[width, height];
        for(int x = 0; x < width; x++)
            for(int y = 0; y < height; y++)
            {
                board[x, y] = new TileInfo();

                //IDみたいな 〇〇さん
                board[x, y].Index = new Vector2Int(x, y);

                //0,0 = 0 1,0 = 1 2,0 = 2... 35まで ペア
                board[x, y].playerA   = playerATemp[x + y * width];
                board[x, y].playerB   = playerBTemp[x + y * width];
                board[x, y].chosen    = chosenArray[x + y * width];
            }


        //仮
        board[0, 0].isUsed = true;
        board[0, 0].pieceName = "Rook";
        

        board[4, 3].isUsed = true;
        board[4, 3].pieceName = "Knight";

        //保存されたタイル
        selectedTile = new TileInfo();

        BoardRenderUpdate();
        GenerateGrid();
    }

    private void Update()
    {
        //タイルを選ぶ
        if (Input.GetMouseButtonDown(0))
        {
            //タイルの情報を取得 もらった
            TileInfo returnedTile = GetTile();
            if (returnedTile == null)
                return;


            //起動、最初false　クリックした前にクリックしたタイルがこれ
            if (!selectedTile.isUsed)
            {
                //取得したタイルにコマがあったら
                if (returnedTile.isUsed)
                {
                    returnedTile.isSelected = true;
                    selectedTile = returnedTile;
                }
            }
            else
            { 
                //  前タイル　後タイル 同じ場所を選んだら
                if (selectedTile.Index == returnedTile.Index)
                {
                    //なかったことに
                    selectedTile.isSelected = false;
                    selectedTile = new TileInfo();    
                }
                //違う場所
                else
                {
                    //新しいタイルにコマがなかったら
                    if (!returnedTile.isUsed)
                    {
                        //コマ出現
                        returnedTile.isUsed = true;
                        returnedTile.pieceName = selectedTile.pieceName;
                        //コマがなくなる
                        selectedTile.clear();
                    }
                }
            }
            //描画
            BoardRenderUpdate();
        }
    }

    private TileInfo GetTile()
    {
        //実際に呼ばれるのはGetTile()が呼ばれるときに使う
        float x = cam.ScreenToWorldPoint(Input.mousePosition).x;
        float y = cam.ScreenToWorldPoint(Input.mousePosition).y;

        //タイルの大きさの矩形を作っている
        //矩形の中にマウスのカーソルが入ってなかったらnullを返す
        if (x < 0 || y < 0 || x > width * boardScale || y > height * boardScale)
            return null;

        //カーソルの位置にあるタイルの情報を返す
        return board[(int)(x / boardScale), (int)(y / boardScale)];
    }


    private bool GenerateGrid()
    {
        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                if ((y % 2 == 0 && x % 2 != 0) || (y % 2 != 0 && x % 2 == 0))
                {
                    var spawnBlack = Instantiate(blackBoard, new Vector3(x, y), Quaternion.identity, gameObject.transform);
                    spawnBlack.name = boardNum.ToString();
                    boardList.Add(boardNum, spawnBlack);
                }
                else
                {
                    var spawnWhite = Instantiate(whiteBoard, new Vector3(x, y), Quaternion.identity, gameObject.transform);
                    spawnWhite.name = boardNum.ToString();
                    boardList.Add(boardNum, spawnWhite);
                }
                boardNum++;
            }
        }

        transform.localScale = new Vector3(boardScale, boardScale, 1);
        //Camera.main.transform.position = new Vector3(((float)width / 2 - 0.5f) * boardScale, ((float)height / 2 - 0.5f) * boardScale, -10f);

        return true;
    }

    //描画
    void BoardRenderUpdate()
    {
        //配列だけ
        //配列の要素の数だけ０から最後まで回す　　変数　配列の要素を一個ずつ取り出しながら回すループ
        foreach(TileInfo tile in board)
        {
            //パンプキン
            if (tile.isSelected)
            {
                tile.chosen.sprite = highlight;
            }
            else tile.chosen.sprite = null;

            //コマがある
            if (tile.isUsed)
            {
                //スプライト（画像）
                tile.playerA.sprite = koma;
                tile.playerB.sprite = koma;
            }
            //コマがない
            else
            {
                //非表示
                tile.playerA.sprite = null;
                tile.playerB.sprite = null;
            }
        }
    }

}
