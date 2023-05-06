using Code.GameObjectLayer;
using Code.Rendering.Engines;
using Code.Shared;
using Cysharp.Threading.Tasks;
using Svelto.ECS;

namespace Code.Rendering
{
    public static class RenderContext
    {
        public static UniTask Compose(GameObjectManager gameObjectManager,
                                      IPrefabProvider prefabProvider,
                                      IPrefabProvider windowPrefabProvider,
                                      IHierarchyProvider hierarchyProvider,
                                      OrderedList<IStepEngine> updateEngines,
                                      OrderedList<IEngine> engines)
        {
            var goCreator = new ObjectCreateEngine(gameObjectManager,
                                                   prefabProvider,
                                                   windowPrefabProvider,
                                                   hierarchyProvider);
            var syncGoToEntity = new SyncObjectsToEntities(gameObjectManager);

            updateEngines.Add(syncGoToEntity, EngineUpdateGroup.Third);
            engines.Add(goCreator, EngineUpdateGroup.First);

            return UniTask.CompletedTask;
        }
    }
}