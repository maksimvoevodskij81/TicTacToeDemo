
using TicTacToe.Domain.Board.Contracts;
using TicTacToe.Domain.Game.Contracts;

namespace TicTacToe.Domain.Board.Abstractions
{
    public interface IBoard
    {
        public PlaceCellResult TryPlace(int row, int column, PlayerSymbol symbol);
        public PlayerSymbol? CheckWinner();
        public bool IsFull();
        public PlayerSymbol[] GetBoard();
    }
}
