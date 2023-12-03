class SolveDayThree : SolutionsBase
{   
    const int SIZE = 140;
    char[,] array = new char[SIZE,SIZE];     
    char[] number = new char[5];

    List<(int,int,int)> starNums = new List<(int,int,int)>();

    public SolveDayThree(StreamReader given_sr) : base(given_sr)
    { 
        while (!stream_handle.EndOfStream)
        {
            for (int row = 0; row < SIZE; row++)
            {
                string line = stream_handle.ReadLine();

                for (int col = 0; col < SIZE; col++)
                {
                    array[col,row] =  line[col];
                }
            }
        }   
    }

    public override void part_one()
    {
        int count = 0;      

        for (int row = 0; row < SIZE; row++)
        {
            int numIndex = 0;
            bool isAdjacent = false;

            for (int col = 0; col < SIZE; col++)
            {
                if (array[col, row] >= 48 && array[col, row] <= 57)
                {
                    // build number
                    number[numIndex] = array[col, row];

                    // set flag
                    if (ScanForAdjacentSymbol(col, row))
                        isAdjacent = true;

                    numIndex++;
                }
                else
                {
                    // each number on a row
                    count += Add_Reset(ref numIndex, ref isAdjacent);
                }
            }
            // number at end of row
            count += Add_Reset(ref numIndex, ref isAdjacent);
        }
        Console.WriteLine($"\t\tThe sum of every number that is adjacent to a symbol is: {count}.\n");
    }

    private int Add_Reset(ref int numIndex, ref bool isAdjacent)
    {
        int retVal = 0;

        if (numIndex > 0)
        {
            int iNum = int.Parse(number);

            if (isAdjacent)
                retVal= iNum;
        }   

        // reset number
        for (int i = 0; i < 5; i++)
            number[i] = ' ';
        
        numIndex = 0;
        isAdjacent = false;

        return retVal;
    }

    private bool Symbol(char ch)
    {
        bool isSymbol = true;

        if (ch >= 48 && ch <= 57)
            isSymbol = false; // its a number
        else if (ch == '.')
            isSymbol = false; // its a dot

        return isSymbol;
    }

    private bool ScanForAdjacentSymbol(int col, int row)
    {
        bool HasAdjacent = false;

        for (int r = -1; r <= 1; r++)
        {
            if (row + r >= 0 && row + r < SIZE)
            {
                for (int c = -1; c <= 1; c++)
                {
                    if (col + c >= 0 && col + c < SIZE)
                    {
                        if (Symbol(array[col + c,row + r]) == true)
                        {
                            HasAdjacent = true;
                        }
                    }
                }
            }
        }
        return HasAdjacent;
    }

    public override void part_two()
    {
        int count = 0;

        for (int row = 0; row < SIZE; row++)
        {
            int numIndex = 0;
            bool adjacentStar = false;
            int starCol = 0;
            int starRow = 0;

            for (int col = 0; col < SIZE; col++)
            {
                if (array[col, row] >= 48 && array[col, row] <= 57)
                {
                    // build number
                    number[numIndex] = array[col, row];
    
                    if (ScanForAdjacentStar(col, row, ref starCol, ref starRow))
                        adjacentStar = true; 
                    
                    numIndex++;
                }
                else
                {
                    count += Add_Reset(ref numIndex, ref adjacentStar, ref starCol, ref starRow);
                }
            }
            count += Add_Reset(ref numIndex, ref adjacentStar, ref starCol, ref starRow);
        }
        Console.WriteLine($"\t\tThe sum of all of the gear ratios in your engine schematic is: {count}.\n");
    }    

    private int Add_Reset(ref int numIndex, ref bool adjacentStar, ref int starCol, ref int starRow)
    {
        int retVal = 0;

        if (numIndex > 0)
        {
            int iNum = int.Parse(number);
            bool foundMatch = false;

            if (adjacentStar)
            {
                foreach ((int,int,int) starNum in starNums)
                {
                    if (starNum.Item2 == starCol && starNum.Item3 == starRow)
                    {
                        foundMatch = true;
                        retVal += iNum * starNum.Item1;                            
                    }
                }
                if (foundMatch == false)
                {
                    starNums.Add((iNum, starCol, starRow));
                }
            }
        }   

        // reset variables
        for (int i = 0; i < 5; i++)
            number[i] = ' ';
        
        numIndex = 0;
        adjacentStar = false;
        starCol = 0;
        starRow = 0;

        return retVal;
    }

    private bool ScanForAdjacentStar(int col, int row, ref int starCol, ref int starRow)
    {
        bool hasAdjacentStar = false;

        for (int r = -1; r <= 1; r++)
        {
            if (row + r >= 0 && row + r < SIZE)
            {
                for (int c = -1; c <= 1; c++)
                {
                    if (col + c >= 0 && col + c < SIZE)
                    {
                        if (array[col + c,row + r] == '*')
                        {
                            hasAdjacentStar = true;
                            starCol = col + c;
                            starRow = row + r;
                        }
                    }
                }
            }
        }
        return hasAdjacentStar;
    }
}