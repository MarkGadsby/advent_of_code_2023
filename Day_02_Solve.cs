class SolveDayTwo : SolutionsBase
{   
    public SolveDayTwo(StreamReader given_sr) : base(given_sr)
    { 
    }

    public override void part_one()
    {
        int count = 0;
        while (!stream_handle.EndOfStream)
        {
            Game game = new Game(stream_handle.ReadLine());
            count += game.gameIdIfPossible();
        }

        Console.WriteLine($"\t\tIf you add up the IDs of the games that would have been possible, you get: {count}.\n");
    }
    public override void part_two()
    {
        resetStream();

        int count = 0;
        while (!stream_handle.EndOfStream)
        {
            Game game = new Game(stream_handle.ReadLine());
            count += game.powerOfFewestColours();
        }

        Console.WriteLine($"\t\tThe sum of the power of the minimum set of cubes that must have been present is: {count}.\n");
    }    
}

class Game
{
    private int gameID;
    List<Handfull> listOfHandfulls = new List<Handfull>();

    public Game(string game)
    {
        string[] gameData = game.Split(": ");

        string[] gameMeta = gameData[0].Split(' ');
        gameID = int.Parse(gameMeta[1]);

        string[] handfulls = gameData[1].Split("; ");

        foreach (string handfull in handfulls)
        {
            listOfHandfulls.Add(new Handfull(handfull));
        }
    }

    public int gameIdIfPossible()
    {
        bool validGame = true;
        
        foreach (Handfull hf in listOfHandfulls)
        {
            if (!hf.AreYouPossible())
                validGame = false;
        }

        if (validGame == true)
            return gameID;
        else
            return 0;
    }

    public int powerOfFewestColours()
    {
        int fewestReds = 0;
        int fewestGreens = 0;
        int fewestBlues = 0;

        foreach (Handfull hf in listOfHandfulls)
        {
            if (hf.GetnReds() > fewestReds)
                fewestReds = hf.GetnReds();

            if (hf.GetnGreens() > fewestGreens)
                fewestGreens = hf.GetnGreens();
            
            if (hf.GetnBlues() > fewestBlues)
                fewestBlues = hf.GetnBlues();
        }
        return fewestReds * fewestGreens * fewestBlues;
    }

    public void Show()
    {
        Console.WriteLine($"gameID = {gameID}");
        Console.WriteLine();

        foreach (Handfull hf in listOfHandfulls)
        {
            hf.Show();
        }
        Console.WriteLine();
    }
}

class Handfull
{   
    private int nRed;
    private int nGreen;
    private int nBlue;

    public int GetnReds(){return nRed;}
    public int GetnGreens(){return nGreen;}
    public int GetnBlues(){return nBlue;}

    public bool AreYouPossible()
    {
        bool retVal = false;            

        if (nRed <= 12 && nGreen <= 13 && nBlue <= 14)
            retVal = true;

        return retVal;
    }

    public Handfull(string handfull)
    {
        string[] colours = handfull.Split(", ");
        
        foreach (string colour in colours)
        {
            string[] colourDetail = colour.Split(' ');

            int numberOfDigits = 0;

            switch (colourDetail[1])
            {
                case "red":
                {
                    nRed = int.Parse(colourDetail[0]);
                    break;
                }
                case "green":
                {
                    nGreen = int.Parse(colourDetail[0]);
                    break;
                }
                case "blue":
                {
                    nBlue = int.Parse(colourDetail[0]);
                    break;
                }
            }
        }
    }
    public void Show()
    {
        Console.WriteLine($"\tRed = {nRed}");
        Console.WriteLine($"\tGreen = {nGreen}");
        Console.WriteLine($"\tBlue = {nBlue}");
        Console.WriteLine($"\tAreYouPossible() = {AreYouPossible()}\n");
    }
}