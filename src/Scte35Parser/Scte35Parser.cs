namespace Scte35Parser
{
    using System;
    using System.Collections.Generic;

    public class Scte35Parser
    {
        private List<int> _bitArray = default!;

        private void Init()
        {
            _bitArray = new List<int>();
        }

        public Splice.Splice ParseFromBase64(string data)
        {
            Init();

            var bytes = Convert.FromBase64String(data);
            var hexadecimal = BitConverter.ToString(bytes);
            hexadecimal = hexadecimal.Replace("-", string.Empty);
            return ParseFromHex(hexadecimal);
        }

        public Splice.Splice ParseFromHex(string hexadecimal)
        {
            Init();

            for (var i = 0; i < hexadecimal.Length; i += 2)
            {
                var hexNumber = hexadecimal.Substring(i, 2);
                var decimalNumber = int.Parse(hexNumber, System.Globalization.NumberStyles.HexNumber);
                WriteToBitArray(decimalNumber);
            }

            return Parse();
        }

        private Splice.Splice Parse()
        {
            var splice = new Splice.Splice();
            splice.Read(_bitArray);
            return splice;
        }

        private void WriteToBitArray(int decimalNumber)
        {
            var r = 128;
            for (var i = 0; i < 8; i++)
            {
                var bitValue = 0;
                if ((r & decimalNumber) > 0)
                {
                    bitValue = 1;
                }

                _bitArray.Add(bitValue);
                r = r >> 1;
            }
        }
    }
}