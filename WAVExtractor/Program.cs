using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace WAVExtractor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.Title = "WAVExtractor | :(Sad8669";

            if (args.Length != 2)

            {
                IO.Usage();
                Environment.Exit(1);
            }

            Stopwatch timecountDetect = new Stopwatch();

            IO.FileExistCheck(args);
            IO.DirectoryExistCheck(args);

            // Input File
            var bigStream = File.Open(args[0], FileMode.Open, FileAccess.Read);


            // Variables and Magic Numbers //
            // ---------------------------------------------- //
            int currentByte;
            byte[] buffer = new byte[4]; // Window
            byte[] pattern = { 82, 73, 70, 70 }; // RIFF
            byte[] waveFormat = { 87, 65, 86, 69 }; // WAVE
            byte[] fileSize = new byte[4]; // TBD
            // ---------------------------------------------- //

            // To save streams position
            List<long> position = new List<long>();


            timecountDetect.Start();



            while ((currentByte = bigStream.ReadByte()) != -1)

            {

                for (int i = 0; i < 3; i++)

                {


                    buffer[i] = buffer[i + 1];
                }

                buffer[3] = (byte)currentByte;



                if (IO.HeaderEquals(buffer, pattern))

                {
                    if (IO.IsWave(buffer, waveFormat, bigStream))

                    {
                        // Resets the position
                        bigStream.Position -= 8;
                        Console.WriteLine("-> [0] WAVE stream located at offset(d): " + (bigStream.Position - buffer.Length));
                        position.Add(bigStream.Position - buffer.Length);

                        fileSize[0] = (byte)bigStream.ReadByte();
                        fileSize[1] = (byte)bigStream.ReadByte();
                        fileSize[2] = (byte)bigStream.ReadByte();
                        fileSize[3] = (byte)bigStream.ReadByte();


                        bigStream.Position += BitConverter.ToInt32(fileSize, 0);
                    }

                    else

                    {
                        continue;
                    }
                }

            }

            IO.NoWAVECaseCheck(position, args);



            position.Add(bigStream.Length);
            bigStream.Close();

            byte[] infile = File.ReadAllBytes(args[0]);
           
            for (int k = 1; k < position.Count; k++)

            {
                long length = position[k] - position[k - 1];
                byte[] outfile = new byte[length];
                Array.Copy(infile, position[k - 1], outfile, 0, length);
                File.WriteAllBytes(args[1] + @"\" + Path.GetFileNameWithoutExtension(args[0]) + "_" + k + ".wav", outfile);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("File: " + Path.GetFileNameWithoutExtension(args[0]) + "_" + k + ".wav" + "| Status: Extracted! | Length: " + length);
                Console.ResetColor();

            }


            timecountDetect.Stop();

            Console.WriteLine("This process took " + timecountDetect.ElapsedMilliseconds / 1000.0 + " seconds!");
            Console.WriteLine();
            Console.WriteLine(":(Sad8669");


        }
    }
}
