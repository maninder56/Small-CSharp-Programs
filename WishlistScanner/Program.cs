using System.Text.RegularExpressions;
using System.Xml.Linq;
using static System.Console; 

internal class Program
{
    private static void Main(string[] args)
    {
        Clear(); 
        WriteLine(new string('-',WindowWidth)); 
        WriteLine(); 

        ReadingWishList wishList = new ReadingWishList(); 

        wishList.WriteAllGamesName(); 


        

    }
}


// Reading WishList
// search for this string "wishlist-list-item-1-tile#title"
internal class ReadingWishList
{
    // readonly string filePath = Path.Combine(Environment.CurrentDirectory,"WishlistFiles", "Wishlist copy.html"); 
    readonly string filePath = Path.Combine(Environment.CurrentDirectory,"WishlistFiles", "test.html"); 
    public bool doesFileExist { get; }

    public ReadingWishList()
    {
        doesFileExist = File.Exists(filePath); 

        if (!doesFileExist)
        {
            throw new FileNotFoundException("File Does not Exists"); 
        }
    }

    public void WriteWishlistFile()
    {
        using (StreamReader reader = new StreamReader(filePath))
        {
            WriteLine(reader?.ReadToEnd()); 
        }
    }


    public void WriteAllGamesName()
    {
        using (StreamReader reader = new StreamReader(filePath)) 
        {
            string? line = reader.ReadToEnd();         

            
            
        }
    }


}
