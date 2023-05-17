using System;
using System.Collections.Generic;
using Scte35Parser.Extensions;

namespace Scte35Parser.Splice.SpliceCommands.SpliceInsert
{
    public class SliceDescriptor
    {
        public int SpliceDescriptorTag { get; set; }
        
        public int DescriptorLength { get; set; }
        
        public long Identifier { get; set; }

        public void Read(List<int> stce35BitArray)
        {
            SpliceDescriptorTag =  Convert.ToInt16(stce35BitArray.Read(8));
            DescriptorLength =  Convert.ToInt16(stce35BitArray.Read(8));
            Identifier =  stce35BitArray.Read(32);

            for (int i = 0; i < DescriptorLength; i++)
            {
                _ = stce35BitArray.Read(8);
            }
        }
    }
}