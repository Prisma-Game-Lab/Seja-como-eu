﻿using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SaveSystem : MonoBehaviour
{
    [HideInInspector]
    public GeneralCounts generalCounts;
    public GeneralCounts emptySave;

    public static float saveVersion = 1.0f;
    
    private static string saveFileName = "save";
    private static string SavePath
    {
        get
        {
            return Path.Combine(Application.persistentDataPath, saveFileName + ".dat");
        }
    }

    private static string versionSaveName = "version";
    private static string VersionSavePath
    {
        get
        {
            return Path.Combine(Application.persistentDataPath, versionSaveName + ".ver");
        }
    }

    private static SaveSystem instance;
    public static SaveSystem GetInstance()
    {
        return instance;
    }

    private void Awake() {
        if(instance != null)
        {
            GameObject.Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            if(!LoadState())
            {
                generalCounts = GameObject.Instantiate(emptySave);
                string path = Path.Combine(Application.persistentDataPath, saveFileName + ".dat");
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private bool ValidateOldSave() {
        Debug.LogWarning("Updating old save files was not yet implemented");
        return false;
    }

    public void SaveState() {
        string json_ps = JsonUtility.ToJson(generalCounts);
        byte[] buffer = Encoding.UTF8.GetBytes(json_ps);
        for(int i = 0; i < buffer.Length; i++)
        {
            buffer[i] = (Byte)((~buffer[i]) & 0xFF);
        }
        try
        {
            File.WriteAllBytes(SavePath, buffer);
        }
        catch (Exception e)
        {
            Debug.LogWarning("Unable to write to savefile" + e.Message);
        }
        
        using (StreamWriter streamWriter = File.CreateText (VersionSavePath))
        {
            streamWriter.Write (saveVersion.ToString());
        }
    }

    public bool LoadState() {
        string path = SavePath;
        string versionPath = VersionSavePath;
        if(!File.Exists(path) || !File.Exists(versionPath)) {
            return false;
        }

        using(StreamReader streamReader = File.OpenText(versionPath)) {
            string str = streamReader.ReadToEnd();
            try {
                float ver = float.Parse(str);
                if(ver < saveVersion) {
                    Debug.LogWarning("Warning: old save version");
                    return ValidateOldSave();
                }
            }
            catch (Exception e) {
                Debug.LogWarning("Warning: invalid save-version format" + e.Message);
                return ValidateOldSave();
            }
        }

        try {
            byte[] buffer = File.ReadAllBytes(path);
            for(int i=0;i< buffer.Length;i ++) {
                buffer[i] = (Byte)((~buffer[i]) & 0xFF);
            }
            string jsonString = Encoding.UTF8.GetString(buffer);
            generalCounts = ScriptableObject.CreateInstance<GeneralCounts>();
            JsonUtility.FromJsonOverwrite(jsonString, generalCounts);
            return true;
        }
        catch (Exception e) {
            Debug.LogWarning("Unable to load savefile " + e.Message);
            return false;
        }
    }  

    public void ClearState() {
        generalCounts = GameObject.Instantiate(emptySave);
    }

    public void NewGame() {
        generalCounts = GameObject.Instantiate(emptySave);
        SaveState();
        string path = Path.Combine(Application.persistentDataPath, saveFileName + ".dat");
        Debug.Log("new save on path:" + path);
    }

    public static void DeleteSaveFile() {
        try {
            File.Delete(SavePath);
            File.Delete(VersionSavePath);
            if(instance != null) {
                instance.generalCounts = GameObject.Instantiate(instance.emptySave);
            }
        }
        catch (Exception e) {
            Debug.LogWarning("Could not delete file:" + e.Message);
        }
    }  
}
