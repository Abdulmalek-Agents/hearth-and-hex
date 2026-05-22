using UnityEngine;
using InventixGames.Core;
using InventixGames.Core.Mission;

namespace HearthAndHex.Farming
{
    public enum PlotState { Wild, Tilled, Planted, Watered, Growing, ReadyToHarvest }

    [RequireComponent(typeof(Collider))]
    public class FarmPlot : MonoBehaviour, IInteractable
    {
        [SerializeField] private string plotId;
        [SerializeField] private PlotState state = PlotState.Wild;
        [SerializeField] private CropDataSO crop;
        [SerializeField] private Transform growthAnchor;
        [SerializeField] private string tillObjectiveId = "m1_till_plot";
        [SerializeField] private string plantObjectiveId = "m1_plant_seeds";
        [SerializeField] private string waterObjectiveId = "m1_water_plants";

        private GameObject _currentStage;
        private float _plantedAtMinute;
        private int _currentStageIndex;

        public string Prompt => state switch
        {
            PlotState.Wild => "Clear & till",
            PlotState.Tilled => "Plant seeds",
            PlotState.Planted => "Water",
            PlotState.ReadyToHarvest => "Harvest",
            _ => null
        };

        public void Interact(GameObject actor)
        {
            switch (state)
            {
                case PlotState.Wild: Till(); break;
                case PlotState.Tilled: Plant(); break;
                case PlotState.Planted: Water(); break;
                case PlotState.ReadyToHarvest: Harvest(); break;
            }
        }

        private void Till() { state = PlotState.Tilled; ReportObjective(tillObjectiveId); }
        private void Plant()
        {
            if (crop == null || crop.growthStagePrefabs == null || crop.growthStagePrefabs.Length == 0) return;
            state = PlotState.Planted; _plantedAtMinute = Time.time;
            SpawnStage(0); ReportObjective(plantObjectiveId);
        }
        private void Water() { state = PlotState.Watered; ReportObjective(waterObjectiveId); }
        private void Harvest() { state = PlotState.Tilled; if (_currentStage) Destroy(_currentStage); _currentStageIndex = 0; }

        private void SpawnStage(int idx)
        {
            if (_currentStage != null) Destroy(_currentStage);
            _currentStage = Instantiate(crop.growthStagePrefabs[idx], growthAnchor.position, Quaternion.identity, growthAnchor);
            _currentStageIndex = idx;
        }

        private void Update()
        {
            if (state != PlotState.Watered && state != PlotState.Growing) return;
            float elapsed = Time.time - _plantedAtMinute;
            int stage = Mathf.Clamp(Mathf.FloorToInt(elapsed / (crop.growMinutes / Mathf.Max(1, crop.growthStagePrefabs.Length))), 0, crop.growthStagePrefabs.Length - 1);
            if (stage != _currentStageIndex)
            {
                SpawnStage(stage);
                state = stage >= crop.growthStagePrefabs.Length - 1 ? PlotState.ReadyToHarvest : PlotState.Growing;
            }
        }

        private void ReportObjective(string id)
        {
            if (string.IsNullOrEmpty(id)) return;
            if (ServiceLocator.TryGet<IMissionService>(out var ms)) ms.ReportObjectiveProgress(id);
        }
    }

    public interface IInteractable { string Prompt { get; } void Interact(GameObject actor); }
}
