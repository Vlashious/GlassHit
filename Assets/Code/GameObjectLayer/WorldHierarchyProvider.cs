using UnityEngine;

namespace Code.GameObjectLayer
{
    [CreateAssetMenu(fileName = "WorldHierarchyProvider", menuName = "SO/World Hierarchy provider")]
    public sealed class WorldHierarchyProvider : ScriptableObject, IHierarchyProvider
    {
        [SerializeField] private WorldHierarchy _hierarchy;

        public WorldHierarchy Hierarchy => _hierarchy;
    }
}