using Code.Shared;
using Cysharp.Threading.Tasks;
using Svelto.ECS;

namespace Code.GameObjectLayer
{
    public static class GameObjectContext
    {
        public static UniTask Compose(GameObjectManager gameObjectManager,
                                      OrderedList<IStepEngine> updateEngines)
        {
            var syncEntitiesToGo = new SyncEntitiesToGameObjects(gameObjectManager);
            updateEngines.Add(syncEntitiesToGo, EngineUpdateGroup.First);
            return UniTask.CompletedTask;
        }
    }
}