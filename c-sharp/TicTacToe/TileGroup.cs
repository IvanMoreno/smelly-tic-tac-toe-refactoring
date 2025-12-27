using System.Collections.Generic;
using System.Linq;

namespace TicTacToe;

public class TileGroup {
    readonly List<Tile> tiles;

    public bool IsTakenBySameSymbol => IsFull && tiles[0].Symbol == tiles[1].Symbol &&  tiles[1].Symbol == tiles[2].Symbol;
    bool IsFull => tiles.All(tile => tile.Symbol != ' ');
    
    public TileGroup(List<Tile> tiles) {
        this.tiles = tiles;
    }
    
    public char SymbolAt(int position) => tiles[position].Symbol;
}