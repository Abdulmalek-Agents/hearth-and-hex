using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InventixGames.Core
{
    public static class SceneLoader
    {
        public static void LoadSceneAsync(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
            => CoroutineRunner.Run(LoadRoutine(sceneName, mode));

        private static IEnumerator LoadRoutine(string sceneName, LoadSceneMode mode)
        {
            var op = SceneManager.LoadSceneAsync(sceneName, mode);
            op.allowSceneActivation = false;
            while (op.progress < 0.9f) { LoadingScreen.SetProgress(op.progress / 0.9f); yield return null; }
            LoadingScreen.SetProgress(1f);
            op.allowSceneActivation = true;
        }
    }

    public class CoroutineRunner : MonoBehaviour
    {
        private static CoroutineRunner _i;
        public static void Run(IEnumerator r)
        {
            if (_i == null) { var go = new GameObject("[CoroutineRunner]"); Object.DontDestroyOnLoad(go); _i = go.AddComponent<CoroutineRunner>(); }
            _i.StartCoroutine(r);
        }
    }

    public static class LoadingScreen
    {
        public static event System.Action<float> ProgressChanged;
        public static void SetProgress(float pct) => ProgressChanged?.Invoke(Mathf.Clamp01(pct));
    }
}
