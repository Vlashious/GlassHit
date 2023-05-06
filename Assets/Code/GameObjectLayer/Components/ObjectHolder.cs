using Code.Shared;
using Svelto.DataStructures.Experimental;
using Svelto.ECS;

namespace Code.GameObjectLayer.Components
{
    public struct ObjectHolder : IEntityComponent
    {
        public int PrefabId;
        public ValueIndex Index;
        public Position Position;
    }
}