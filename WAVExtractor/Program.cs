using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace WAVExtractor
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.Clear();
            Console.Title = "WAVExtractor";



            //////////////////////////////////////////////////////////////////////////////////////////
            if (args.Length == 2)
            {

                string fileName = Path.GetFileNameWithoutExtension(args[0]);
                byte[] infile = File.ReadAllBytes(args[0]);


                // Keeps in check the positions of headers.
                List<int> position = new List<int>();


                // Reading Loop : Just to find positions of headers..
                PositionFinder(infile, position);
             

                if (position.Count == 0)

                {
                    Console.WriteLine("No audio was found, please check your input.");
                }


                WAVMethods.DirectoryCheck(args);
                string[] dir = Directory.GetFiles(args[1]);


                // Extraction Algorithm : (Check 'MethodsForWAVExtractor.cs' for more info)
                ExtractingWAVFiles EWF = new ExtractingWAVFiles(position, infile, args, fileName);

                Console.WriteLine();
                Console.WriteLine("WAV/WEM - Extractor | :(Sad8669 / Saif Ullah");


            }


            //////////////////////////////////////////////////////////////////////////////////////
            else if (args.Length == 3 && args[2] == "-r")
            {
                string fileName = Path.GetFileNameWithoutExtension(args[0]);
                byte[] infile = File.ReadAllBytes(args[0]);


                // Keeps in check the positions of headers.
                List<int> position = new List<int>();

                position.Add(0);


                // Reading Loop : Just to find positions of headers..


                if (position.Count == 0)

                {
                    Console.WriteLine("No audio was found, please check your input.");
                }

                // Checks if "<output path> exist, if not then it creates the path. (Check 'MethodsForWAVExtractor.cs' for more info)
                WAVMethods.DirectoryCheck(args);
                string[] dir = Directory.GetFiles(args[1]);

                // Reimporting Algorithm : (Check 'MethodsForWAVExtractor.cs' for more info)
                for (int k = 1; k < position.Count; k++)

                {

                    int length = position[k] - position[k - 1]; // Absolute length of the WAV Audio.

                    foreach (string file in dir)

                    {

                        /*byte[] smallFile = File.ReadAllBytes(file);
                        Array.Copy(smallFile, 0, infile, position[k - 1], length);
                        File.WriteAllBytes(args[0], infile);*/

                        Console.WriteLine(file);

                    }

                }


            }

            /////////////////////////////////////////////////////////////////////////////////////////
            else

            {
                WAVMethods.Help();
            }

        }

        public static void PositionFinder(byte[] infile, List<int> position)
        {
            for (int i = 0; i < infile.Length - 15; i++)

            {

                // It's easy to read in strings.
                string headerType = Encoding.ASCII.GetString(infile[i..(i + 4)]);
                string subChunk = Encoding.ASCII.GetString(infile[(i + 8)..(i + 15)]);


                if (headerType == "RIFF" && subChunk == "WAVEfmt")

                {

                    position.Add(i);
                    Console.WriteLine("-> RIFF Header Found at offset(d): " + i);

                }

            }

            position.Add(infile.Length);
            
        }
    }
}

