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

        // Smell: Commented code
        // Smell: Feature Envy
        // Smell: Complicated boolean expression
        // Smell: Message chain
        // Smell: Long Method
        // Smell: Duplicated code
        // Smell: Divergent change (knows about the size of the board)
        public char Winner() {
            //if the positions in first row are taken
            var tile0 = _board.TileAt(0, 0);
            var tile1 = _board.TileAt(0, 1);
            var tile2 = _board.TileAt(0, 2);
            if (tile0.IsEmpty && tile1.IsEmpty && tile2.IsEmpty) {
                //if first row is full with same symbol
                if (AreAllTilesEqual(tile0, tile1, tile2)) {
                    return tile0.Symbol;
                }
            }

            //if the positions in first row are taken
            if (_board.TileAt(1, 0).Symbol != ' ' &&
                _board.TileAt(1, 1).Symbol != ' ' &&
                _board.TileAt(1, 2).Symbol != ' ') {
                //if middle row is full with same symbol
                if (_board.TileAt(1, 0).Symbol ==
                    _board.TileAt(1, 1).Symbol &&
                    _board.TileAt(1, 2).Symbol ==
                    _board.TileAt(1, 1).Symbol) {
                    return _board.TileAt(1, 0).Symbol;
                }
            }

            //if the positions in first row are taken
            if (_board.TileAt(2, 0).Symbol != ' ' &&
                _board.TileAt(2, 1).Symbol != ' ' &&
                _board.TileAt(2, 2).Symbol != ' ') {
                //if middle row is full with same symbol
                if (_board.TileAt(2, 0).Symbol ==
                    _board.TileAt(2, 1).Symbol &&
                    _board.TileAt(2, 2).Symbol ==
                    _board.TileAt(2, 1).Symbol) {
                    return _board.TileAt(2, 0).Symbol;
                }
            }

            return ' ';
        }

        static bool AreAllTilesEqual(Tile tile0, Tile tile1, Tile tile2) {
            return tile0.Symbol ==
                   tile1.Symbol &&
                   tile2.Symbol ==
                   tile1.Symbol;
        }
    }
}