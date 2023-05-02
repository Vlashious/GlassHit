using UnityEngine;

namespace Code.GameObjectLayer
{
    public interface IPrefabProvider
    {
        GameObject Get(int id);
    }
}