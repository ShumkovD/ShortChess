using System.Collections.Generic;
using UnityEngine;

public class Queen : ChessPiece
{
    public override List<Vector2Int> GetAvailableMoves(ref ChessPiece[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> returnValue = new List<Vector2Int>();

        //右上
        for (int x = currentX + 1, y = currentY + 1; x < tileCountX && y < tileCountY; x++, y++)
        {
            if (board[x, y] == null)
                returnValue.Add(new Vector2Int(x, y));
            else
            {
                if (board[x, y].team != team)
                    returnValue.Add(new Vector2Int(x, y));
                break;
            }
        }
        //右下
        for (int x = currentX + 1, y = currentY - 1; x < tileCountX && y >= 0; x++, y--)
        {
            if (board[x, y] == null)
                returnValue.Add(new Vector2Int(x, y));
            else
            {
                if (board[x, y].team != team)
                    returnValue.Add(new Vector2Int(x, y));
                break;
            }
        }
        //左上
        for (int x = currentX - 1, y = currentY + 1; x >= 0 && y < tileCountY; x--, y++)
        {
            if (board[x, y] == null)
                returnValue.Add(new Vector2Int(x, y));
            else
            {
                if (board[x, y].team != team)
                    returnValue.Add(new Vector2Int(x, y));
                break;
            }
        }
        //左上
        for (int x = currentX - 1, y = currentY - 1; x >= 0 && y >= 0; x--, y--)
        {
            if (board[x, y] == null)
                returnValue.Add(new Vector2Int(x, y));
            else
            {
                if (board[x, y].team != team)
                    returnValue.Add(new Vector2Int(x, y));
                break;
            }
        }
        //下
        for (int i = currentY - 1; i >= 0; i--)
        {
            if (board[currentX, i] == null)
                returnValue.Add(new Vector2Int(currentX, i));
            else
            {
                if (board[currentX, i].team != team)
                    returnValue.Add(new Vector2Int(currentX, i));
                break;
            }
        }
        //上
        for (int i = currentY + 1; i < tileCountY; i++)
        {
            if (board[currentX, i] == null)
                returnValue.Add(new Vector2Int(currentX, i));
            else
            {
                if (board[currentX, i].team != team)
                    returnValue.Add(new Vector2Int(currentX, i));
                break;
            }
        }
        //右
        for (int i = currentX + 1; i < tileCountX; i++)
        {
            if (board[i, currentY] == null)
                returnValue.Add(new Vector2Int(i, currentY));
            else
            {
                if (board[i, currentY].team != team)
                    returnValue.Add(new Vector2Int(i, currentY));
                break;
            }
        }
        //左
        for (int i = currentX - 1; i >= 0; i--)
        {
            if (board[i, currentY] == null)
                returnValue.Add(new Vector2Int(i, currentY));
            else
            {
                if (board[i, currentY].team != team)
                    returnValue.Add(new Vector2Int(i, currentY));
                break;
            }
        }

        return returnValue;
    }
}
