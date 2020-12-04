using System.IO;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

namespace FeelGoodOpgUtils
{
    public static class FileController
    {
        public const string Xml = "xml";

        public static void SerializeXml(object item, string fileName, string path)
        {
            string pathFolder = Path.Combine(Application.persistentDataPath, path);
            Directory.CreateDirectory(pathFolder);
            string fullPath = Path.Combine(Application.persistentDataPath, path, $"{fileName}.xml");
            XmlSerializer serializer = new XmlSerializer(item.GetType());
            StreamWriter writer = new StreamWriter(fullPath);
            serializer.Serialize(writer.BaseStream, item);
            writer.Close();
        }

        public static T DeserializeXml<T>(string fileName, string path)
        {
            string fullPath = Path.Combine(Application.persistentDataPath, path, $"{fileName}.xml");
            if (File.Exists(fullPath) == false)
                return default;

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StreamReader reader = new StreamReader(fullPath);
            T deserialized = (T)serializer.Deserialize(reader.BaseStream);
            reader.Close();
            return deserialized;
        }

        /// <summary>Copies all files with the specified extension from the specified folder
        /// inside the streaming assets path into the persistent data path.</summary>
        public static void CopyFolderIfNotExists(string folderName, string extension = Xml)
        {
            string path = Path.Combine(Application.persistentDataPath, folderName);
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            string assetsPath = Path.Combine(Application.streamingAssetsPath, folderName);
            foreach (string fileName in Directory.GetFiles(assetsPath, $"*.{extension}"))
            {
                string[] nameSplit = fileName.Split(Path.DirectorySeparatorChar);
                string newPath = Path.Combine(path, nameSplit[nameSplit.Length - 1]);
                File.Copy(fileName, newPath, true);
            }
        }

        public static string[] GetAllFilesInFolder(string folderName, bool includeExtensions = false)
        {
            string path = Path.Combine(Application.persistentDataPath, folderName);
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            string[] files = Directory.GetFiles(path);

            if (includeExtensions == true)
                return files.Select(f => Path.GetFileName(f)).ToArray();
            else
                return files.Select(f => Path.GetFileNameWithoutExtension(f)).ToArray();
        }

        public static void DeleteFile(string fileName, string folder, string extension = Xml)
        {
            string path = Path.Combine(Application.persistentDataPath, folder, $"{fileName}.{extension}");
            File.Delete(path);
        }

        public static bool FileExists(string fileName, string path, string extension = Xml)
        {
            string pathFolder = Path.Combine(Application.persistentDataPath, path);
            Directory.CreateDirectory(pathFolder);
            string fullPath = Path.Combine(Application.persistentDataPath, path, $"{fileName}.{extension}");
            return File.Exists(fullPath);
        }
    }
}
