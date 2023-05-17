using Code.GameObjectLayer;
using Code.Shared;
using Cysharp.Threading.Tasks;
using Svelto.ECS;

namespace Code.Physics
{
    public static class PhysicsContext
    {
        public static UniTask Compose(GameObjectManager gameObjectManager,
                                      OrderedList<IEngine> readyEngines)
        {
            var listenToCollisionsEngine = new ListenToCollisionEngine(gameObjectManager);
            var applyForceEngine = new ApplyForceEngine(gameObjectManager);
            readyEngines.Add(listenToCollisionsEngine, EngineUpdateGroup.First);
            readyEngines.Add(applyForceEngine, EngineUpdateGroup.Second);
            return UniTask.CompletedTask;
        }
    }
}