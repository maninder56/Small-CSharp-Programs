
using System.Runtime.CompilerServices;

namespace GameLibrary;

public class Player
{
    public string Name { get; set; }
    static Random random = new Random(); 
    public readonly Hand hand = (Hand)random.Next(0,3);

    public Player(string name) => this.Name = name;

}
