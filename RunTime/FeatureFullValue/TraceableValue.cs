using System;
using UnityEngine;

namespace VitoBarra.GeneralUtility.FeatureFullValue
{
    public class TraceableValue<T>
    {
        T CurrentValue;

        [HideInInspector]
        public bool IsValueChanged;
        public Action OnValueChange;

        public TraceableValue(T value = default(T))
        {
            CurrentValue = value;
        }

        public T Value
        {
            get => CurrentValue;
            set
            {
                IsValueChanged = !CurrentValue.Equals(value);
                if (!IsValueChanged)
                    return;
                CurrentValue = value;
                OnValueChange?.Invoke();
            }
        }



    }

    [Serializable]
    public class TraceableInt : TraceableValue<int>
    {

        public TraceableInt(int value) : base(value){ }

        public static bool operator <(TraceableInt a, int b) => a.Value < b;
        public static bool operator <(int b, TraceableInt a) => b < a.Value;

        public static bool operator >(TraceableInt a, int b) => a.Value > b;
        public static bool operator >(int b, TraceableInt a) => b > a.Value;


        public static int operator +(TraceableInt a, int b) => a.Value + b;
        public static int operator +(int b, TraceableInt a) => b + a.Value;

        public static int operator -(TraceableInt a, int b) => a.Value - b;
        public static int operator -(int b, TraceableInt a) => b - a.Value;

        public static int operator *(TraceableInt a, int b) => a.Value * b;
        public static int operator *(int b, TraceableInt a) => b * a.Value;

        public static int operator /(TraceableInt a, int b) => a.Value / b;
        public static int operator /(int b, TraceableInt a) => b / a.Value;

        public static bool operator ==(TraceableInt a, int b) => a.Value.Equals(b);
        public static bool operator ==(int b, TraceableInt a) => b.Equals(a.Value);

        public static bool operator !=(TraceableInt a, int b) => a.Value != b;
        public static bool operator !=(int b, TraceableInt a) => b != a.Value;

        public static bool operator <=(TraceableInt a, int b) => a.Value <= b;
        public static bool operator <=(int b, TraceableInt a) => b <= a.Value;

        public static bool operator >=(TraceableInt a, int b) => a.Value >= b;
        public static bool operator >=(int b, TraceableInt a) => b >= a.Value;
    }
}