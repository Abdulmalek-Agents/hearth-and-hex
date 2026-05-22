using System.Collections.Generic;
using UnityEngine;

namespace InventixGames.Core
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int preWarmCount = 16;
        private readonly Stack<GameObject> _pool = new();

        private void Awake() { for (int i = 0; i < preWarmCount; i++) Return(Create()); }
        private GameObject Create() { var go = Instantiate(prefab, transform); go.SetActive(false); return go; }

        public GameObject Get(Vector3 pos, Quaternion rot)
        {
            var go = _pool.Count > 0 ? _pool.Pop() : Create();
            go.transform.SetPositionAndRotation(pos, rot);
            go.SetActive(true);
            return go;
        }
        public void Return(GameObject go) { go.SetActive(false); go.transform.SetParent(transform); _pool.Push(go); }
    }
}
