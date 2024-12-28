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

        // wishList.WriteAllGamesName(); 


        WriteLine(); 
    }
}


// Reading WishList
// search for this string "wishlist-list-item-1-tile#title"
internal class ReadingWishList
{
    // readonly string filePath = Path.Combine(Environment.CurrentDirectory,"WishlistFiles", "Wishlist copy.html"); 
    readonly string filePath = Path.Combine(Environment.CurrentDirectory,"WishlistFiles", "Wishlist.html"); 
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
            string? htmlFile = reader.ReadToEnd();         

            // (?<=\d+-tile#title">).+?(?=<\/div>)
            // (?<=\d+-tile#title">)(.+?)(&amp)?(.+?)(?=<\/div>)

            string namePattern = 
                @"(?<=\d+-tile#title"">)" +     // Positive lookbehind
                @"(?'GameName'.+?) " +          // To capture game name
                @"(?=<\/div>)";                 // Positive lookahead 

            Regex regex = new Regex(namePattern); 
            MatchCollection namesFound = regex.Matches(htmlFile); 
            int gameCount = 0; 

            WriteLine("Games saved in Wishlist:\n"); 

            foreach(Match match in namesFound)
            {
                string name = match.Groups["GameName"].Value; 
                if (name.Contains("&amp;"))
                {
                    name = name.Remove(name.IndexOf("&amp;"), "&amp; ".Count()); 
                }
                WriteLine("{0,-2}: {1}", ++gameCount, name); 
            }
            WriteLine("\nTotal Games: {0}", gameCount); 
        }
    }

    // (?<=\d+-tile#title">)(.+?)(?=<\/div>)(.+?)(?<=\d+-tile#price#display-price)(.+?)(?<=class="psw-m-r-3">)(.+?)(?=<\/span>)
    // (?<=\d+-tile#title">)(.+?)(?=<\/div>)(.+?)(?<=class="psw-m-r-3">)(.+?)(?=<\/span>)
    // (?<=<li class=)(.+?)(?=<\/li>)
    // (?<=\d+-tile#title">)(.+?)(?=<\/div>)(.+?)(?<=class="psw-m-r-3">)(.+?)(?=<\/span)(.+?)(?=<\/span) -- sort of works

    public List<GameInfo> GetAllGamesInfo()
    {
        using (StreamReader reader = new StreamReader(filePath))
        {
            string? htmlFile = reader.ReadToEnd(); 
            string gameInfoPattern = 
                @"(?<=\d+-tile#title"">)" + 
                @"(?'GameName'.+?)" +           // Group 1 : To capture game name
                @"(?=<\/div>)" + 
                @"(.+?)" +                      // Group 2 
                @"(?<=class=""psw-m-r-3"">)" +
                @"(?'GamePrice'.+?)" +          // Group 3 : To cpature current game price
                @"(?=<\/span)" +
                @"(?'Discount'.+?)" +           // Group 4 : To capure Discount and original price
                @"(?=.<\/span)"; 

            Regex regex = new Regex(gameInfoPattern); 
            MatchCollection gameFound = regex.Matches(htmlFile); 
            List<GameInfo> gameList = new List<GameInfo>(); 

            foreach(Match match in gameFound)
            {
                gameList.Add(new GameInfo(
                    match.Groups["GameName"].Value,
                    match.Groups["GamePrice"].Value,
                    match.Groups["Discount"].Value
                )); 
            }
            return gameList;
        }
    }

}

public class GameInfo
{
    public string Name { get; set; }
    public string? CurrentPrice { get; set; }
    public bool Discounted { get; set; }
    public string? OriginalPrice {get; set; }
    public GameInfo(string name, string price, string originalPrice)
    {
        Name = name; 
        CurrentPrice = price; 
        
        if (originalPrice.Contains("Original price"))
        {
            Discounted = true; 

            OriginalPrice = originalPrice.Substring(
                startIndex: originalPrice.IndexOf("Original price") + 
                "Original price, ".Length
            ); 
        }
    } 
}
