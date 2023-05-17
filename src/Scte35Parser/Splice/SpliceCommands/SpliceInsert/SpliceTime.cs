using System;
using System.Collections.Generic;
using Scte35Parser.Extensions;

namespace Scte35Parser.Splice.SpliceCommands.SpliceInsert
{
    public class SpliceTime
    {
        public bool TimeSpecifiedFlag { get; set; }
        
        public long PtsTime { get; set; }
        
        public void Read(List<int> stce35BitArray)
        {
            TimeSpecifiedFlag = Convert.ToBoolean(stce35BitArray.Read(1));

            if (TimeSpecifiedFlag)
            {
                _ = stce35BitArray.Read(6);
                PtsTime = stce35BitArray.Read(33);
            }
            else
            {
                _ = stce35BitArray.Read(7);
            }
        }
    }
}