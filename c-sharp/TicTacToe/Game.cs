using System;

// Global Smell: Shotgun surgery around coordinates (x and y) manipulation, same for symbol.

namespace TicTacToe {
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
        // Smell: Complicated boolean expression
        // Smell: Feature Envy
        // Smell: Complicated boolean expression
        // Smell: Message chain
        // Smell: Long Method
        // Smell: Duplicated code
        public char Winner() {
            //if the positions in first row are taken
            if (_board.AreRowPositionsTaken(0) && _board.IsRowTakenBySameSymbol(0)) {
                //if first row is full with same symbol
                return _board.TileAt(0, 0).Symbol;
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
    }
}