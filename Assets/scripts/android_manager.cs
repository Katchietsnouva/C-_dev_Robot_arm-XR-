using UnityEngine;
using System.IO;

public class FileManager : MonoBehaviour
{
    private string rootFolderName = "robot_app";
    private string subFolderName = "user_data";
    private string fileName = "user_logs.txt";

    private string GetFilePath()
    {
        string rootPath = Path.Combine(Application.persistentDataPath, rootFolderName);
        string subFolderPath = Path.Combine(rootPath, subFolderName);
        string filePath = Path.Combine(subFolderPath, fileName);

        return filePath;
    }

    private void CreateFoldersAndFile()
    {
        string rootPath = Path.Combine(Application.persistentDataPath, rootFolderName);
        string subFolderPath = Path.Combine(rootPath, subFolderName);

        if (!Directory.Exists(rootPath))
        {
            Directory.CreateDirectory(rootPath);
        }

        if (!Directory.Exists(subFolderPath))
        {
            Directory.CreateDirectory(subFolderPath);
        }

        // You can create the file here if needed
        // string filePath = Path.Combine(subFolderPath, fileName);
        // File.WriteAllText(filePath, "Initial content");
    }

    private void SaveData(string data)
    {
        string filePath = GetFilePath();
        File.WriteAllText(filePath, data);
    }

    private string LoadData()
    {
        string filePath = GetFilePath();

        if (File.Exists(filePath))
        {
            return File.ReadAllText(filePath);
        }
        else
        {
            Debug.LogError("File not found: " + filePath);
            return null;
        }
    }

    // Example usage
    void Start()
    {
        CreateFoldersAndFile();
        SaveData("Hello, this is user data!");
        string loadedData = LoadData();
        Debug.Log("Loaded data: " + loadedData);
    }
}
