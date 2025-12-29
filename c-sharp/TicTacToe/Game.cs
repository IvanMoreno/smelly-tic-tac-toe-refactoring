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

        Row FirstRow() {
            return [TileAt(0, 0), TileAt(0, 1), TileAt(0, 2)];
        }
        
        Row SecondRow() {
            return [TileAt(1, 0), TileAt(1, 1), TileAt(1, 2)];
        }
        
        Row ThirdRow() {
            return [TileAt(2, 0), TileAt(2, 1), TileAt(2, 2)];
        }

        public List<Row> AllRows() {
            return [FirstRow(), SecondRow(), ThirdRow()];
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
        // Smell: Magic Number
        public char Winner() {
            var allRows = _board.AllRows();
            if (!allRows.Any(IsWinner))
                return ' ';

            return allRows.First(IsWinner).First().Symbol;
        }

        bool IsWinner(Row firstRow) {
            return IsFullyTaken(firstRow) && HaveSamePlay(firstRow);
        }

        bool IsFullyTaken(Row allTiles) {
            return allTiles.All(tile => tile.IsEmpty);
        }

        bool HaveSamePlay(Row allTiles) {
            for (var i = 0; i < allTiles.Count - 1; i++) {
                if (allTiles[i].HaveSamePlay(allTiles[i + 1])) {
                    return false;
                }
            }

            return true;
        }
    }

    public class Row : List<Tile> {
        
    }
}