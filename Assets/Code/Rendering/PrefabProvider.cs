using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Rendering
{
    [CreateAssetMenu(fileName = "PrefabProvider", menuName = "SO/Prefab provider")]
    public sealed class PrefabProvider : ScriptableObject, IPrefabProvider
    {
        [SerializeField] private List<PrefabEntry> _entries = new();
        [SerializeField] private WorldHierarchy _hierarchy;

        public IReadOnlyDictionary<int, GameObject> Prefabs { get; private set; }

        private void OnEnable()
        {
            Prefabs = _entries.ToDictionary(x => x.Id, x => x.Prefab);
        }

        public GameObject Get(int id)
        {
            return Prefabs[id];
        }

        public WorldHierarchy Hierarchy => _hierarchy;
    }

    [Serializable]
    public sealed class PrefabEntry
    {
        public int Id;
        public GameObject Prefab;
    }
}