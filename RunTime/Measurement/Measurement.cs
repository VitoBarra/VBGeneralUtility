using UnityEngine;

namespace VitoBarra.GeneralUtility.Measurement
{
    public class Measurement
    {
        public static int ManhattanDistance(int x1, int y1, int x2, int y2, int xWeight =1,int yWeight =1 )
        {
            return Mathf.Abs(x1 - x2) * xWeight + Mathf.Abs(y1 - y2) * yWeight;
        }

        public static float EuclideanDistance(Vector3 from, Vector3 to)
        {
            return Vector3.Distance(from, to);
        }

        public static float EuclideanDistance(Vector2 from, Vector2 to)
        {
            return Vector2.Distance(from, to);
        }
    }
}