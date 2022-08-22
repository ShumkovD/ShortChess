using System.Collections.Generic;
using UnityEngine;

public class King : ChessPiece
{
    public override List<Vector2Int> GetAvailableMoves(ref ChessPiece[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> returnValue = new List<Vector2Int>();

        //下
        if (currentY - 1 >= 0)
        {
            if (board[currentX, currentY - 1] == null)
                returnValue.Add(new Vector2Int(currentX, currentY - 1));
            else
                if (board[currentX, currentY - 1].team != team)
                    returnValue.Add(new Vector2Int(currentX, currentY - 1));
        }
        //上
        if (currentY + 1 < tileCountY)
        {
            if (board[currentX, currentY + 1] == null)
                returnValue.Add(new Vector2Int(currentX, currentY + 1));
            else
                if (board[currentX, currentY + 1].team != team)
                    returnValue.Add(new Vector2Int(currentX, currentY + 1));
        }
        //右
        if (currentX + 1 < tileCountX)
        {
            if (board[currentX + 1, currentY] == null)
                returnValue.Add(new Vector2Int(currentX + 1, currentY));
            else
                if(board[currentX + 1, currentY].team != team)
                    returnValue.Add(new Vector2Int(currentX + 1, currentY));
            //右上
            if (currentY + 1 < tileCountY)
            {
                if (board[currentX + 1, currentY + 1] == null)
                    returnValue.Add(new Vector2Int(currentX + 1, currentY + 1));
                else
               if (board[currentX + 1, currentY + 1].team != team)
                    returnValue.Add(new Vector2Int(currentX + 1, currentY + 1));
            }
            //右下
            if (currentY - 1 >= 0)
            {
                if (board[currentX + 1, currentY - 1] == null)
                    returnValue.Add(new Vector2Int(currentX + 1, currentY - 1)); 
                else
               if (board[currentX + 1, currentY - 1].team != team)
                    returnValue.Add(new Vector2Int(currentX + 1, currentY - 1));
            }

        }

        //左
        if (currentX - 1 >= 0)
        {
            if (board[currentX - 1, currentY] == null)
                returnValue.Add(new Vector2Int(currentX - 1, currentY));
            else
                if (board[currentX - 1, currentY].team != team)
                returnValue.Add(new Vector2Int(currentX - 1, currentY));
            //右上
            if (currentY + 1 < tileCountY)
            {
                if (board[currentX - 1, currentY + 1] == null)
                    returnValue.Add(new Vector2Int(currentX - 1, currentY + 1));
                else
               if (board[currentX - 1, currentY + 1].team != team)
                    returnValue.Add(new Vector2Int(currentX - 1, currentY + 1));
            }
            //右下
            if (currentY - 1 >= 0)
            {
                if (board[currentX - 1, currentY - 1] == null)
                    returnValue.Add(new Vector2Int(currentX - 1, currentY - 1));
                else
               if (board[currentX - 1, currentY - 1].team != team)
                    returnValue.Add(new Vector2Int(currentX - 1, currentY - 1));
            }

        }

        return returnValue;
    }
}
