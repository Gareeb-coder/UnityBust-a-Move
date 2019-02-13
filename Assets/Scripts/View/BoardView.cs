﻿using UnityEngine;
using BAMEngine;
using System.Linq;
using System.Collections.Generic;

public class BoardView : MonoBehaviour
{
    public Board board { get; private set; }
    public List<PieceView> viewPieces { get; private set; } = new List<PieceView>();
    public GameView gameView { get; private set; }

    public void Initiate(GameView gameView)
    {
        this.gameView = gameView;
        board = gameView.gameEngine.board;
        DrawBoard();
    }

    private void DrawBoard()
    {
        for (int i = 0; i < board.lines.Count; i++)
        {
            var line = board.lines[i];
            for (int j = 0; j < line.Count; j++)
            {
                if (line[j] == null) continue;
                var piece = GameObject.Instantiate(gameView.piecePrefab, new Vector3(j + (line.IsShortLine ? 0.5f : 0f), -i, 0f), Quaternion.identity).AddComponent<PieceView>();
                piece.Initiate(this, line[j]);
                viewPieces.Add(piece);
            }
        }
    }

    public PieceView GetPiece(Piece piece)
    {
        return GetPiece(piece.Line.Index, piece.Index);
    }

    public PieceView GetPiece(int line, int position)
    {
        return viewPieces.FirstOrDefault(x => x.piece.Line.Index == line && x.piece.Index == position);
    }

    public void PlacePiece(PieceView piece)
    {
        viewPieces.Add(piece);
    }
}
