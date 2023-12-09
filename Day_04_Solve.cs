class SolveDayFour : SolutionsBase
{   
    public SolveDayFour(StreamReader given_sr) : base(given_sr)
    { 
    }

    public override void part_one()
    {       
        int count = 0; 
        while (!stream_handle.EndOfStream)
        {
            Card card = new Card(stream_handle.ReadLine());
            count += card.GetCardScore();
        }

        Console.WriteLine($"\t\tThe number of points the scratchcards are they worth are: {count}\n");
    }

    public override void part_two()
    {
        resetStream();

        int count = 0; 
        while (!stream_handle.EndOfStream)
        {
            Card card = new Card(stream_handle.ReadLine());
            Console.WriteLine($"{card.GetDetail()} - {card.GetNumberofWins()}");
        }

        Console.WriteLine($"\t\tPart Two:\n");
    }
} 

class Card
{
    private List<int> myNumbers = new List<int>();
    private List<int> winningNumbers = new List<int>();
    string cardDetail;

    public Card(string data)
    {
        string[] dataArray = data.Split(": ");
        cardDetail = dataArray[0];
        string[] numbersArray = dataArray[1].Split(" | ");

        string[] sNumbers = numbersArray[0].Split(new string[] { " ", "  " }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string number in sNumbers)
        {
            myNumbers.Add(int.Parse(number));
        }

        string[] sWinning = numbersArray[1].Split(new string[] { " ", "  "}, StringSplitOptions.RemoveEmptyEntries  );

        foreach (string number in sWinning)
        {
            winningNumbers.Add(int.Parse(number));
        }
    }

    public string GetDetail()
    {
        return cardDetail;
    }                


    public int GetCardScore()
    {
        int wins = GetNumberofWins();
        int score = 1;

        while (wins-- > 0)
            score *= 2;

        score /= 2;
        return score;
    }

    public int GetNumberofWins()
    {
        int nWins = 0;
        foreach (int i in myNumbers)
        {
            if (winningNumbers.Contains(i))
                nWins++;
        }
        return nWins;
    }
}