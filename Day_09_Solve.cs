class SolveDayNine : SolutionsBase
{   
    public SolveDayNine(StreamReader given_sr) : base(given_sr)
    { 
    }

    public override void part_one()
    {       
        int count = 0; 

        while (!stream_handle.EndOfStream)
        {
            LineData myLineData = new LineData(stream_handle.ReadLine());

            myLineData.FillDiffS();
            myLineData.AddDiffS();
            count += myLineData.GetSum();
//            myLineData.ShowData();
        }
        Console.WriteLine($"\t\tThe sum of the extrapolated values is: {count} \n");
    }

    public override void part_two()
    {
        resetStream();

        int count = 0; 

        while (!stream_handle.EndOfStream)
        {
            LineDataPt2 myLineData = new LineDataPt2(stream_handle.ReadLine());
            myLineData.FillDiffS();
            myLineData.AddDiffS();
            count += myLineData.GetSum();
//            myLineData.ShowData();
        }
        Console.WriteLine($"\t\tThe sum of the extrapolated values is: {count} \n");
    }
} 

class LineDataPt2
{
    private const int RowCount = 21;
    private const int ColCount = 21 + 1;

    private (int, int) diffPoint; // col, row

    private int[,] dataArray = new int[ColCount,RowCount]; 

    public LineDataPt2(string data)
    {
        int c = 1;
        foreach (string num in data.Split(" "))
        {
            dataArray[c++, 0] = int.Parse(num);
        }
    }
    public int GetSum()
    {
        return dataArray[0, 0];
    }

    public void AddDiffS()
    {
        while (diffPoint.Item2 > 0)
        {
            // get the diff value
            int diffVal = dataArray[diffPoint.Item1, diffPoint.Item2];
            // get last value in the row below
            int lastVal = dataArray[diffPoint.Item1, diffPoint.Item2 - 1];             
            // apply it to row below
            dataArray[diffPoint.Item1 - 1, diffPoint.Item2 - 1] = lastVal - diffVal;
            // shift dowm a row and over a col
            diffPoint.Item1 = diffPoint.Item1 - 1;
            diffPoint.Item2 = diffPoint.Item2 - 1;
        }
    }

    public void FillDiffS()
    {
        int colstop = 2;
        bool havediff = false;

        for (int r = 0; r < RowCount - 1; r++)
        {
            havediff = false;
            for (int c = ColCount - 1; c >= colstop; c--)
            {
                int diff = dataArray[c,r] - dataArray[c-1, r];

                if (diff != 0)
                  havediff = true;                        

                dataArray[c, r + 1] = diff;
            }
            colstop++;

            if (havediff == false)
            {
                diffPoint.Item1 = colstop - 2;
                diffPoint.Item2 = r;
                r = RowCount;
            }
        }
        dataArray[diffPoint.Item1 - 1, diffPoint.Item2] = dataArray[diffPoint.Item1, diffPoint.Item2];
        diffPoint.Item1 -= 1;
    }

    public void ShowData()
    {
        for (int r = 0; r < RowCount; r++)
        {
            for (int c = 0; c < ColCount; c++)
            {
                Console.Write($"{dataArray[c,r]}\t");
            }
            Console.WriteLine();
        }
        Console.WriteLine($"diffPoint.Item1 col = {diffPoint.Item1} diffPoint.Item2 row = {diffPoint.Item2}");
    }    
}

class LineData
{
    private const int RowCount = 21;
    private const int ColCount = 21 + 1;
    private (int, int) diffPoint; // col, row

    private int[,] dataArray = new int[ColCount,RowCount]; 

    public LineData(string data)
    {
        int c = 0;
        foreach (string num in data.Split(" "))
        {
            dataArray[c++, 0] = int.Parse(num);
        }
    }
    public int GetSum()
    {
        return dataArray[ColCount - 1, 0];
    }

    public void AddDiffS()
    {
        while (diffPoint.Item2 > 0)
        {
            // get the diff value
            int diffVal = dataArray[diffPoint.Item1, diffPoint.Item2];
            // get last value in the row below
            int lastVal = dataArray[diffPoint.Item1, diffPoint.Item2 - 1];             
            // apply it to row below
            dataArray[diffPoint.Item1 + 1, diffPoint.Item2 - 1] = lastVal + diffVal;
            // shift dowm a row and over a col
            diffPoint.Item1 = diffPoint.Item1 + 1;
            diffPoint.Item2 = diffPoint.Item2 - 1;
        }
    }


    public void FillDiffS()
    {
        int col = ColCount;
        bool havediff = false;

        for (int r = 0; r < RowCount -1; r++)
        {
            havediff = false;
            for (int c = 1; c < col -1; c++)
            {
                int diff = dataArray[c,r] - dataArray[c-1,r];

                if (diff != 0)
                    havediff = true;                        

                dataArray[c-1, r+1] = diff;
            }
            col--;
            if (havediff == false)
            {
                diffPoint.Item1 = col - 1;
                diffPoint.Item2 = r;
                r = RowCount;
            }
        }
        dataArray[diffPoint.Item1 + 1, diffPoint.Item2] = dataArray[diffPoint.Item1, diffPoint.Item2];
        diffPoint.Item1 += 1;
    }

    public void ShowData()
    {
        for (int r = 0; r < RowCount; r++)
        {
            for (int c = 0; c < ColCount; c++)
            {
                Console.Write($"{dataArray[c,r]}\t");
            }
            Console.WriteLine();
        }
        Console.WriteLine($"diffPoint.Item1 col = {diffPoint.Item1} diffPoint.Item2 row = {diffPoint.Item2}");
    }    
}
