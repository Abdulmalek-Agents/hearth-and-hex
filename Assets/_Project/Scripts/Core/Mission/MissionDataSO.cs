using UnityEngine;
using System.Collections.Generic;

namespace InventixGames.Core.Mission
{
    [CreateAssetMenu(menuName = "Inventix/Mission/Mission Data", fileName = "MissionData_")]
    public class MissionDataSO : ScriptableObject
    {
        [Header("Identity")]
        public string missionId = "M01";
        public string displayName = "Mission";
        [TextArea(3, 6)] public string briefingText;
        [Header("Scene")]
        public string sceneName;
        public string addressableSceneKey;
        [Header("Pacing")]
        public float targetDurationMinutes = 12f;
        [Header("Objectives")]
        public List<MissionObjective> objectives = new();
        [Header("Unlocking")]
        public List<string> prerequisiteMissionIds = new();
        [Header("Rewards")]
        public int xpReward = 100;
        public int currencyReward = 50;
    }

    [System.Serializable]
    public class MissionObjective
    {
        public string objectiveId;
        public string description;
        public ObjectiveType type;
        public int targetCount = 1;
        public bool optional;
    }

    public enum ObjectiveType { ReachLocation, DefeatEnemies, CollectItems, InteractWith, EscortNpc, Survive, Custom }
}
