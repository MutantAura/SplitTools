// See https://aka.ms/new-console-template for more information

using System.Xml;

namespace Balanced
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read the raw directory of a drag and dropped lss file
            Console.WriteLine("Drag and Drop your .lss split file here then press ENTER: ");
            string splitDirRaw = Console.ReadLine();

            // Trim any " or * from the string and call Load
            string splitDir = splitDirRaw.Trim(new char[] { '"', '*' });

            Splits PB = Load(splitDir);
            Console.WriteLine($"Game Name: {PB.GameName} {"\n"}" +
                              $"Category Name: {PB.CategoryName} {"\n"}" +
                              $"Variable: {PB.CategoryVariable} {"\n"}" +
                              $"Attempt Count: {PB.AttemptCount} {"\n"}" +
                              $"Personal Best: {PB.PersonalBest} {"\n"}" +
                              $"Sum of Best: {PB.SumOfBest}");

            Console.ReadLine();
        }

        public static Splits Load(string Location)
        {
            // Open and load the split file as XML
            XmlDocument splitXML = new XmlDocument();
            splitXML.Load(Location);

            try
            {
                // Parse XML for basic data
                XmlNodeList TitleList = splitXML.GetElementsByTagName("GameName");
                XmlNodeList CategoryList = splitXML.GetElementsByTagName("CategoryName");
                XmlNodeList VariableList = splitXML.GetElementsByTagName("Variable");
                XmlNodeList RunsList = splitXML.GetElementsByTagName("AttemptCount");
                XmlNodeList PBList = splitXML.GetElementsByTagName("SplitTime");
                XmlNodeList SOBList = splitXML.GetElementsByTagName("BestSegmentTime");

                // Convert to base values
                int SplitCount = PBList.Count;
                string Title = TitleList[0].InnerText;
                string Category = CategoryList[0].InnerText;
                string Variable = VariableList[0].InnerText;
                uint Runs = UInt32.Parse(RunsList[0].InnerText);
                
                List<double> PBSeconds = new List<double>();
                List<double> SOBSeconds = new List<double>();   

                for (int i = 0; i < SplitCount; i++)
                {
                    string currentPBSplit = PBList[i].InnerText;
                    string currentSOBSplit = SOBList[i].InnerText;

                    if (currentPBSplit != null)
                    {
                        double temp1 = (TimeSpan.Parse(currentPBSplit).TotalSeconds)-PBSeconds.Sum();
                        double temp2 = TimeSpan.Parse(currentSOBSplit).TotalSeconds;

                        PBSeconds.Add(temp1);
                        SOBSeconds.Add(temp2);
                    }
                }

                string PBTotal = SecondsToTime(PBSeconds.Sum());
                string SOBTotal = SecondsToTime(SOBSeconds.Sum());

                Splits splitsPB = new Splits(Title, Category, Variable, Runs, PBTotal, SOBTotal, PBSeconds, SOBSeconds);

                return splitsPB;
            }

            catch (Exception)
            {
                Console.WriteLine("An error has occured when parsing these splits. Please try again:");

                return null;
            }    
        }

        public static string SecondsToTime(double Seconds)
        {
            TimeSpan t = TimeSpan.FromSeconds(Seconds);
            string timeStamp = t.ToString(@"hh\:mm\:ss");

            return timeStamp;
        }
    }

}
