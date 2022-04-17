using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace Balanced
{
    public class Splits
    {
        public string GameName;
        public string CategoryName;
        public string CategoryVariable;
        public uint AttemptCount;
        public string PersonalBest;
        public string SumOfBest;
        public List<double> PBSplits;
        public List<double> BestSplits;

        public Splits(string aGameName, string aCategoryName, string aCategoryVariable, uint aAttemptCount, string aPersonalBest, string aSumOfBest, List<double> aPBSplits, List<double> aSOBSplits)
        {
            GameName = aGameName;
            CategoryName = aCategoryName;
            CategoryVariable = aCategoryVariable;
            AttemptCount = aAttemptCount;
            PersonalBest = aPersonalBest;
            SumOfBest = aSumOfBest;
            PBSplits = aPBSplits;
            BestSplits = aSOBSplits;
        }

        public Splits()
        {
        }
    }
}
