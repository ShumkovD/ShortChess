using System.Collections.Generic;
using UnityEngine;

public class Rook : ChessPiece
{
    public override List<Vector2Int> GetAvailableMoves(ref ChessPiece[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> returnValue = new List<Vector2Int>();

        //â∫
        for(int i = currentY-1;i>=0;i--)
        {
            if (board[currentX, i] == null)
                returnValue.Add(new Vector2Int(currentX, i));
            else
            {
                if(board[currentX, i].team != team)
                    returnValue.Add(new Vector2Int(currentX, i));
                break;
            }
        }
        //è„
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
        //âE
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
        //ç∂
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

    public override List<Vector2Int> GetAvailablePrepMoves(ref ChessPiece[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> returnValue = new List<Vector2Int>();
        int teamY = team == 0 ? 0 : 5;
        for (int x = 0; x < tileCountX; x++)
            if (board[x, teamY] == null)
            {
                returnValue.Add(new Vector2Int(x, teamY));
            }

        return returnValue;
    }

}
