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
    
    //��}�X�̃^�C���̏��
    [System.Serializable]
    public class TileInfo
    {
        public bool isUsed;�@        //�R�}������
        public bool isSelected;     //�I��ł�����
        public bool isHovered;     //�z�[�o�[���
        public string pieceName;    //�R�}�̎��

        public Vector2Int Index;       //�z��ł�Index

        //�I�u�W�F�N�g�ł͂Ȃ�
        public SpriteRenderer playerA;
        public SpriteRenderer playerB;
        public SpriteRenderer chosen;

        //�R���X�g���N�^
        public TileInfo()
        {
            isUsed = false;
            isSelected = false;
            pieceName = "�Ȃ�";
        }

        public void clear()
        {
            isUsed = false;
            isSelected = false;
            pieceName = "�Ȃ�";
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
        //�z��Ɏq������
        SpriteRenderer[] playerATemp = playerARenders.GetComponentsInChildren<SpriteRenderer>();
        SpriteRenderer[] playerBTemp = playerBRenders.GetComponentsInChildren<SpriteRenderer>();
        SpriteRenderer[] chosenArray =      chosenHighlits.GetComponentsInChildren<SpriteRenderer>();

        board = new TileInfo[width, height];
        for(int x = 0; x < width; x++)
            for(int y = 0; y < height; y++)
            {
                board[x, y] = new TileInfo();

                //ID�݂����� �Z�Z����
                board[x, y].Index = new Vector2Int(x, y);

                //0,0 = 0 1,0 = 1 2,0 = 2... 35�܂� �y�A
                board[x, y].playerA   = playerATemp[x + y * width];
                board[x, y].playerB   = playerBTemp[x + y * width];
                board[x, y].chosen    = chosenArray[x + y * width];
            }


        //��
        board[0, 0].isUsed = true;
        board[0, 0].pieceName = "Rook";
        

        board[4, 3].isUsed = true;
        board[4, 3].pieceName = "Knight";

        //�ۑ����ꂽ�^�C��
        selectedTile = new TileInfo();

        BoardRenderUpdate();
        GenerateGrid();
    }

    private void Update()
    {
        //�^�C����I��
        if (Input.GetMouseButtonDown(0))
        {
            //�^�C���̏����擾 �������
            TileInfo returnedTile = GetTile();
            if (returnedTile == null)
                return;


            //�N���A�ŏ�false�@�N���b�N�����O�ɃN���b�N�����^�C��������
            if (!selectedTile.isUsed)
            {
                //�擾�����^�C���ɃR�}����������
                if (returnedTile.isUsed)
                {
                    returnedTile.isSelected = true;
                    selectedTile = returnedTile;
                }
            }
            else
            { 
                //  �O�^�C���@��^�C�� �����ꏊ��I�񂾂�
                if (selectedTile.Index == returnedTile.Index)
                {
                    //�Ȃ��������Ƃ�
                    selectedTile.isSelected = false;
                    selectedTile = new TileInfo();    
                }
                //�Ⴄ�ꏊ
                else
                {
                    //�V�����^�C���ɃR�}���Ȃ�������
                    if (!returnedTile.isUsed)
                    {
                        //�R�}�o��
                        returnedTile.isUsed = true;
                        returnedTile.pieceName = selectedTile.pieceName;
                        //�R�}���Ȃ��Ȃ�
                        selectedTile.clear();
                    }
                }
            }
            //�`��
            BoardRenderUpdate();
        }
    }

    private TileInfo GetTile()
    {
        //���ۂɌĂ΂��̂�GetTile()���Ă΂��Ƃ��Ɏg��
        float x = cam.ScreenToWorldPoint(Input.mousePosition).x;
        float y = cam.ScreenToWorldPoint(Input.mousePosition).y;

        //�^�C���̑傫���̋�`������Ă���
        //��`�̒��Ƀ}�E�X�̃J�[�\���������ĂȂ�������null��Ԃ�
        if (x < 0 || y < 0 || x > width * boardScale || y > height * boardScale)
            return null;

        //�J�[�\���̈ʒu�ɂ���^�C���̏���Ԃ�
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

    //�`��
    void BoardRenderUpdate()
    {
        //�z�񂾂�
        //�z��̗v�f�̐������O����Ō�܂ŉ񂷁@�@�ϐ��@�z��̗v�f��������o���Ȃ���񂷃��[�v
        foreach(TileInfo tile in board)
        {
            //�p���v�L��
            if (tile.isSelected)
            {
                tile.chosen.sprite = highlight;
            }
            else tile.chosen.sprite = null;

            //�R�}������
            if (tile.isUsed)
            {
                //�X�v���C�g�i�摜�j
                tile.playerA.sprite = koma;
                tile.playerB.sprite = koma;
            }
            //�R�}���Ȃ�
            else
            {
                //��\��
                tile.playerA.sprite = null;
                tile.playerB.sprite = null;
            }
        }
    }

}
