﻿using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace FeelGoodOpgUtils
{
	public static class FileController
	{
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
			XmlSerializer serializer = new XmlSerializer(typeof(T));
			StreamReader reader = new StreamReader(fullPath);
			T deserialized = (T)serializer.Deserialize(reader.BaseStream);
			reader.Close();
			return deserialized;
		}

		/// <summary>Copies all files with the specified extension from the specified folder
		/// inside the streaming assets path into the persistent data path.</summary>
		public static void CopyFolderIfNotExists(string folderName, string extension)
		{
			string path = Path.Combine(Application.persistentDataPath, folderName);
			if (Directory.Exists(path) == false)
			{
				Directory.CreateDirectory(path);
			}
			string assetsPath = Path.Combine(Application.streamingAssetsPath, folderName);
			foreach (string fileName in Directory.GetFiles(assetsPath, $"*.{extension}"))
			{
				string[] nameSplit = fileName.Split('\\');
				string newPath = Path.Combine(path, nameSplit[nameSplit.Length - 1]);
				if (File.Exists(newPath) == false)
				{
					File.Copy(fileName, newPath);
				}
			}
		}

		public static string[] GetAllFilesInFolder(string folderName)
		{
			string path = Path.Combine(Application.persistentDataPath, folderName);
			return Directory.GetFiles(path);
		}

		public static bool FileExists(string fileName, string path, string extension)
		{
			string pathFolder = Path.Combine(Application.persistentDataPath, path);
			Directory.CreateDirectory(pathFolder);
			string fullPath = Path.Combine(Application.persistentDataPath, path, $"{fileName}.{extension}");
			return File.Exists(fullPath);
		}
	}
}