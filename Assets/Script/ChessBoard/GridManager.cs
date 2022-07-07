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

    public List<GameObject> boardList = new List<GameObject>();
    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;

        GenerateGrid();
    }


    private bool GenerateGrid()
    {
        for(int y = 0;y < height;++y)
        {
            for(int x = 0;x < width;++x)
            {
                if((y % 2 == 0 && x % 2 != 0) || (y % 2 != 0 && x % 2 ==0))
                {
                    var spawnBlack = Instantiate(blackBoard, new Vector3(x, y), Quaternion.identity,gameObject.transform);
                    spawnBlack.name = $"Board{x}{y}";
                    boardList.Add(spawnBlack);
                }
                else
                {
                    var spawnWhite = Instantiate(whiteBoard, new Vector3(x, y), Quaternion.identity, gameObject.transform);
                    spawnWhite.name = $"Board{x}{y}";
                    boardList.Add(whiteBoard);
                }
            }
        }

        transform.localScale = new Vector3(boardScale, boardScale, 1);
        mainCam.transform.position = new Vector3(((float)width / 2 - 0.5f) * boardScale, ((float)height / 2 - 0.5f) * boardScale, -10f);

        return true;
    }
}
