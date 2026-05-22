using UnityEngine;
using InventixGames.Core;
using InventixGames.Core.Mission;

namespace HearthAndHex.MissionOne
{
    public class Mission01Director : MonoBehaviour
    {
        [SerializeField] private string missionId = "M01";
        [SerializeField] private Transform playerSpawn;
        [SerializeField] private GameObject openingCutscene;
        [SerializeField] private GameObject completionPanel;
        private IMissionService _mission;

        private void Start()
        {
            _mission = ServiceLocator.Get<IMissionService>();
            _mission.OnObjectiveUpdated += OnObjective;
            _mission.OnMissionCompleted += OnComplete;
            var player = GameObject.FindWithTag("Player");
            if (player && playerSpawn) player.transform.SetPositionAndRotation(playerSpawn.position, playerSpawn.rotation);
            if (openingCutscene) openingCutscene.SetActive(true);
        }
        private void OnDestroy()
        {
            if (_mission == null) return;
            _mission.OnObjectiveUpdated -= OnObjective;
            _mission.OnMissionCompleted -= OnComplete;
        }
        private void OnObjective(MissionObjective o) { if (o.objectiveId == "m1_meet_elra" || o.objectiveId == "m1_meet_benn") if (ServiceLocator.TryGet<ISaveService>(out var save)) save.Save(); }
        private void OnComplete(MissionDataSO m) { if (m.missionId == missionId && completionPanel) completionPanel.SetActive(true); }
    }
}
