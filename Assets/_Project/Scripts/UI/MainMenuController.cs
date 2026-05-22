using UnityEngine;
using UnityEngine.UI;
using InventixGames.Core;
using InventixGames.Core.Mission;

namespace InventixGames.UI
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private MissionDatabaseSO database;
        [SerializeField] private Button continueButton;
        [SerializeField] private Button newGameButton;
        [SerializeField] private Button quitButton;

        private void Start()
        {
            continueButton.onClick.AddListener(OnContinue);
            newGameButton.onClick.AddListener(OnNewGame);
            quitButton.onClick.AddListener(OnQuit);
            var save = ServiceLocator.Get<ISaveService>();
            continueButton.interactable = save.Data.completedMissionIds.Count > 0;
        }
        private void OnContinue()
        {
            var save = ServiceLocator.Get<ISaveService>();
            foreach (var m in database.missions)
                if (!save.IsMissionComplete(m.missionId)) { ServiceLocator.Get<IMissionService>().StartMission(m.missionId); return; }
            if (database.missions.Count > 0) ServiceLocator.Get<IMissionService>().StartMission(database.missions[database.missions.Count - 1].missionId);
        }
        private void OnNewGame()
        {
            var save = ServiceLocator.Get<ISaveService>();
            save.Data.completedMissionIds.Clear(); save.Save();
            if (database.missions.Count > 0) ServiceLocator.Get<IMissionService>().StartMission(database.missions[0].missionId);
        }
        private void OnQuit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
