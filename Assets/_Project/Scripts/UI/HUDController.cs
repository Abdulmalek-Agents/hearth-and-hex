using UnityEngine;
using UnityEngine.UI;
using TMPro;
using InventixGames.Core;
using InventixGames.Core.Mission;

namespace InventixGames.UI
{
    public class HUDController : MonoBehaviour
    {
        [SerializeField] private TMP_Text missionTitleText;
        [SerializeField] private TMP_Text objectiveText;
        [SerializeField] private Slider missionProgressBar;
        private IMissionService _mission;

        private void OnEnable()
        {
            _mission = ServiceLocator.Get<IMissionService>();
            _mission.OnMissionStarted += OnStart;
            _mission.OnObjectiveUpdated += OnObjective;
            _mission.OnMissionCompleted += OnComplete;
        }
        private void OnDisable()
        {
            if (_mission == null) return;
            _mission.OnMissionStarted -= OnStart;
            _mission.OnObjectiveUpdated -= OnObjective;
            _mission.OnMissionCompleted -= OnComplete;
        }
        private void OnStart(MissionDataSO m) { if (missionTitleText) missionTitleText.text = m.displayName; if (objectiveText && m.objectives.Count > 0) objectiveText.text = m.objectives[0].description; }
        private void OnObjective(MissionObjective o) { if (objectiveText) objectiveText.text = o.description; }
        private void OnComplete(MissionDataSO m) { if (objectiveText) objectiveText.text = "Mission Complete!"; }
    }
}
