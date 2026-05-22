using UnityEngine;
using System;
using System.Collections.Generic;

namespace InventixGames.Core.Mission
{
    public interface IMissionService
    {
        MissionDataSO CurrentMission { get; }
        event Action<MissionDataSO> OnMissionStarted;
        event Action<MissionDataSO> OnMissionCompleted;
        event Action<MissionObjective> OnObjectiveUpdated;
        void StartMission(string missionId);
        void ReportObjectiveProgress(string objectiveId, int delta = 1);
        bool IsObjectiveComplete(string objectiveId);
        void CompleteMission();
    }

    public class MissionManager : MonoBehaviour, IMissionService
    {
        [SerializeField] private MissionDatabaseSO database;
        public MissionDataSO CurrentMission { get; private set; }
        public event Action<MissionDataSO> OnMissionStarted;
        public event Action<MissionDataSO> OnMissionCompleted;
        public event Action<MissionObjective> OnObjectiveUpdated;
        private readonly Dictionary<string, int> _progress = new();

        public void StartMission(string missionId)
        {
            CurrentMission = database != null ? database.Get(missionId) : null;
            if (CurrentMission == null) { Debug.LogError($"[MissionManager] '{missionId}' not in database."); return; }
            _progress.Clear();
            foreach (var o in CurrentMission.objectives) _progress[o.objectiveId] = 0;
            Debug.Log($"[MissionManager] Start {CurrentMission.displayName}");
            OnMissionStarted?.Invoke(CurrentMission);
            if (!string.IsNullOrEmpty(CurrentMission.sceneName)) SceneLoader.LoadSceneAsync(CurrentMission.sceneName);
        }

        public void ReportObjectiveProgress(string objectiveId, int delta = 1)
        {
            if (CurrentMission == null || !_progress.ContainsKey(objectiveId)) return;
            var obj = CurrentMission.objectives.Find(o => o.objectiveId == objectiveId);
            if (obj == null) return;
            _progress[objectiveId] = Mathf.Min(obj.targetCount, _progress[objectiveId] + delta);
            OnObjectiveUpdated?.Invoke(obj);
            if (AllRequiredObjectivesComplete()) CompleteMission();
        }

        public bool IsObjectiveComplete(string objectiveId)
        {
            if (CurrentMission == null) return false;
            var obj = CurrentMission.objectives.Find(o => o.objectiveId == objectiveId);
            return obj != null && _progress.TryGetValue(objectiveId, out var c) && c >= obj.targetCount;
        }

        public void CompleteMission()
        {
            if (CurrentMission == null) return;
            Debug.Log($"[MissionManager] Complete {CurrentMission.displayName}");
            OnMissionCompleted?.Invoke(CurrentMission);
            if (ServiceLocator.TryGet<ISaveService>(out var save)) save.MarkMissionComplete(CurrentMission.missionId);
        }

        private bool AllRequiredObjectivesComplete()
        {
            foreach (var o in CurrentMission.objectives)
            {
                if (o.optional) continue;
                if (!_progress.TryGetValue(o.objectiveId, out var c) || c < o.targetCount) return false;
            }
            return true;
        }
    }
}
