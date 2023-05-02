using Code.GameObjectLayer;
using Code.GameObjectLayer.Components;
using Code.Physics;
using Svelto.ECS;
using Unity.Burst;
using UnityEngine;

namespace Code.Rendering.Engines
{
    [BurstCompile]
    public sealed class SyncObjectsToEntities : IQueryingEntitiesEngine, IStepEngine
    {
        private readonly GameObjectManager _manager;
        public EntitiesDB entitiesDB { get; set; }

        public string name => nameof(SyncObjectsToEntities);

        public SyncObjectsToEntities(GameObjectManager manager)
        {
            _manager = manager;
        }

        public void Ready()
        {
        }

        [BurstCompile]
        public void Step()
        {
            var groupsWithPositions = entitiesDB.FindGroups<Position, ObjectHolder>();

            foreach (var ((pos, obj, count), _) in
                     entitiesDB.QueryEntities<Position, ObjectHolder>(groupsWithPositions))
            {
                for (int i = 0; i < count; i++)
                {
                    var go = _manager[obj[i].Index];
                    var entityPosition = pos[i];
                    go.transform.position = new Vector3(entityPosition.X, entityPosition.Y, entityPosition.Z);
                }
            }
        }
    }
}