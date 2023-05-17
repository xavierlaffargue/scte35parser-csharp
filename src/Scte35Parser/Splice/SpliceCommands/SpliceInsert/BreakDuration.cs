using System;
using System.Collections.Generic;
using Scte35Parser.Extensions;

namespace Scte35Parser.Splice.SpliceCommands.SpliceInsert
{
    public class BreakDuration
    {
        public bool AutoReturn { get; set; }
        
        public long Duration { get; set; }
        
        public void Read(List<int> stce35BitArray)
        {
            AutoReturn = Convert.ToBoolean(stce35BitArray.Read(1));
            _ = stce35BitArray.Read(6);
            Duration = stce35BitArray.Read(33);
        }
    }
}