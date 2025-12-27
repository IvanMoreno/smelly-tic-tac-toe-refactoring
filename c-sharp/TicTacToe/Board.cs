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

    public IEnumerable<TileGroup> AllTileGroups() {
        yield return new TileGroup([TileAt(0, 0), TileAt(0, 1), TileAt(0, 2)]);
        yield return new TileGroup([TileAt(1, 0), TileAt(1, 1), TileAt(1, 2)]);
        yield return new TileGroup([TileAt(2, 0), TileAt(2, 1), TileAt(2, 2)]);
    }
}