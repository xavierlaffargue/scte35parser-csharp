using System;
using System.Collections.Generic;
using Scte35Parser.Extensions;

namespace Scte35Parser.Splice.SpliceCommands.SpliceInsert
{
    public class SpliceInsert : ISpliceCommand
    {
        public long SpliceEventId { get; set; }
        
        public bool SpliceEventCancelIndicator { get; set; }

        public bool OutOfNetworkIndicator { get; set; }
        
        public bool ProgramSpliceFlag { get; set; }
        
        public bool DurationFlag { get; set; }
        
        public bool SpliceImmediateFlag { get; set; }

        public int ComponentCount { get; set; }
        
        public int ComponentTag { get; set; }
        
        public int UniqueProgramId { get; set; }
        
        public int AvailNum { get; set; }
        
        public int AvailsExpected { get; set; }

        public BreakDuration BreakDuration { get; set; } = default!;
        
        public SpliceTime SpliceTime { get; set; } = default!;

        public void Read(List<int> stce35BitArray)
        {
            SpliceEventId = stce35BitArray.Read(32);
            SpliceEventCancelIndicator = Convert.ToBoolean(stce35BitArray.Read(1));
            _ = stce35BitArray.Read(7);

            if (!SpliceEventCancelIndicator)
            {
                OutOfNetworkIndicator = Convert.ToBoolean(stce35BitArray.Read(1));
                ProgramSpliceFlag = Convert.ToBoolean(stce35BitArray.Read(1));
                DurationFlag = Convert.ToBoolean(stce35BitArray.Read(1));
                SpliceImmediateFlag = Convert.ToBoolean(stce35BitArray.Read(1));
                _ = stce35BitArray.Read(4);
                if (ProgramSpliceFlag && !SpliceImmediateFlag)
                {
                    SpliceTime = new SpliceTime();
                    SpliceTime.Read(stce35BitArray);
                }

                if (!ProgramSpliceFlag)
                {
                    throw new NotSupportedException();
                }
                
                if (DurationFlag)
                { 
                    BreakDuration = new BreakDuration();
                    BreakDuration.Read(stce35BitArray);
                }

                UniqueProgramId = Convert.ToInt32(stce35BitArray.Read(16));
                AvailNum = Convert.ToInt16(stce35BitArray.Read(8));
                AvailsExpected = Convert.ToInt16(stce35BitArray.Read(8));
            }
        }
        
    }
}