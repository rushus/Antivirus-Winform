using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.IO;

namespace Antivirus_winlab
{
    class Antivirus
    {
        public static void IteratingFiles(string startPath, Form1 form)
        {
            string[] folders = Directory.GetDirectories(startPath);
            foreach (string folder in folders)
            {
                IteratingFiles(folder, form);
            }
            string[] files = Directory.GetFiles(startPath, "*.exe");
            foreach (string fileName in files)
            {
                Scanner(fileName, form);
            }
        }
        static List<string> txtsign = File.ReadAllLines("E:/Games/txtsign.txt").ToList();
        public static void Scanner(string fileName, Form1 form)
        {
            foreach (var signature in txtsign)
            {
                if (CheckSignature(File.ReadAllBytes(fileName), signature))
                {
                    fileName = fileName.Replace("/", "\\");
                    form.UpdateTxt($"Сигнатура НАЙДЕНА: {fileName}\r\n");
                    fileName = Path.GetFileNameWithoutExtension(fileName);
                    form.UpdateTxt2($"{fileName}.exe\r\n");
                    return;
                }
            }
            fileName = fileName.Replace("/", "\\");
            form.UpdateTxt($"Сигнатура не найдена: {fileName}\r\n");
        }
        public static bool CheckSignature(byte[] file, string signature)
        {
            int lengthFile = file.Length;
            string[] charsSignature = signature.Split(' ');
            int lengthSignature = charsSignature.Length;
            int k = 0;
            for (int i = 0; i < lengthFile; i++)
            {
                if (charsSignature[k] == "??" ||
                byte.Parse(charsSignature[k], NumberStyles.HexNumber) == file[i])
                {
                    k++;
                }
                else
                {
                    k = 0;
                    continue;
                }
                if (k == lengthSignature)
                {
                    return true;
                }
            }
            return false;
        }
    }
}