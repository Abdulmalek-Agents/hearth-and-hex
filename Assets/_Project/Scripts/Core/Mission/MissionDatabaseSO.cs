using UnityEngine;
using System.Collections.Generic;

namespace InventixGames.Core.Mission
{
    [CreateAssetMenu(menuName = "Inventix/Mission/Mission Database", fileName = "MissionDatabase")]
    public class MissionDatabaseSO : ScriptableObject
    {
        public List<MissionDataSO> missions = new();
        public MissionDataSO Get(string id) => missions.Find(m => m.missionId == id);
    }
}
