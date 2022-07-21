using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    #region MouseClick
    [SerializeField] GameObject highLight;
    [SerializeField] GameObject select;
    GameObject selected;
    bool isSelected = false;
    int selectedNum;


    private void OnMouseEnter()
    {
        if (GridManager.Instance.boardList.TryGetValue(int.Parse(transform.name), out var board))
        {
            Debug.Log(board.transform.name);
            selected = board;
        }
        highLight.SetActive(true);
        selectedNum = int.Parse(gameObject.name);
    }
    private void OnMouseExit()
    {
        highLight.SetActive(false);
    }
    #endregion

    public struct TileInfo
    {
       public bool isPieceOn;//駒がのっているか
       public bool isMove;//移動できるタイルか
       public string chessName;//のっている駒の名前
    }
    //TileInfo tile = new TileInfo();

    private void Start()
    {
        highLight.SetActive(false);
        select.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log(selected.transform.position);
            //タイルに駒が乗っていたら
            //if (tile.isPieceOn)
            //{

                //タイル選択
                Select();
            //}
        }
    }

    //選択
    private void Select()
    {
        if (!isSelected)
        {
            isSelected = true;
        }
        else
        {
            isSelected = false;
        }
        
        select.SetActive(isSelected);

        if(gameObject != selected)
        {
            select.SetActive(false);
        }
    }

    //移動
    private void Move()
    {
        
    }
}
