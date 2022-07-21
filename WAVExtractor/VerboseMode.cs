using System;
using System.IO;

namespace WAVExtractor
{
    internal class VerboseMode

    {
        public static string FileSize(byte[] input)

        {

            var fileSize = input.Length - 8;

            if (fileSize < 1024 * 1024 * 1024 && fileSize > 1024 * 1024)
               
                return (fileSize / (1024d * 1024d)).ToString("0.01") + " MB ";

            else 
                
                return fileSize.ToString() + " bytes";

        }

        public static string Format(byte[] input)

        {

            if (BitConverter.ToInt16(input, 20) == 1)

            {
                return "PCM";
            }

            else

            {
                return "Unknown!";
            }

        }

        public static string Channel(byte[] input)

        {

            if (BitConverter.ToInt16(input, 22) == 1)

            {
                return "Mono";
            }

            else

            {
                return "Stereo";
            }

        }

        public static int SampleRate(byte[] input)

        {

            return BitConverter.ToInt32(input, 24);

        }

        public static int BitsPerSample(byte[] input)

        {

            return BitConverter.ToInt16(input, 34);

        }

        public static void ShowInfo(byte[] input)

        {
            Console.WriteLine(new string('-',50));

            Console.WriteLine("[o] File Size: " + FileSize(input));
            Console.WriteLine("[o] Channel: " + Channel(input));
            Console.WriteLine("[o] Format: " + Format(input));
            Console.WriteLine("[o] SampleRate: " + SampleRate(input) + " Hz ");
            Console.WriteLine("[o] BitsPerSample: " + BitsPerSample(input));

            Console.WriteLine(new string('-', 50));
        }
    }
}
