using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Svelto.ECS;

namespace Code.Rendering
{
    public static class RenderContext
    {
        public static UniTask Compose(IPrefabProvider prefabProvider,
                                      IList<IStepEngine> updateEngines,
                                      IList<IEngine> engines)
        {
            var goManager = new GameObjectManager();
            var goCreator = new ObjectCreateEngine(goManager, prefabProvider);
            var syncGoToEntity = new SyncObjectsToEntities(goManager);

            updateEngines.Add(syncGoToEntity);
            engines.Add(goCreator);

            return UniTask.CompletedTask;
        }
    }
}