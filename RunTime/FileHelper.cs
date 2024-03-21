using System.IO;


namespace VitoBarra.GeneralUtility
{
    public static class FileHelper
    {
        public static string ReadFile(string path)
        {
            using StreamReader reader = new StreamReader(path);
            return reader.ReadToEnd();
        }

        public static void WriteFile(string path, string content)
        {
            FileInfo FilePath = new FileInfo(path);
            Directory.CreateDirectory(FilePath.Directory.ToString());
            File.WriteAllText(path, content);
        }
    }
}