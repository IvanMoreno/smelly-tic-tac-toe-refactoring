using System.Collections.Generic;
using System.Linq;

namespace TicTacToe;

public class TileGroup {
    readonly List<Tile> tiles;

    public bool IsFull => tiles.All(tile => tile.Symbol != ' ');
    public bool IsTakenBySameSymbol => tiles[0].Symbol == tiles[1].Symbol &&  tiles[1].Symbol == tiles[2].Symbol;
    
    public TileGroup(List<Tile> tiles) {
        this.tiles = tiles;
    }
    
    public char SymbolAt(int position) => tiles[position].Symbol;
}