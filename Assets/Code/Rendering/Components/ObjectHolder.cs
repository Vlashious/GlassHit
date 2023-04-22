using Svelto.DataStructures.Experimental;
using Svelto.ECS;

namespace Code.Rendering.Components
{
    public struct ObjectHolder : IEntityComponent
    {
        public ValueIndex Index;
    }
}