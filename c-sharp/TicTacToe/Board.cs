using System.Collections.Generic;
using System.Linq;

namespace TicTacToe; 

// Smell: Divergent change (creates the board and also operates on it)
public class Board
{
    private List<Tile> _plays = new List<Tile>();
       
    public Board()
    {
        for (int i = 0; i < 3; i++) // MSmell: agic number.
        {
            for (int j = 0; j < 3; j++)
            {
                _plays.Add(new Tile{ X = i, Y = j, Symbol = ' '});
            }  
        }       
    } 
        
    // Smell: Data clump
    public Tile TileAt(int x, int y)
    {
        return _plays.Single(tile => tile.X == x && tile.Y == y);
    }

    // Smell: Data clump and primitive obsession
    public void AddTileAt(char symbol, int x, int y)
    {
        _plays.Single(tile => tile.X == x && tile.Y == y).Symbol = symbol;
    }

    // Smell: Feature envy
    public bool AreRowPositionsTaken(int row) {
        return TileAt(row, 0).Symbol != ' ' &&
               TileAt(row, 1).Symbol != ' ' &&
               TileAt(row, 2).Symbol != ' ';
    }

    // Smell: Feature envy
    public bool IsRowTakenBySameSymbol(int row) {
        return TileAt(row, 0).Symbol ==
               TileAt(row, 1).Symbol &&
               TileAt(row, 2).Symbol ==
               TileAt(row, 1).Symbol;
    }
}