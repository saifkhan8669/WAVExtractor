using System;
using System.Collections.Generic;
using System.Text;

namespace WAVExtractor
{
    class ExtractingWAVFiles
    {
        public ExtractingWAVFiles(List<int> position, byte[] infile, string[] args, string fileName)
        {
            for (int k = 1; k < position.Count; k++)

            {
                int length = position[k] - position[k - 1];
                byte[] outfile = new byte[length];
                Array.Copy(infile, position[k - 1], outfile, 0, length);
                File.WriteAllBytes(args[1] + @"\" + fileName + k + ".wav", outfile);
                Console.WriteLine(fileName + k + ".wav " + "of length " + length + " ....Extracted!");

            }
        }

    }

    class WAVMethods
    {
        public static void Help()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Syntax: " + AppDomain.CurrentDomain.FriendlyName + " <input file> <output path>");  // Syntax
            Console.WriteLine();
            Console.WriteLine("<input file> - The archive which contains WAV audio");
            Console.WriteLine("<output path> - Path to store the extracted audio.");
            Console.WriteLine();
            Console.WriteLine("WAV/WEM - Extractor | :(Sad8669 / Saif Ullah");


        }

        public static void DirectoryCheck(string[] args)
        {
            if (!Directory.Exists(args[1]))

            {
                Directory.CreateDirectory(args[1]);
            }
        }


    }

    class ReimportingWAVFiles
    {
        public ReimportingWAVFiles(List<int> position, byte[] infile, string[] args, string fileName,string[] dir)
        {
            for (int k = 1; k < position.Count; k++)  

            {

                int length = position[k] - position[k - 1]; // Absolute length of the WAV Audio.

                foreach (string file in dir)
                
                {

                    byte[] smallFile = File.ReadAllBytes(file);
                    Array.Copy(smallFile, 0, infile, position[k - 1], length);
                    File.WriteAllBytes(args[0], infile);

                }

            }
        }

    }
}
