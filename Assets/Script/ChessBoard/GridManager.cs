using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : Singleton<GridManager>
{
    [SerializeField] int width, height;
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

        mainCam.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10f);

        return true;
    }
}
