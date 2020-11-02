using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Net;
using System.Text;
// ReSharper disable ConvertIfStatementToSwitchStatement

namespace mUtilities
{
    static class UtilMain
    {
        static void Main()
        {
            string Ans;

            Completed:
            Console.WriteLine("\nHeyo! Welcome to the mUtil multi-tool. Press a matching key to use a utility function.");
            Console.WriteLine("A. Number System Converter\nB. MD5 String hashing\nC. Base64 Encoding/Decoding\nD. Check for internet connection\nE. Random Password Generator\nF. Logic Operators\nQ. Quit.");
            errorcheck:
            Ans = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(Ans))
            {
                goto errorcheck;
            }
            
            switch (Ans.ToUpper())
            {
                case "A":
                    NumberSysConv();
                    break;
                case "B":
                    MD5Hash();
                    break;
                case "C":
                    base64();
                    break;
                case "D":
                    Console.WriteLine(InternetCon());
                    break;
                case "E":
                    PassManager();
                    break;
                case "F":
                    BooleanLogic();
                    break;
                case "Q":
                    Environment.Exit(1);
                    break;
                default:
                    goto errorcheck;
            }

            goto Completed;
        }

        static void NumberSysConv()
        {
            retry:
            Console.WriteLine("What base would you like to convert from?");
            int fromBase = int.Parse(Console.ReadLine());
            Console.WriteLine("What value of this base would you like to convert?");
            int typeBase = int.Parse(Console.ReadLine());
            Console.WriteLine("What is the new base you'd like to convert the valaue to?");
            int newBase = int.Parse(Console.ReadLine());

            try
            {
                string BaseVal = Convert.ToString(fromBase, typeBase);
                Console.WriteLine(Convert.ToString(int.Parse(BaseVal), newBase));
            }
            catch (Exception)
            {
                Console.WriteLine("There was an error while converting, you made an error, please try again.");
                goto retry;
            }
        }

        static void MD5Hash()
        {

            redo:

            Console.WriteLine("Write a string you'd like to have hashed.");
            string? response = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(response))
            {
                goto redo;
            }

            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(response));

            foreach (var t in bytes)
            {
                hash.Append(t.ToString("x2"));
            }

            Console.WriteLine(hash.ToString());
            hash.Clear();

            Console.WriteLine("Would you like another string to be hashed?");
            string? response2 = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(response2))
            {
                goto end;
            }

            if (response2.ToLower() == "yes" || response2.ToLower() == "y" || response2.ToLower() == "true")
            {
                goto redo;
            }
            
            end: ;
        }

        static void base64()
        {
            redo:
            Console.WriteLine("Would you like to encode or decode your text? (e or d)");
            var op = Console.ReadLine();
            
            if (op.ToLower().Trim() == "e" || op.ToLower().Trim() == "encode")
            {
                op = "e";
                Console.WriteLine("What would you like to encode?");
            }
            else if (op.ToLower().Trim() == "d" || op.ToLower().Trim() == "decode")
            {
                op = "d";
                Console.WriteLine("What would you like to decode?");
            }
            
            var opCode = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(opCode))
            {
                goto redo;
            }
            
            switch (op)
            {
                case "e":
                    var Encoded = Encode(opCode);
                    Console.WriteLine(Encoded + "\n");
                    Console.WriteLine("Would you like to encode/decode another string? (yes,y/no,n)");
                    var x = Console.ReadLine();
                    if (x.ToLower().Trim() == "y" || x.ToLower().Trim() == "yes")
                    {
                        goto redo;
                    }
                    else
                    {
                        break;
                    }
                case "d":
                    string output = Decode(opCode);
                    if (output == "This is not a base 64 string, please try again.")
                    {
                        goto redo;
                    }
                    else
                    {
                        Console.WriteLine(output);
                    }
                    var y = Console.ReadLine();
                    if (y.ToLower().Trim() == "y" || y.ToLower().Trim() == "yes")
                    {
                        goto redo;
                    }
                    else
                    {
                        break;
                    }
                default:
                    goto redo;
            }
        }

        static string Encode(string texttoencode)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(texttoencode));
        }

        static string Decode(string texttodecode)
        {
            try
            {
                return Encoding.UTF8.GetString(Convert.FromBase64String(texttodecode));
            }
            catch (Exception)
            {
                return "This is not a base 64 string, please try again.";
            }
        }

        static void PassManager()
        {
            int input;
            
            retry:
            Console.WriteLine("Choose how many characters you want the new password to be? (between 8-64 characters)");
            try
            {
                input = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                goto retry;
            }
            
            if (input < 8 || input > 64)
            {
                goto retry;
            }

            try
            {
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < input; i++)
                {
                    const string chars = "@!*abcdefghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ&";
                    Random rand = new Random();
                    int num = rand.Next(0, chars.Length - 1);
                    sb.Append(chars[num]);
                }

                Console.WriteLine(sb.ToString());
                sb.Clear();
            }
            catch (Exception)
            {
                Console.WriteLine("An error occured making a new string, please try again.");
                goto retry;
            }

            Console.WriteLine("Would you like to generate a new password?");
            var Answer = Console.ReadLine();
            if (Answer.ToLower().Trim() == "y" || Answer.ToLower().Trim() == "yes")
            {
                goto retry;
            }
        }

        static bool InternetCon()
        {
            Console.WriteLine("\nTrue = There is an internet connection.\nFalse = There is no internet connection.\n");
            
            using WebClient x = new WebClient();
            try
            {
                x.DownloadString("https://www.google.com");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        static void BooleanLogic()
        {
            redobArrayCount:
            Console.WriteLine("How many bit arrays do you want to have?");
            bool bArrayCCheck = int.TryParse(Console.ReadLine(), out int bArrayCount);
            if (!bArrayCCheck || bArrayCount <= 0 || bArrayCount > 4)
            {
                goto redobArrayCount;
            }

            List<BitArray> bArrays = new List<BitArray>(bArrayCount);
            

            foreach (var i in bArrays.Select(bAr => 0).Select(i => i + 1))
            {
                redoBitsForBitArray:
                Console.WriteLine("How many bits would you like in bit array " + i + "?");
                bool bitCCheck = int.TryParse(Console.ReadLine(), out int bitCount);
                if (!bitCCheck || bitCount <= 0 || bitCount > 8)
                {
                    goto redoBitsForBitArray;
                }

                bArrays[i].Length+=bitCount;
            }

            foreach (var bArr in bArrays)
            {
                for (int i = 0; i < bArr.Count; i++)
                {
                    bitState:
                    Console.WriteLine("What state would you like bit " + i+1 + " to be in? (1 or 0)");
                    bool SoBCheck = int.TryParse(Console.ReadLine(), out int StateofBitVal);
                    if (!SoBCheck || StateofBitVal < 0 || StateofBitVal > 1)
                    {
                        goto bitState;
                    }

                    bool StateOfBit = StateofBitVal == 1 ? StateOfBit = true : StateOfBit = false;
                    bArr[i] = StateOfBit;
                }
            }


            Console.WriteLine("How many bits would you like for there t");

            /*
             * Choose how many arrays they wish for ✔
             * Choose how many bits they want in each array ✔
             * Choose the state of each value in the array [nested for]
             * Perform AND, OR, XOR, NOT operation on chosen bit arrays
             * Choose whether they want to use NOT on the whole bit array or parts of the array
             * Create an output bit for bit operations
             */
        }
    }
}

/*
            foreach (var bAr in bArrays)
            {
                int i = 0;
                i += 1;
                redoBitsForBitArray:
                Console.WriteLine("How many bits would you like in bit array " + i + "?");
                bool bitCCheck = int.TryParse(Console.ReadLine(), out int bitCount);
                if (!bitCCheck || bitCount <= 0 || bitCount > 8)
                {
                    goto redoBitsForBitArray;
                }
            }
*/