using System.Collections.Generic;
using Code.GameObjectLayer;
using Cysharp.Threading.Tasks;
using Svelto.ECS;

namespace Code.Physics
{
    public static class PhysicsContext
    {
        public static async UniTask Compose(GameObjectManager gameObjectManager,
                                            IList<IStepEngine> updateEngines)
        {
            var syncEntitiesToRbs = new SyncEntitiesToRigidbodies(gameObjectManager);
            updateEngines.Add(syncEntitiesToRbs);
            return;
        }
    }
}