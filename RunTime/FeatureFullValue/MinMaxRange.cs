using System;

namespace VitoBarra.GeneralUtility.FeatureFullValue
{
    [Serializable]
    public class MinMaxRange
    {
        public int Min;
        public int Max;

        public MinMaxRange(int minEnergy, int maxEnergy)
        {
            Min = minEnergy;
            Max = maxEnergy;
        }
    }
}