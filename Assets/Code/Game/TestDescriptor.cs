using Code.Rendering;
using Code.Rendering.Components;
using Svelto.ECS;

namespace Code.Game
{
    public sealed class TestDescriptor : IEntityDescriptor
    {
        public IComponentBuilder[] componentsToBuild => _componentBuilders;

        private static readonly IComponentBuilder[] _componentBuilders;

        static TestDescriptor()
        {
            _componentBuilders = new IComponentBuilder[]
            {
                new ComponentBuilder<Position>(),
                new ComponentBuilder<Prefab>(),
                new ComponentBuilder<ObjectHolder>()
            };
        }
    }
}