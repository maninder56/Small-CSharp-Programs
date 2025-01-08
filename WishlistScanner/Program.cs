using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Net;
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

        wishList.WriteAllGamesInfo();


        WriteLine(); 
    }
}


// Reading WishList
// search for this string "wishlist-list-item-1-tile#title"
internal class ReadingWishList
{
    // readonly string filePath = Path.Combine(Environment.CurrentDirectory,"WishlistFiles", "Wishlist copy.html"); 
    readonly string filePath = Path.Combine(Environment.CurrentDirectory,"WishlistFiles", "Wishlist8.html"); 
    public bool doesFileExist { get; }
    private List<GameInfo>? gameList { get; set; }
    public ReadingWishList()
    {
        doesFileExist = File.Exists(filePath); 

        if (!doesFileExist)
        {
            throw new FileNotFoundException("File Does not Exists"); 
        }

        gameList ??= GetAllGamesInfo();
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
                @"(?'GameName'.+?)" +          // To capture game name
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

    private List<GameInfo> GetAllGamesInfo()
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
                    match.Groups["GameName"].Value.Trim(),
                    match.Groups["GamePrice"].Value.Trim(),
                    match.Groups["Discount"].Value.Trim()
                )); 
            }
            return gameList;
        }
    }

    public void WriteAllGamesInfo()
    {
        List<GameInfo>? localGameList = gameList;
        localGameList ??= GetAllGamesInfo();
        
        int highestGameNameLength = localGameList.Max(g => g.Name.Length);
        WriteLine("      {0} {1,-20} {2,-10} {3}", "Name".PadRight(highestGameNameLength), "Current Price", "Discount", "Original Price"); 
        int gameCount = 0; 
        foreach(GameInfo game in localGameList)
        {
            WriteLine("{0,-4}: {1} {2,-20} {3,-10} {4}",
                ++gameCount, game.Name.PadRight(highestGameNameLength), game.CurrentPrice, game.Discounted, game.OriginalPrice); 
        }

        WriteLine("\nTotal Games saved in Wishlist: {0}", localGameList.Count); 
        WriteLine("\nCurrent Cost of All Games: £{0}", localGameList.Sum(c => c.CurrentPrice)); 

        IEnumerable<GameInfo> cheapestGames = localGameList.
            Where(g => g.CurrentPrice == localGameList.Min(g => g.CurrentPrice));
        WriteLine(); 
        foreach(GameInfo game in cheapestGames)
        {
            WriteLine("Cheapest Game : [{0}] {1} at £{2}", localGameList.IndexOf(game) +1, game.Name, game?.CurrentPrice); 
        }

        WriteLine(); 
        GameInfo? highestSavingGame = GetHighestSavingsGame(); 
        WriteLine("Game that provides highest Savings: {0} at £{1}, Total Savings: £{2}", 
            highestSavingGame?.Name, highestSavingGame?.CurrentPrice, 
            highestSavingGame?.OriginalPrice - highestSavingGame?.CurrentPrice); 

        decimal? gamesOnSalePrice = localGameList
            .Where(g => g.Discounted)
            .Sum(g => g.CurrentPrice); 
        decimal? gamesOnSaleOriginalPrice = localGameList
            .Where(g => g.Discounted)
            .Sum(g => g.OriginalPrice); 
        WriteLine(); 
        WriteLine("Current Cost of All Games on Sale: £{0}, Total Savigns: £{1}, Total Without Sale: £{2}", 
            gamesOnSalePrice, 
            gamesOnSaleOriginalPrice - gamesOnSalePrice,
            gamesOnSaleOriginalPrice); 
    }

    GameInfo? GetHighestSavingsGame()
    {
        List<GameInfo>? localGameList = gameList;
        localGameList ??= GetAllGamesInfo();
        
        GameInfo? highestSavingGame = null; 
        decimal? mostSavings = 0; 

        foreach(GameInfo game in localGameList)
        {
            if (game.Discounted)
            {
                decimal? totalSavingsFromThisGame = game.OriginalPrice - game.CurrentPrice; 
                if (totalSavingsFromThisGame > mostSavings)
                {
                    mostSavings = totalSavingsFromThisGame; 
                    highestSavingGame = game; 
                }
            }
        }
        return highestSavingGame; 
    }
}

public class GameInfo
{
    public string Name { get; set; }
    public decimal? CurrentPrice { get; set; }
    public bool Discounted { get; set; }
    public decimal? OriginalPrice {get; set; }
    string? originalPriceText { get; set;}
    public GameInfo(string name, string price, string originalPrice)
    {
        Name = name.Contains("&amp;") ? 
            name.Remove(name.IndexOf("&amp;"), "&amp;".Count()) : 
            name; 

        if (decimal.TryParse(price, NumberStyles.Currency, CultureInfo.GetCultureInfo("en-GB"), out _))
        {
            CurrentPrice = decimal.Parse(price, NumberStyles.Currency, CultureInfo.GetCultureInfo("en-GB")); 
        }
        
        if (originalPrice.Contains("Original price"))
        {
            Discounted = true; 

            originalPriceText = originalPrice.Substring(
                startIndex: originalPrice.IndexOf("Original price") + 
                "Original price, ".Length
            ); 

            if (decimal.TryParse(originalPriceText, NumberStyles.Currency, CultureInfo.GetCultureInfo("en-GB"), out _))
            {
                OriginalPrice = decimal.Parse(originalPriceText, 
                    NumberStyles.Currency, CultureInfo.GetCultureInfo("en-GB")); 
            }

        }
    } 
}
