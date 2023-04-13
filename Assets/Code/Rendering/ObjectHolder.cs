using Svelto.DataStructures.Experimental;
using Svelto.ECS;

namespace Code.Rendering
{
    public struct ObjectHolder : IEntityComponent
    {
        public ValueIndex Index;
    }
}