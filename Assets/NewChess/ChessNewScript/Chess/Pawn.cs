using System.Collections.Generic;
using UnityEngine;

public class Pawn : ChessPiece
{
    public override List<Vector2Int> GetAvailableMoves(ref ChessPiece[,] board, int tileCountX, int tileCountY)
    {
        List<Vector2Int> returnValue = new List<Vector2Int>();

        int direction = (team == 0) ? 1 : -1;

        if (currentY + direction < tileCountY && currentY + direction >= 0)
        {
            if (board[currentX, currentY + direction] == null)
                returnValue.Add(new Vector2Int(currentX, currentY + direction));

            if (currentX != tileCountX - 1)
                if (board[currentX + 1, currentY + direction] != null && board[currentX + 1, currentY + direction].team != team)
                    returnValue.Add(new Vector2Int(currentX + 1, currentY + direction));

            if (currentX != 0)
                if (board[currentX - 1, currentY + direction] != null && board[currentX - 1, currentY + direction].team != team)
                    returnValue.Add(new Vector2Int(currentX - 1, currentY + direction));
        }
        return returnValue;
    }

    public override SpecialMove GetSpecialMoves(ref ChessPiece[,] chessPieces, ref List<Vector2Int[]> moveList, ref List<Vector2Int> availableMoves)
    {
        int direction = (team == 0) ? 1 : -1;
        if ((team == 0 && currentY == 4) || (team == 1 && currentY == 1))
        {
            Debug.Log("Called Promotion");
            return SpecialMove.Promotion;
        }

        return SpecialMove.None;
    }

}
