﻿using System;

namespace VitoBarra.GeneralUtility.FeatureFullValue
{
    [Serializable]
    public class Vector2Hook
    {
        public float i, j;

        public Vector2Hook(float i, float j)
        {
            this.i = i;
            this.j = j;
        }

        public void Set(float i, float j)
        {
            this.i = i;
            this.j = j;
        }
    }
}