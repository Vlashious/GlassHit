using Svelto.ECS;
using UnityEngine;

namespace Code.Rendering
{
    public sealed class ObjectCreateEngine : IQueryingEntitiesEngine, IReactOnAddEx<Prefab>
    {
        private readonly GameObjectManager _manager;
        private readonly IPrefabProvider _prefabProvider;

        private WorldHierarchy _hierarchy;

        public EntitiesDB entitiesDB { get; set; }

        public ObjectCreateEngine(GameObjectManager manager,
                                  IPrefabProvider prefabProvider)
        {
            _manager = manager;
            _prefabProvider = prefabProvider;
        }

        public void Ready()
        {
            _hierarchy = Object.Instantiate(_prefabProvider.Hierarchy);
        }

        public void Add((uint start, uint end) rangeOfEntities, in EntityCollection<Prefab> entities,
                        ExclusiveGroupStruct groupID)
        {
            var (buffer, count) = entities;
            for (var i = rangeOfEntities.start; i < rangeOfEntities.end; i++)
            {
                var prefab = buffer[i];
                var go = Object.Instantiate(_prefabProvider.Get(prefab.Id), _hierarchy.Root);
                ref var objectHolder = ref entitiesDB.QueryEntity<ObjectHolder>(i, groupID);
                objectHolder.Index = _manager.Add(go);
            }
        }
    }
}