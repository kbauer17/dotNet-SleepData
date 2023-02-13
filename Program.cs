using NLog;
string path = Directory.GetCurrentDirectory() + "\\nlog.config";
// create instance of Logger
var logger = LogManager.LoadConfiguration(path).GetCurrentClassLogger();

// See https://aka.ms/new-console-template for more information
// ask for input
Console.WriteLine("Enter 1 to create data file.");
Console.WriteLine("Enter 2 to parse data.");
Console.WriteLine("Enter anything else to quit.");
// input response
string? resp = Console.ReadLine();

if (resp == "1")
{
    // create data file

    // ask a question
    Console.WriteLine("How many weeks of data is needed?");
    // input the response (convert to int)
    // int weeks;
    if (int.TryParse(Console.ReadLine(), out int weeks)){
	    // determine start and end date
        DateTime today = DateTime.Now;
        // we want full weeks sunday - saturday
        DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);
        // subtract # of weeks from endDate to get startDate
        DateTime dataDate = dataEndDate.AddDays(-(weeks * 7));
        // random number generator
        Random rnd = new Random();
        // create file
        StreamWriter sw = new StreamWriter("data.txt");

        // loop for the desired # of weeks
        while (dataDate < dataEndDate)
        {
            // 7 days in a week
            int[] hours = new int[7];
            for (int i = 0; i < hours.Length; i++)
            {
                // generate random number of hours slept between 4-12 (inclusive)
                hours[i] = rnd.Next(4, 13);
            }
            // M/d/yyyy,#|#|#|#|#|#|#
            // Console.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
            sw.WriteLine($"{dataDate:M/d/yyyy},{string.Join("|", hours)}");
            // add 1 week to date
            dataDate = dataDate.AddDays(7);
        }
        sw.Close();
    } else {
        // log error
        logger.Error("You must enter a valid number");
    }
}
else if (resp == "2")
{
    // parse data file

    
            //string line = "";
            using (StreamReader sr = new StreamReader("data.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    // parse the date from each line of data and output to console with headers
                    string date = line.Substring(0,line.IndexOf(','));
                    var parsedDate = DateTime.Parse(date);
                    Console.WriteLine($"Week of {parsedDate:MMM, dd, yyyy}:");
                    Console.WriteLine("{0,3}{1,3}{2,3}{3,3}{4,3}{5,3}{6,3}","Su","Mo","Tu","We","Th","Fr","Sa");
                    Console.WriteLine("{0,3}{1,3}{2,3}{3,3}{4,3}{5,3}{6,3}","--","--","--","--","--","--","--");

                    // parse the hour data from each line of date and output to console aligned to header/columns
                    string hours = line.Substring(line.IndexOf(',')+1);
                    String[] arr = hours.Split('|');
                    Console.WriteLine("{0,3}{1,3}{2,3}{3,3}{4,3}{5,3}{6,3}",arr[0],arr[1],arr[2],arr[3],arr[4],arr[5],arr[6]);
                    System.Console.WriteLine();
                }
            }
   
    
}
