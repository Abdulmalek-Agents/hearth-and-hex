using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace InventixGames.Core
{
    public interface ISaveService
    {
        SaveData Data { get; }
        void Save();
        void Load();
        void MarkMissionComplete(string missionId);
        bool IsMissionComplete(string missionId);
    }

    [Serializable]
    public class SaveData
    {
        public List<string> completedMissionIds = new();
        public int currency;
        public int xp;
        public Dictionary<string, string> kv = new();
    }

    public class JsonSaveService : ISaveService
    {
        private const string FileName = "save.json";
        public SaveData Data { get; private set; } = new();
        public JsonSaveService() { Load(); }
        private string Path => System.IO.Path.Combine(Application.persistentDataPath, FileName);

        public void Save() => File.WriteAllText(Path, JsonUtility.ToJson(Data, true));
        public void Load()
        {
            try { if (File.Exists(Path)) Data = JsonUtility.FromJson<SaveData>(File.ReadAllText(Path)) ?? new SaveData(); }
            catch (Exception e) { Debug.LogWarning($"[Save] {e.Message}"); Data = new SaveData(); }
        }
        public void MarkMissionComplete(string missionId)
        {
            if (!Data.completedMissionIds.Contains(missionId)) Data.completedMissionIds.Add(missionId);
            Save();
        }
        public bool IsMissionComplete(string missionId) => Data.completedMissionIds.Contains(missionId);
    }
}
