using System.Collections.Generic;

namespace Scte35Parser.Splice.SpliceCommands
{
    public interface ISpliceCommand
    {
        public void Read(List<int> stce35BitArray);
    }
}