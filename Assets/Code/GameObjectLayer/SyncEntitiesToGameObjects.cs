using Code.GameObjectLayer.Components;
using Code.Shared;
using Svelto.ECS;

namespace Code.GameObjectLayer
{
    internal sealed class SyncEntitiesToGameObjects : IStepEngine, IQueryingEntitiesEngine
    {
        private readonly GameObjectManager _gameObjectManager;
        public EntitiesDB entitiesDB { get; set; }
        public string name => nameof(SyncEntitiesToGameObjects);

        public SyncEntitiesToGameObjects(GameObjectManager gameObjectManager)
        {
            _gameObjectManager = gameObjectManager;
        }

        public void Ready()
        {
        }

        public void Step()
        {
            var allObjectsAndPositions = entitiesDB.FindGroups<Position, ObjectHolder>();
            foreach (var ((positions, objects, count), _) in entitiesDB
                         .QueryEntities<Position, ObjectHolder>(allObjectsAndPositions))
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