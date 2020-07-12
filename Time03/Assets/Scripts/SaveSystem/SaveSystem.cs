using System;
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

    public static bool SucessfulLoad = false;
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

    // Feedback ---------------

    private static string FeedbackFileName = "feedback";

    public static string FeedbackPath
    {
        get
        {
            return Path.Combine(Application.persistentDataPath, FeedbackFileName + ".log");
        }
    }

    // --------------------------


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
                
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private bool ValidateOldSave() {
        Debug.LogWarning("Updating old save files was not yet implemented");
        return false;
    }

    public void SaveState() {
        for(int i=0;i<generalCounts.Events.Count;i++) {
            generalCounts.EventsBools[i] = generalCounts.Events[generalCounts.EventsStrings[i]];
        }

        for(int i=0;i<generalCounts.Stats.Count;i++) {
            generalCounts.StatsInts[i] = generalCounts.Stats[generalCounts.StatsStrings[i]];
        }

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

        if(generalCounts.DictionaryExists) {
            WriteFeedbackLog();
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

            for(int i=0;i<generalCounts.EventsStrings.Count;i++) {
                generalCounts.Events.Add(generalCounts.EventsStrings[i],generalCounts.EventsBools[i]);
            }

            for(int i=0;i<generalCounts.StatsStrings.Count;i++) {
                generalCounts.Stats.Add(generalCounts.StatsStrings[i],generalCounts.StatsInts[i]);
            }

            SucessfulLoad = true;
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
        SucessfulLoad = true;
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

    public void WriteFeedbackLog() {
        int TotalDashs;
        int TotalDeaths;
        TotalDashs = generalCounts.Stats["HubDashCount"] + generalCounts.Stats["CarinhoDashCount"] + generalCounts.Stats["TristezaDashCount"] +
        generalCounts.Stats["ExpressividadeDashCount"] + generalCounts.Stats["MDMDashCount"];
        TotalDeaths = generalCounts.Stats["CarinhoDeathCount"] + generalCounts.Stats["TristezaDeathCount"] + generalCounts.Stats["MDMDeathCount"] +
        generalCounts.Stats["ExpressividadeDeathCount"];
        using(StreamWriter sw = new StreamWriter(FeedbackPath,false)) {
            sw.WriteLine($"Número de Rolamentos: {TotalDashs}");
            sw.WriteLine($"Número de Mortes: {TotalDeaths}");
            sw.WriteLine($"Número de Rolamentos no Hub: {generalCounts.Stats["HubDashCount"]}");
            sw.WriteLine($"Tempo de Batalha do Carinho: {ConvertToTime(generalCounts.CarinhoCompleteTimer)}");
            sw.WriteLine($"Número de Rolamentos no Carinho: {generalCounts.Stats["CarinhoDashCount"]}");
            sw.WriteLine($"Número de Mortes no Carinho: {generalCounts.Stats["CarinhoDeathCount"]}");
            sw.WriteLine($"Tempo de Batalha da Tristeza: {ConvertToTime(generalCounts.TristezaCompleteTimer)}");
            sw.WriteLine($"Número de Rolamentos na Tristeza: {generalCounts.Stats["TristezaDashCount"]}");
            sw.WriteLine($"Número de Mortes na Tristeza: {generalCounts.Stats["TristezaDeathCount"]}");
            sw.WriteLine($"Tempo de Batalha da Expressividade: {ConvertToTime(generalCounts.ExpressividadeCompleteTimer)}");
            sw.WriteLine($"Número de Rolamentos na Expressividade: {generalCounts.Stats["ExpressividadeDashCount"]}");
            sw.WriteLine($"Número de Mortes na Expressividade: {generalCounts.Stats["ExpressividadeDeathCount"]}");
            sw.WriteLine($"Tempo de Batalha do Mestre dos Machos: {ConvertToTime(generalCounts.MDMCompleteTimer)}");
            sw.WriteLine($"Número de Rolamentos no Mestre dos Machos: {generalCounts.Stats["MDMDashCount"]}");
            sw.WriteLine($"Número de Mortes no Mestre dos Machos: {generalCounts.Stats["MDMDeathCount"]}");
            sw.WriteLine($"Tempo Total de Jogo: {ConvertToTime(generalCounts.TotalPlayTime)}");
        }
    }  

    private string ConvertToTime(float time) {
        float minutes = time/60;
        float seconds = time%60;
        string min = minutes.ToString("00");
        string sec = seconds.ToString("00");
        return $"{min}:{sec}";
    }
}
