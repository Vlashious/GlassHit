using Code.GameObjectLayer;
using Code.Shared;
using Cysharp.Threading.Tasks;
using Svelto.ECS;

namespace Code.Physics
{
    public static class PhysicsContext
    {
        public static UniTask Compose(GameObjectManager gameObjectManager,
                                      OrderedList<IEngine> engines)
        {
            var applyForceEngine = new ApplyForceEngine(gameObjectManager);
            engines.Add(applyForceEngine, EngineUpdateGroup.Second);
            return UniTask.CompletedTask;
        }
    }
}