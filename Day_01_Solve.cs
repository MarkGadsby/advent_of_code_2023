class SolveDayOne : SolutionsBase
{   
    public SolveDayOne(StreamReader given_sr) : base(given_sr)
    { 
    }

    public override void part_one()
    {
        int totalSum = 0;

        while (stream_handle.Peek() >= 0)
        {
            string data = stream_handle.ReadLine();
            int start = 0;
            int end = data.Count() - 1;

            int firstDigit = -1;  
            int lastDigit = -1;  

            while (start <= end && (firstDigit == -1 || lastDigit == -1))  
            {
                if (data[start] >= 48 && data[start] <= 57) // is number
                    firstDigit = data[start] - 48;

                if (data[end] >= 48 && data[end] <= 57) // is number
                    lastDigit = data[end] - 48;

                if (firstDigit == -1)
                    start++;

                if (lastDigit == -1)
                    end--;
            }

            totalSum += firstDigit * 10 + lastDigit;
        }
        Console.WriteLine($"\t\tThe total sum of all of the calibration values is: {totalSum}.\n");
    }

    public override void part_two()
    {
        resetStream();
        int totalSum = 0;

        while (stream_handle.Peek() >= 0)
        {
            string data = stream_handle.ReadLine();
            int start = 0;
            int end = data.Count() - 1;

            int firstDigit = -1;  
            int lastDigit = -1;  

            while (start <= end && (firstDigit == -1 || lastDigit == -1))  
            {
                if (data[start] >= 48 && data[start] <= 57) // is number
                    firstDigit = data[start] - 48;
                else 
                    firstDigit = CheckForSpelledOutNumber(data, start, true);

                if (data[end] >= 48 && data[end] <= 57) // is number
                    lastDigit = data[end] - 48;
                else 
                    lastDigit = CheckForSpelledOutNumber(data, end, false);

                if (firstDigit == -1)
                    start++;

                if (lastDigit == -1)
                    end--;
            }
            totalSum += firstDigit * 10 + lastDigit;
        }
        Console.WriteLine($"\t\tThe total sum of all of the calibration values - including spelled out ones -  is: {totalSum}.\n");
    }
    
    private int CheckForSpelledOutNumber(string data, int index, bool forward)
    {
        if (forward)
        {
            switch (data[index])
            {
                case 'o':
                {
                    if (IsInRange(data.Count(), index, 3) && data.Substring(index, 3) == "one")
                        return 1;
                    break;
                }
                case 't':
                {
                    if (IsInRange(data.Count(), index, 3) && data.Substring(index, 3) == "two")
                        return 2;
                    else if (IsInRange(data.Count(), index, 5) && data.Substring(index, 5) == "three")
                        return 3;
                    break;
                }
                case 'f':
                {
                    if (IsInRange(data.Count(), index, 4) && data.Substring(index, 4) == "four")
                        return 4;
                    else if (IsInRange(data.Count(), index, 4) && data.Substring(index, 4) == "five")
                        return 5;
                    break;
                }
                case 's':
                {
                    if (IsInRange(data.Count(), index, 3) && data.Substring(index, 3) == "six")
                        return 6;
                    else if (IsInRange(data.Count(), index, 5) && data.Substring(index, 5) == "seven")
                        return 7;
                    break;
                }
                case 'e':
                {
                    if (IsInRange(data.Count(), index, 5) && data.Substring(index, 5) == "eight")
                        return 8;
                    break;
                }
                case 'n':
                {
                    if (IsInRange(data.Count(), index, 4) && data.Substring(index, 4) == "nine")
                        return 9;
                    break;
                }
                default:
                {
                    return -1;
                    break;
                }
            }
        }
        if (!forward)
        {
            switch (data[index])
            {
                case 'e':
                {
                    if (index - 2 >= 0 && data.Substring(index - 2, 3) == "one")
                        return 1;
                    else if (index - 3 >= 0 && data.Substring(index - 3, 4) == "nine")
                        return 9;                        
                    else if (index - 3 >= 0 && data.Substring(index - 3, 4) == "five")
                        return 5;                        
                    else if (index - 4 >= 0 && data.Substring(index - 4, 5) == "three")
                        return 3;                        
                    break;
                }
                case 'o':
                {
                    if (index - 2 >= 0 && data.Substring(index - 2, 3) == "two")
                        return 2;
                    break;
                }
                case 'r':
                {
                    if (index - 3 >= 0 && data.Substring(index - 3, 4) == "four")
                        return 4;
                    break;
                }
                case 'x':
                {
                    if (index - 2 >= 0 && data.Substring(index - 2, 3) == "six")
                        return 6;
                    break;
                }
                case 'n':
                {
                    if (index - 4 >= 0 && data.Substring(index - 4, 5) == "seven")
                        return 7;
                    break;
                }
                case 't':
                {
                    if (index - 4 >= 0 && data.Substring(index - 4, 5) == "eight")
                        return 8;
                    break;
                }
                default:
                {
                    return -1;
                    break;
                }
            }
        }
        return -1;
    }

    private bool IsInRange(int count, int index, int letters)
    {
        if ((count - index) < letters)
            return false;  

        return true;                      
    }
}

