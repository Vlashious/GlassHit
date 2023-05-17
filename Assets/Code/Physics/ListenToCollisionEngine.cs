using Code.GameObjectLayer;
using Code.GameObjectLayer.Components;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using Svelto.ECS;
using UnityEngine;

namespace Code.Physics
{
    public sealed class ListenToCollisionEngine : IReactOnAddEx<ObjectHolder>, IQueryingEntitiesEngine
    {
        private readonly GameObjectManager _gameObjectManager;
        public EntitiesDB entitiesDB { get; set; }

        public ListenToCollisionEngine(GameObjectManager gameObjectManager)
        {
            _gameObjectManager = gameObjectManager;
        }


        public void Ready()
        {
        }

        public void Add((uint start, uint end) rangeOfEntities,
                        in EntityCollection<ObjectHolder> entities,
                        ExclusiveGroupStruct groupID)
        {
            var (buffer, count) = entities;
            for (var i = rangeOfEntities.start; i < rangeOfEntities.end; i++)
            {
                var go = _gameObjectManager[buffer[i].Index];
                if (go.TryGetComponent(out Rigidbody rb))
                {
                    ListenToCollisions(rb).Forget();
                }
            }
        }

        private async UniTaskVoid ListenToCollisions(Rigidbody rb)
        {
            var cancellation = rb.GetCancellationTokenOnDestroy();
            while (true)
            {
                var result = await rb.GetAsyncCollisionEnterTrigger().OnCollisionEnterAsync(cancellation);
                Debug.Log($"{rb.name} HIT {result.collider.name}");
                
                await UniTask.Yield(cancellation);
            }
        }
    }
}