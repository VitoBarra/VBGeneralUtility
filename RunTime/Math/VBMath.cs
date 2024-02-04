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
    }
}
