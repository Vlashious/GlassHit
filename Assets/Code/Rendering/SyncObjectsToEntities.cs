using Svelto.ECS;
using UnityEngine;

namespace Code.Rendering
{
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

        public void Step()
        {
            var kek = entitiesDB.FindGroups<Position, ObjectHolder>();

            foreach (var ((pos, obj, count), _) in entitiesDB.QueryEntities<Position, ObjectHolder>(kek))
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