using UnityEngine;
using System.Collections.Generic;

namespace InventixGames.Core
{
    public interface ICheckpointService
    {
        void Register(Vector3 position, string id);
        bool TryGetLatest(out Vector3 position, out string id);
        void Clear();
    }

    public class CheckpointService : MonoBehaviour, ICheckpointService
    {
        private readonly List<(Vector3 pos, string id)> _checkpoints = new();

        public void Register(Vector3 position, string id)
        {
            _checkpoints.Add((position, id));
            if (ServiceLocator.TryGet<ISaveService>(out var save))
            {
                save.Data.kv[$"chk_{id}_x"] = position.x.ToString();
                save.Data.kv[$"chk_{id}_y"] = position.y.ToString();
                save.Data.kv[$"chk_{id}_z"] = position.z.ToString();
                save.Save();
            }
        }
        public bool TryGetLatest(out Vector3 position, out string id)
        {
            if (_checkpoints.Count == 0) { position = Vector3.zero; id = null; return false; }
            var last = _checkpoints[_checkpoints.Count - 1];
            position = last.pos; id = last.id; return true;
        }
        public void Clear() => _checkpoints.Clear();
    }
}
