using System.Collections.Generic;
using UnityEngine;

public class Bishop : ChessPiece
{
    public override List<Vector2Int> GetAvailableMoves(ref ChessPiece[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> returnValue = new List<Vector2Int>();

        //âEè„
        for(int x = currentX+1, y = currentY+1; x < tileCountX && y< tileCountY; x++, y++)
        {
            if(board[x, y] == null)
                returnValue.Add(new Vector2Int(x, y));
            else
            {
                if(board[x,y].team != team)
                    returnValue.Add(new Vector2Int(x, y));
                break;
            }
        }
        //âEâ∫
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

        //ç∂è„
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

        //ç∂è„
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

        return returnValue;
    }

    public override List<Vector2Int> GetAvailablePrepMoves(ref ChessPiece[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> returnValue = new List<Vector2Int>();
        for (int i = 0; i < tileCountX; i++)
            if (board[i, 0] == null)
                returnValue.Add(new Vector2Int(i, 0));

        return returnValue;
    }

}
