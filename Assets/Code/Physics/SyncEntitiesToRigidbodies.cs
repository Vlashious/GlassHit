using Code.GameObjectLayer;
using Code.GameObjectLayer.Components;
using Code.Shared;
using Svelto.ECS;
using Unity.Burst;

namespace Code.Physics
{
    [BurstCompile]
    internal sealed class SyncEntitiesToRigidbodies : IStepEngine, IQueryingEntitiesEngine
    {
        private readonly GameObjectManager _gameObjectManager;
        public EntitiesDB entitiesDB { get; set; }
        public string name => nameof(SyncEntitiesToRigidbodies);

        public SyncEntitiesToRigidbodies(GameObjectManager gameObjectManager)
        {
            _gameObjectManager = gameObjectManager;
        }

        public void Ready()
        {
        }

        [BurstCompile]
        public void Step()
        {
            foreach (var ((positions, objects, count), _) in entitiesDB
                         .QueryEntities<Position, ObjectHolder>(RigidbodiesInWorld.Groups))
            {
                for (int i = 0; i < count; i++)
                {
                    var go = _gameObjectManager[objects[i].Index];
                    var position = go.transform.position;

                    ref var pos = ref positions[i];
                    pos.X = position.x;
                    pos.Y = position.y;
                    pos.Z = position.z;
                }
            }
        }
    }
}