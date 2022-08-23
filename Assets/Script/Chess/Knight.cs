using System.Collections.Generic;
using UnityEngine;

public class Knight : ChessPiece
{
    public override List<Vector2Int> GetAvailableMoves(ref ChessPiece[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> returnValue = new List<Vector2Int>();

        //�E��1

        int x = currentX + 1;
        int y = currentY + 2;

        if (x < tileCountX && y < tileCountY)
            if (board[x, y] == null || board[x, y].team != team)
               returnValue.Add(new Vector2Int(x, y));
        //�E��2
        x = currentX + 2;
        y = currentY + 1;

        if (x < tileCountX && y < tileCountY)
            if (board[x, y] == null || board[x, y].team != team)
                returnValue.Add(new Vector2Int(x, y));

        //����1

        x = currentX - 1;
        y = currentY + 2;

        if (x >= 0 && y < tileCountY)
            if (board[x, y] == null || board[x, y].team != team)
                returnValue.Add(new Vector2Int(x, y));
        //����2
        x = currentX - 2;
        y = currentY + 1;

        if (x >= 0 && y < tileCountY)
            if (board[x, y] == null || board[x, y].team != team)
                returnValue.Add(new Vector2Int(x, y));

        //����1

        x = currentX - 1;
        y = currentY - 2;

        if (x >= 0 && y >= 0)
            if (board[x, y] == null || board[x, y].team != team)
                returnValue.Add(new Vector2Int(x, y));
        //����1
        x = currentX - 2;
        y = currentY - 1;

        if (x >= 0 && y >= 0)
            if (board[x, y] == null || board[x, y].team != team)
                returnValue.Add(new Vector2Int(x, y));

        //�E��1

        x = currentX + 1;
        y = currentY - 2;

        if (x < tileCountX && y >= 0)
            if (board[x, y] == null || board[x, y].team != team)
                returnValue.Add(new Vector2Int(x, y));
        //�E��2
        x = currentX + 2;
        y = currentY - 1;

        if (x < tileCountX && y >= 0)
            if (board[x, y] == null || board[x, y].team != team)
                returnValue.Add(new Vector2Int(x, y));

        return returnValue;
    }
    public override List<Vector2Int> GetAvailablePrepMoves(ref ChessPiece[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> returnValue = new List<Vector2Int>();

        int teamY = team == 0 ? 0 : 5;

        for (int x = 0; x < tileCountX; x++)
            if(board[x, teamY] == null)
            {
                returnValue.Add(new Vector2Int(x, teamY));
            }

        return returnValue;
    }

}
