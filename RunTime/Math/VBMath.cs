using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VitoBarra.GeneralUtility
{
    public static class VBMath
    {
        public static float Map(float value, float fromLeft, float fromRight, float toLeft, float toRight)
        {
            if (value < fromLeft || value > fromRight)
                throw new ArgumentException($"value x must be in range [{fromLeft}, {fromRight}].");
            return (value - fromLeft) / (fromRight - fromLeft) * (toRight - toLeft) + toLeft;
        }


        #region Line
        public static Vector2 ClosestPointOnPath(Vector2 p, IList<Vector2> path, out int closestSegmentIndex)
        {
            Vector2 cp = path[0];
            float bestDst = float.MaxValue;
            closestSegmentIndex = 0;

            for (int i = 0; i < path.Count - 1; i++)
            {
                Vector2 newP = ClosestPointOnLineSegment(path[i], path[i + 1], p);
                float sqrDst = (p - newP).sqrMagnitude;
                if (sqrDst < bestDst)
                {
                    bestDst = sqrDst;
                    cp = newP;
                    closestSegmentIndex = i;
                }
            }

            return cp;
        }

        public static Vector2 ClosestPointOnPath(Vector2 p, IList<Vector2> path)
        {
            return ClosestPointOnPath(p, path, out _);
        }

        public static Vector2 ClosestPointOnLineSegment(Vector2 lineStart, Vector2 lineEnd, Vector2 p)
        {
            Vector2 aB = lineEnd - lineStart;
            Vector2 aP = p - lineStart;
            float sqrLenAB = aB.sqrMagnitude;
            // Handle case where start/end points are in same position (i.e. line segment is just a single point)
            if (sqrLenAB == 0)
            {
                return lineStart;
            }

            float t = Mathf.Clamp01(Vector3.Dot(aP, aB) / sqrLenAB);
            return lineStart + aB * t;
        }

        #endregion
    }
}