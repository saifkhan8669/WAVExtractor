using System;
using System.Collections.Generic;
using System.IO;

namespace WAVExtractor
{
    internal static class IO


    {

        public static void FileExistCheck(string[] args)

        {
            if (!File.Exists(args[0]))

            {
                Console.Clear();
                Visual.Red("Input does not exist, please check your files and try again.");
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
            Visual.Yellow("WAVExtractor <input_file> <output_path>");
        }

        public static bool IsWave(byte[] buffer, byte[] waveFormat, Stream bigStream)

        {
            bigStream.Position += 4;  // RIFF|....WAVEfmt --> RIFF....|WAVEfmt,        "|" = Pointer
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

        public static byte[] ReadBytes(this Stream stream, int count)
        {
            var result = new byte[count];
            int offset = 0;
            while (offset < count)
            {
                int bytesRead = stream.Read(result, offset, count - offset);
                if (bytesRead <= 0)
                    throw new IOException();
                offset += bytesRead;
            }
            return result;
        }

        public static byte[] WriteBytes(this Stream stream)
        {
            return WriteBytes(stream, (int)stream.Length);
        }

        public static byte[] WriteBytes(this Stream stream, int count)
        {
            var result = new byte[count];
            int offset = 0;
            while (offset < count)
            {
                stream.Write(result, offset, count - offset);
                int bytesRead = (int)stream.Position;
                if (bytesRead <= 0)
                    throw new IOException();
                offset += bytesRead;
            }
            return result;
        }

        public static byte[] ReadBytes(this Stream stream)
        {
            return ReadBytes(stream, (int)stream.Length);
        }
       
        public static void WAVStreamsCount(long count,int i)
        
        {
            Console.Title = "File Progress: " + (i + 1) + "/" + count;
        }

    }
}
