using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VigenereCipher
{
    class Program
    {
        private static string Key;
        private static string Message;

        static void Main(string[] args)
        {
            if (args.Length == 3)
            {
                Key = args[1].ToUpper();
                Message = args[2].ToUpper();

                switch (args[0])
                {
                    case "/d":
                        Console.WriteLine($"Decoded '{Message}' using '{Key}' : '{Decrypt()}'");
                        break;
                    case "/e":
                        Console.WriteLine($"Encoded '{Message}' using '{Key}' : '{Encrypt()}'");
                        break;
                    default:
                        Console.WriteLine("Invalid operation. Please use /e or /d.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Usage: [/e or /d] [key] [msg]\n\n/e\t- Encode msg with the key using the Vigenère Cipher\n/d\t- Decode msg with the key using the Vigenère Cipher");
            }
        }

        private static string Encrypt()
        {
            string result = "";
            for (int i = 0; i < Message.Length; i++) result += GetCorrespondingLetter(Message[i], i, true);
            return result;
        }

        private static string Decrypt()
        {
            string result = "";
            for (int i = 0; i < Message.Length; i++) result += GetCorrespondingLetter(Message[i], i, false);
            return result;
        }

        private static char GetCorrespondingLetter(char c, int index, bool addOperation)
        {
            int row = IndexOfLetterInAlphabet(Key[index % Key.Length]);
            int col = IndexOfLetterInAlphabet(c);
            int i;
            if (addOperation) i = (col + row + 26) % 26;
            else i = (col - row + 26) % 26;
            return LetterFromIndexInAlphabet(i);
        }
        
        private static char LetterFromIndexInAlphabet(int i) => (char)(i + 65);
        private static int IndexOfLetterInAlphabet(char l) => l - 65;
    }
}
