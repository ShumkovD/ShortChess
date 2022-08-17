using System.Collections.Generic;
using UnityEngine;

public class Pawn : ChessPiece
{
    public override List<Vector2Int> GetAvailableMoves(ref ChessPiece[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> returnValue = new List<Vector2Int>();

        int direction = (team == 0) ? 1 : -1;
        if(board[currentX,currentY+direction] == null)
            returnValue.Add(new Vector2Int(currentX, currentY+direction));

        if (currentX != tileCountX - 1)
            if (board[currentX + 1, currentY + direction] != null && board[currentX + 1, currentY + direction].team != team)
                returnValue.Add(new Vector2Int(currentX + 1, currentY + direction));

        if (currentX != 0)
            if (board[currentX - 1, currentY + direction] != null && board[currentX - 1, currentY + direction].team != team)
                returnValue.Add(new Vector2Int(currentX - 1, currentY + direction));

        return returnValue;
    }
}
