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

        // Smell: Feature Envy
        // Smell: Duplicated code
        public char Winner() {
            return WinnerNew();
            if (_board.AreRowPositionsTaken(0) && _board.IsRowTakenBySameSymbol(0)) {
                return _board.TileAt(0, 0).Symbol;
            }
            
            if (_board.AreRowPositionsTaken(1) && _board.IsRowTakenBySameSymbol(1)) {
                return _board.TileAt(1, 0).Symbol;
            }
            
            if (_board.AreRowPositionsTaken(2) && _board.IsRowTakenBySameSymbol(2)) {
                return _board.TileAt(2, 0).Symbol;
            }

            return ' ';
        }

        public char WinnerNew() {
            var tileGroups = _board.AllTileGroups();
            foreach (var tileGroup in tileGroups) {
                if (tileGroup.IsFull && tileGroup.IsTakenBySameSymbol) {
                    return tileGroup.SymbolAt(0);
                }
            }
            
            return ' ';
        }
    }
}