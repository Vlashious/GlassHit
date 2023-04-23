using Code.Rendering.Components;
using Svelto.ECS;

namespace Code.Windows.StartScreen
{
    public sealed class StartScreenDescriptor : IEntityDescriptor
    {
        public IComponentBuilder[] componentsToBuild => _componentBuilders;

        private static readonly IComponentBuilder[] _componentBuilders;

        static StartScreenDescriptor()
        {
            _componentBuilders = new IComponentBuilder[]
            {
                new ComponentBuilder<Prefab>(),
                new ComponentBuilder<ObjectHolder>(),
                new ComponentBuilder<StartScreen>()
            };
        }
    }
}