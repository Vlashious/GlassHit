using Code.Rendering.Components;
using Svelto.ECS;
using UnityEngine;

namespace Code.Rendering.Engines
{
    public sealed class ObjectCreateEngine : IQueryingEntitiesEngine, IReactOnAddEx<Prefab>
    {
        private readonly GameObjectManager _manager;
        private readonly IPrefabProvider _prefabProvider;
        private readonly IPrefabProvider _windowPrefabProvider;
        private readonly IHierarchyProvider _hierarchyProvider;

        private WorldHierarchy _hierarchy;

        public EntitiesDB entitiesDB { get; set; }

        public ObjectCreateEngine(GameObjectManager manager,
                                  IPrefabProvider prefabProvider,
                                  IPrefabProvider windowPrefabProvider,
                                  IHierarchyProvider hierarchyProvider)
        {
            _manager = manager;
            _prefabProvider = prefabProvider;
            _windowPrefabProvider = windowPrefabProvider;
            _hierarchyProvider = hierarchyProvider;
        }

        public void Ready()
        {
            _hierarchy = Object.Instantiate(_hierarchyProvider.Hierarchy);
        }

        public void Add((uint start, uint end) rangeOfEntities, in EntityCollection<Prefab> entities,
                        ExclusiveGroupStruct groupID)
        {
            var (buffer, count) = entities;
            for (var i = rangeOfEntities.start; i < rangeOfEntities.end; i++)
            {
                var prefab = buffer[i];
                GetCorrectBundle(groupID, out var provider, out var root);
                var go = Object.Instantiate(provider.Get(prefab.Id), root);
                ref var objectHolder = ref entitiesDB.QueryEntity<ObjectHolder>(i, groupID);
                objectHolder.Index = _manager.Add(go);
            }
        }

        private void GetCorrectBundle(ExclusiveGroupStruct group, out IPrefabProvider prefabProvider, out Transform root)
        {
            prefabProvider = default;
            root = default;
            if (group == Groups.World)
            {
                prefabProvider = _prefabProvider;
                root = _hierarchy.Root;
                return;
            }

            if (group == Groups.Windows)
            {
                prefabProvider = _windowPrefabProvider;
                root = _hierarchy.WindowRoot;
                return;
            }

            if (group == Groups.Popups)
            {
                root = _hierarchy.PopupRoot;
                return;
            }
        }
    }
}