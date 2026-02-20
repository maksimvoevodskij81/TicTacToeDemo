namespace TicTacToe.Domain.Board.Constants
{
    public static class BoardConstants
    {
        public const int BoardSize = 3;
        public const int CellCount = BoardSize * BoardSize;
        public static readonly int[,] WinningLines = new int[8, 3]
        {
            // Rows
            { 0, 1, 2 },
            { 3, 4, 5 },
            { 6, 7, 8 },

            // Columns
            { 0, 3, 6 },
            { 1, 4, 7 },
            { 2, 5, 8 },

            // Diagonals
            { 0, 4, 8 },
            { 2, 4, 6 }
        };
    }
}
