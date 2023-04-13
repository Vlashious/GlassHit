using UnityEngine;

namespace Code.Rendering
{
    public interface IPrefabProvider
    {
        GameObject Get(int id);
        WorldHierarchy Hierarchy { get; }
    }
}