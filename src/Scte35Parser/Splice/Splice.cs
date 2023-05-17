using System;
using System.Collections.Generic;
using System.Text.Json;
using Scte35Parser.Extensions;
using Scte35Parser.Splice.SpliceCommands;
using Scte35Parser.Splice.SpliceCommands.SpliceInsert;

namespace Scte35Parser.Splice
{
    public class Splice
    {
        private const int TABLE_ID = 0xFC; // 252
        
        public int TableId { get; set; }
        
        public bool SectionSyntaxIndicator { get; set; }
        
        public bool PrivateIndicator { get; set; }
        
        public int SapType { get; set; }
        
        public int SectionLength { get; set; }
        
        public int ProtocolVersion { get; set; }
        
        public bool EncryptedPacket { get; set; }
        
        public int EncryptionAlgorithm { get; set; }
        
        public long PtsAdjustment { get; set; }
        
        public int CwIndex { get; set; }
        
        public int Tier { get; set; }

        public int SpliceCommandLength { get; set; }
        
        public int SpliceCommandType { get; set; }
        
        public int DescriptorLoopLength { get; set; }
        
        public List<SliceDescriptor> SliceDescriptors { get; set; }  = default!;

        public ISpliceCommand SpliceCommand { get; set; }  = default!;
        
        public void Read(List<int> stce35BitArray)
        {
            TableId = Convert.ToInt16(stce35BitArray.Read(8));

            if (TableId != TABLE_ID)
            {
                throw new Exception("Table id value shall be 0xFC.");
            }

            SectionSyntaxIndicator = Convert.ToBoolean(stce35BitArray.Read(1));
            PrivateIndicator = Convert.ToBoolean(stce35BitArray.Read(1));
            SapType = Convert.ToInt16(stce35BitArray.Read(2));
            SectionLength = Convert.ToInt16(stce35BitArray.Read(12));
            ProtocolVersion = Convert.ToInt16(stce35BitArray.Read(8));
            EncryptedPacket = Convert.ToBoolean(stce35BitArray.Read(1));
            EncryptionAlgorithm = Convert.ToInt16(stce35BitArray.Read(6));
            PtsAdjustment = stce35BitArray.Read(33);
            CwIndex = Convert.ToInt16(stce35BitArray.Read(8));
            Tier = Convert.ToInt16(stce35BitArray.Read(12));
            SpliceCommandLength = Convert.ToInt16(stce35BitArray.Read(12));
            SpliceCommandType = Convert.ToInt16(stce35BitArray.Read(8));

            if (SpliceCommandType == SpliceCommandValue.SPLICE_INSERT)
            {
                SpliceCommand = new SpliceInsert();
                SpliceCommand.Read(stce35BitArray);
            }
            else
            {
                throw new NotSupportedException($"SpliceCommandType : {SpliceCommandType} not supported.");
            }
            
            DescriptorLoopLength = Convert.ToInt32(stce35BitArray.Read(16));

            SliceDescriptors = new List<SliceDescriptor>();
            for (var i = 0; i < DescriptorLoopLength; i++)
            {
                var sliceDescriptor = new SliceDescriptor(); 
                sliceDescriptor.Read(stce35BitArray);
                SliceDescriptors.Add(sliceDescriptor);
            }
        }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}