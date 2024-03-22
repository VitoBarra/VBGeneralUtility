using System;
using System.Collections.Generic;

namespace VitoBarra.GeneralUtility.DataStructure
{
    public class DynamicMatrix<T>
    {
        IList<IList<T>> Matrix;

        int Column, Row;

        T DefaultValue;

        public DynamicMatrix(int row, int column, T defaultValue = default)
        {
            Column = column;
            Row = row;
            DefaultValue = defaultValue;
            Matrix = new List<IList<T>>();
            for (int i = 0; i < row; i++)
            {
                var col = new List<T>();
                for (int j = 0; j < column; j++)
                    col.Add(defaultValue);
                Matrix.Add(col);
            }
        }

        public T this[int i, int j]
        {
            get => Get(i, j);
            set => Set(value, i, j);
        }

        public T Get(int i, int j)
        {
            if (!(IsValidCord(i, j)))
                throw new ArgumentOutOfRangeException();
            return Matrix[i][j];
        }

        public void Set(T data, int i, int j)
        {
            if (!(IsValidCord(i, j)))
                throw new ArgumentOutOfRangeException();
            Matrix[i][j] = data;
        }


        public bool IsValidCord(int row, int col)
        {
            return row >= 0 && row < Row && col >= 0 && col < Column;
        }

        public void Resize(int newMaxRow, int newMaxColumn)
        {
            var colDiff = newMaxColumn - Column;
            var rowDiff = newMaxRow - Row;

            Row = newMaxRow;

            switch (rowDiff)
            {
                case > 0:
                {
                    for (int i = 0; i < rowDiff; i++)
                    {
                        var row = new List<T>();
                        for (int j = 0; j < Row; j++)
                            row.Add(DefaultValue);
                        Matrix.Add(row);
                    }

                    break;
                }
                case < 0:
                {
                    for (int i = 0; i < -rowDiff; i++)
                        Matrix.RemoveAt(Matrix.Count - 1);
                    break;
                }
            }

            Column = newMaxColumn;

            switch (colDiff)
            {
                case > 0:
                {
                    foreach (var t in Matrix)
                        for (int j = 0; j < colDiff; j++)
                            t.Add(DefaultValue);

                    break;
                }
                case < 0:
                {
                    foreach (var t in Matrix)
                        for (int j = 0; j < -colDiff; j++)
                            t.RemoveAt(t.Count - 1);

                    break;
                }
            }
        }
    }
}