using System;
using System.Collections.Generic;
using System.IO;

namespace WAVExtractor
{
    internal class IO


    {

        public static void FileExistCheck(string[] args)

        {
            if (!File.Exists(args[0]))

            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input does not exist, please check your files and try again.");
                Console.ResetColor();
                Environment.Exit(1);
            }

        }

        public static void DirectoryExistCheck(string[] args)


        {

            if (!Directory.Exists(args[1]))

            {
                Directory.CreateDirectory(args[1]);
            }

        }

        public static void NoWAVECaseCheck(List<long> position, string[] args)

        {
            if (position.Count == 0)

            {
                Console.Clear();
                Console.WriteLine("No WAVE streams detected, please check your input and try again.");
                Directory.Delete(args[1]);
                Environment.Exit(1);
            }

        }

        public static bool HeaderEquals(byte[] buffer, byte[] pattern)
        {


            var index = 3;
            var patterChecksRemeaning = 4;


            while (patterChecksRemeaning > 0)
            {
                if (buffer[index] != pattern[index])
                {
                    return false;
                }

                index--;
                patterChecksRemeaning--;
            }

            return true;
        }


        public static void Usage()

        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("WAVExtractor <input_file> <output_path>");
            Console.WriteLine();
            Console.WriteLine(":(Sad8669 | insomnyawolf (Contribution to new algorithm)");
            Console.ResetColor();
        }

        public static bool IsWave(byte[] buffer, byte[] waveFormat, Stream bigStream)

        {
            bigStream.Position += 4;  // RIFF|....WAVEfmt -> RIFF....|WAVEfmt
            buffer[0] = (byte)(bigStream.ReadByte());
            buffer[1] = (byte)(bigStream.ReadByte());
            buffer[2] = (byte)(bigStream.ReadByte());
            buffer[3] = (byte)(bigStream.ReadByte());


            var waveIndex = 3;
            var waveChecksRemeaning = 4;

            while (waveChecksRemeaning > 0)
            {
                if (buffer[waveIndex] != waveFormat[waveIndex])
                {
                    return false;
                }

                waveIndex--;
                waveChecksRemeaning--;
            }

            return true;
        }
    }
}
