using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Unity.Mathematics;
using System;

public class AndroidFileManager : MonoBehaviour
{
    // ... (Your existing code)

    private string rootFolderName = "robot_app";
    private string subFolderName = "user_data";
    private string fileName = "Keyframes.txt";
    private string keyframesFilePath_to_txt;
    private List<RobotKeyframe> keyframes = new List<RobotKeyframe>();

    void Start()
    {
        keyframesFilePath_to_txt = GetFilePath();
        // ... (Your existing code)
    }

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

    private void SaveKeyframesToFile()
    {
        CreateFoldersAndFile(); // Ensure folders and file exist
        using (StreamWriter writer = new StreamWriter(keyframesFilePath_to_txt))
        {
            foreach (var keyframe in keyframes)
            {
                // Write each keyframe data to the file
                writer.WriteLine($"{keyframe.Slider1} {keyframe.Slider2} {keyframe.Slider3} {keyframe.Slider4} {keyframe.Slider5} {keyframe.Slider6}");
            }
        }

        Debug.Log("Keyframes saved to file: " + keyframesFilePath_to_txt);
    }

    private List<RobotKeyframe> LoadKeyframesFromFile()
    {
        List<RobotKeyframe> loadedKeyframes = new List<RobotKeyframe>();
        using (StreamReader reader = new StreamReader(keyframesFilePath_to_txt))
        {
            while (!reader.EndOfStream)
            {
                string[] values = reader.ReadLine().Split(' ');
                float s1 = float.Parse(values[0]);
                float s2 = float.Parse(values[1]);
                float s3 = float.Parse(values[2]);
                float s4 = float.Parse(values[3]);
                float s5 = float.Parse(values[4]);
                float s6 = float.Parse(values[5]);

                loadedKeyframes.Add(new RobotKeyframe(s1, s2, s3, s4, s5, s6));
            }
        }
        return loadedKeyframes;
    }

    // ... (Your existing code)

    public void SaveKeyframesToTxtFile()
    {
        SaveKeyframesToFile();
    }

    public List<RobotKeyframe> LoadKeyframesFromTxtFile()
    {
        return LoadKeyframesFromFile();
    }

    // ... (Your existing code)
}
