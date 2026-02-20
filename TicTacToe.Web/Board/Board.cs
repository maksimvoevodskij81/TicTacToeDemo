using TicTacToe.Domain.Board.Abstractions;
using TicTacToe.Domain.Board.Contracts;
using TicTacToe.Domain.Game.Contracts;
using TicTacToe.Domain.Board.Constants;

namespace TicTacToe.Web.Board
{
    public class Board : IBoard
    {
        private PlayerSymbol[,] _board;
        public Board()
        {
            _board = new PlayerSymbol[BoardConstants.BoardSize, BoardConstants.BoardSize];
            BuildBoard();
        }
        public PlayerSymbol? CheckWinner()
        {
            for (int lineIndex = 0; lineIndex < BoardConstants.WinningLines.GetLength(0); lineIndex++)
            {
                int aLineIndex = BoardConstants.WinningLines[lineIndex, 0];
                int bLineIndex = BoardConstants.WinningLines[lineIndex, 1];
                int cLineIndex = BoardConstants.WinningLines[lineIndex, 2];

                PlayerSymbol a = _board[aLineIndex / BoardConstants.BoardSize, aLineIndex % BoardConstants.BoardSize];
               
                if (a == PlayerSymbol.Empty)
                {
                    continue;
                }

                PlayerSymbol b = _board[bLineIndex / BoardConstants.BoardSize, bLineIndex % BoardConstants.BoardSize];
                PlayerSymbol c = _board[cLineIndex / BoardConstants.BoardSize, cLineIndex % BoardConstants.BoardSize];

                if (a == b && a == c) 
                {
                    return a;
                }
            }

            return null;
        }

        public PlayerSymbol[] GetBoard()
        {
            return Flat2DBoard();
        }

        public bool IsFull()
        {
            for (int rowIndex = 0; rowIndex < BoardConstants.BoardSize; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < BoardConstants.BoardSize; columnIndex++)
                {
                    if (_board[rowIndex, columnIndex] == PlayerSymbol.Empty)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public PlaceCellResult TryPlace(int row, int column, PlayerSymbol symbol)
        {
            if(row < 0 || row >= BoardConstants.BoardSize)
            {
                return new PlaceCellResult(false, "OutOfBounds", $"Row {row} is out of bounds (0..{BoardConstants.BoardSize - 1}).");
            }

            if (column < 0 || column >= BoardConstants.BoardSize)
            {
                return new PlaceCellResult(false, "OutOfBounds", $"Column {column} is out of bounds (0..{BoardConstants.BoardSize - 1}).");
            }


            if (_board[row, column] != PlayerSymbol.Empty)
            {
                return new PlaceCellResult(false, "CellOccupied", $"This cell row:{row} - column {column} is already busy with value:{_board[row, column]}");
            }

            _board[row, column] = symbol;

            return new PlaceCellResult(true, "PlacedSuccessfully", "Cell placed successfully");
        }

        private bool BuildBoard()
        {
            for (int rowIndex = 0; rowIndex < BoardConstants.BoardSize; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < BoardConstants.BoardSize; columnIndex++)
                {
                    _board[rowIndex, columnIndex] = PlayerSymbol.Empty;
                }
            }
            return true;
        }

        private PlayerSymbol[] Flat2DBoard()
        {
            return _board.Cast<PlayerSymbol>().ToArray();
        }
    }
}
