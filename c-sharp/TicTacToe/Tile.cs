namespace TicTacToe;

// Smell: Data Class
public class Tile
{
    // Smell: Data Clump (x and y)
    public int X {get; set;}
    public int Y {get; set;}
        
    // Smell: Primitive obsession
    public char Symbol {get; set;}
}