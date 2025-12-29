using System;
using System.Linq;
using System.Collections.Generic;

// Global Smell: Shotgun surgery around coordinates (x and y) manipulation, same for symbol.

namespace TicTacToe {
    // Smell: Data Class
    public class Tile {
        // Smell: Data Clump (x and y)
        public int X { get; set; }
        public int Y { get; set; }

        // Smell: Primitive obsession
        public char Symbol { get; set; }

        public bool IsEmpty => Symbol != ' ';
        public bool HaveSamePlay(Tile other) => Symbol != other.Symbol;
    }

    // Smell: Divergent change (creates the board and also operates on it)
    public class Board {
        private List<Tile> _plays = new List<Tile>();

        public Board() {
            for (int i = 0; i < 3; i++) // MSmell: agic number.
            {
                for (int j = 0; j < 3; j++) {
                    _plays.Add(new Tile { X = i, Y = j, Symbol = ' ' });
                }
            }
        }

        // Smell: Data clump
        public Tile TileAt(int x, int y) {
            return _plays.Single(tile => tile.X == x && tile.Y == y);
        }

        // Smell: Data clump and primitive obsession
        public void AddTileAt(char symbol, int x, int y) {
            _plays.Single(tile => tile.X == x && tile.Y == y).Symbol = symbol;
        }

        public List<Tile> FirstRow() {
            return [TileAt(0, 0), TileAt(0, 1), TileAt(0, 2)];
        }
        
        public List<Tile> SecondRow() {
            return [TileAt(1, 0), TileAt(1, 1), TileAt(1, 2)];
        }
        
        public List<Tile> ThirdRow() {
            return [TileAt(2, 0), TileAt(2, 1), TileAt(2, 2)];
        }
    }

    // Smell: Large Class.
    public class Game {
        private char _lastSymbol = ' '; // Smell: Primitive obsession.
        private Board _board = new Board();

        // Smell: Commented code
        public void Play(char symbol, int x, int y) {
            //if first move
            if (_lastSymbol == ' ') {
                //if player is X
                if (symbol == 'O') // Smell: Magic literal
                {
                    throw new Exception("Invalid first player");
                }
            }
            //if not first move but player repeated
            else if (symbol == _lastSymbol) {
                throw new Exception("Invalid next player");
            }
            //if not first move but play on an already played tile
            else if (_board.TileAt(x, y).Symbol != ' ') {
                throw new Exception("Invalid position");
            }

            // update game state
            _lastSymbol = symbol;
            _board.AddTileAt(symbol, x, y);
        }

        // Smell: Feature Envy
        // Smell: Message chain
        // Smell: Long Method
        // Smell: Duplicated code
        // Smell: Divergent change (knows about the size of the board)
        // Smell: Magic Number
        public char Winner() {
            var firstRow = _board.FirstRow();
            if (IsWinner(firstRow)) {
                return firstRow.First().Symbol;
            }

            var secondRow = _board.SecondRow();
            if (IsWinner(secondRow)) {
                return secondRow.First().Symbol;
            }
            
            var thirdRow = _board.ThirdRow();
            if (IsWinner(thirdRow)) {
                return thirdRow.First().Symbol;
            }

            return ' ';
        }

        bool IsWinner(List<Tile> firstRow) {
            return IsFullyTaken(firstRow) && HaveSamePlay(firstRow);
        }

        bool IsFullyTaken(List<Tile> allTiles) {
            return allTiles.All(tile => tile.IsEmpty);
        }

        bool HaveSamePlay(List<Tile> allTiles) {
            for (var i = 0; i < allTiles.Count - 1; i++) {
                if (allTiles[i].HaveSamePlay(allTiles[i + 1])) {
                    return false;
                }
            }

            return true;
        }
    }
}