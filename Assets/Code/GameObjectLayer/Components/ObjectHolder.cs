using Svelto.DataStructures.Experimental;
using Svelto.ECS;

namespace Code.GameObjectLayer.Components
{
    public struct ObjectHolder : IEntityComponent
    {
        public ValueIndex Index;
    }
}