using Code.GameObjectLayer.Components;
using Code.Physics;
using Code.Shared;
using Svelto.ECS;

namespace Code.Game.Shooting
{
    public sealed class BallDescriptor : IEntityDescriptor
    {
        public IComponentBuilder[] componentsToBuild => _componentBuilders;

        private static readonly IComponentBuilder[] _componentBuilders;

        static BallDescriptor()
        {
            _componentBuilders = new IComponentBuilder[]
            {
                new ComponentBuilder<Position>(),
                new ComponentBuilder<ObjectHolder>(),
                new ComponentBuilder<Force>()
            };
        }
    }
}