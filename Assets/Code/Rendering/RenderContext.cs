using System.Collections.Generic;
using Code.GameObjectLayer;
using Code.Physics;
using Code.Rendering.Engines;
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
                                      IList<IStepEngine> updateEngines,
                                      IList<IEngine> engines)
        {
            var goCreator = new ObjectCreateEngine(gameObjectManager,
                                                   prefabProvider,
                                                   windowPrefabProvider,
                                                   hierarchyProvider);
            var syncGoToEntity = new SyncObjectsToEntities(gameObjectManager);

            updateEngines.Add(syncGoToEntity);
            engines.Add(goCreator);

            return UniTask.CompletedTask;
        }
    }
}