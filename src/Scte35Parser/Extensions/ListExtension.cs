using System;
using System.Collections.Generic;

namespace Scte35Parser.Extensions
{
    public static class ListExtension
    {
        public static long Read(this List<int> bitArray, int size)
        {
            var a = bitArray.GetRange(0, size);
            bitArray.RemoveRange(0, size);
            long hSigNum = 0;
            if (size > 32)
            {
                for (int i = 0; i < size - 32; i++)
                {
                    hSigNum = hSigNum << 1;
                    long aVal = a[i];
                    if (aVal == 1)
                    {
                        hSigNum += 1;
                    }
                }

                hSigNum = hSigNum * (int)Math.Pow(2, 32);
                size = 32;
            }

            long num = 0;
            for (int i = 0; i < size; i++)
            {
                num = num << 1;
                long aVal = a[i + (size > 32 ? size - 32 : 0)];
                if (aVal == 1)
                {
                    num += 1;
                }
            }

            if (size >= 32)
            {
                num = num >> 0;
            }

            return hSigNum + num;
        }
    }
}