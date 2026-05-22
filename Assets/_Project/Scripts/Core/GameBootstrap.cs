using UnityEngine;
using InventixGames.Core.Dialogue;

namespace InventixGames.Core
{
    /// <summary>
    /// First object to spawn at runtime. Lives on a Bootstrap scene (build idx 0).
    /// Registers core services and loads the main menu.
    /// Scalable: Mission 1..N all flow through the same services.
    /// v0.2: removed ClaudeCopilotService; registers ScriptedDialogueService instead.
    /// </summary>
    [DefaultExecutionOrder(-10000)]
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private string firstSceneToLoad = "MainMenu";
        [SerializeField] private bool dontDestroyOnLoad = true;

        private void Awake()
        {
            if (dontDestroyOnLoad) DontDestroyOnLoad(gameObject);
            Application.targetFrameRate = 60;
            QualitySettings.vSyncCount = 1;

            ServiceLocator.Register<ISaveService>(new JsonSaveService());
            ServiceLocator.Register<IAudioService>(gameObject.AddComponent<AudioService>());
            ServiceLocator.Register<Mission.IMissionService>(gameObject.AddComponent<Mission.MissionManager>());
            ServiceLocator.Register<ICheckpointService>(gameObject.AddComponent<CheckpointService>());
            ServiceLocator.Register<IScriptedDialogueService>(gameObject.AddComponent<ScriptedDialogueService>());
            Debug.Log("[GameBootstrap] Core services registered.");
        }

        private void Start() => SceneLoader.LoadSceneAsync(firstSceneToLoad);
    }
}
