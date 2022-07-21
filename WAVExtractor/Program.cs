using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace WAVExtractor
{
    internal class Program
    {

        static void Main(string[] args)
        {
                   
            Console.Clear();
            Console.Title = "WAVExtractor v1.3 | A Useless Guy";

            /*if (args.Length != 2)

            {
                IO.Usage();
                Environment.Exit(1);
            }*/

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
                // ---------------------------------------------- //

                // To save streams position
                List<long> position = new List<long>();


                timecountDetect.Start();


                while ((currentByte = bigStream.ReadByte()) != -1)

                {

                    for (int i = 0; i < buffer.Length - 1; i++)

                    {


                        buffer[i] = buffer[i + 1];
                    }

                    buffer[buffer.Length - 1] = (byte)currentByte;



                    if (IO.HeaderEquals(buffer, pattern))

                    {
                        if (IO.IsWave(buffer, waveFormat, bigStream))

                        {
                            bigStream.Position -= 8;
                            Console.WriteLine("-> [0] WAVE stream located at offset(d): " + (bigStream.Position - buffer.Length));
                            position.Add(bigStream.Position - buffer.Length);
                        }

                    }

                }

                IO.NoWAVECaseCheck(position, args);
                
                position.Add(bigStream.Length);


                for (int k = 1; k < position.Count; k++)

                {
                  Thread fileProgress = new Thread(() => IO.WAVStreamsCount(position.Count, k));
                  fileProgress.Start();
                  string fileName = Path.GetFileNameWithoutExtension(args[0]);

                  long length = position[k] - position[k - 1];
                  bigStream.Position = position[k - 1];
                  byte[] outfile = bigStream.ReadBytes((int)length);

                  File.WriteAllBytes(args[1] + @"\" + fileName + "_" + k + ".wav", outfile);
                  Visual.Yellow("-> File: " + fileName + "_" + k + ".wav" + "| Status: Extracted! | Length: " + length);

                if (args.Length == 3 && args[2] == "-verbose")
               
                {
                    VerboseMode.ShowInfo(outfile);
                }
            }

               timecountDetect.Stop();
                
                Console.WriteLine();
                Console.WriteLine("Time Taken: " + timecountDetect.ElapsedMilliseconds / 1000.0 + " seconds!");


            }

      

        }
    }
