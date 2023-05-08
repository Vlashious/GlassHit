using Code.GameObjectLayer;
using Code.GameObjectLayer.Components;
using Code.Shared;
using Cysharp.Threading.Tasks;
using Svelto.ECS;
using UnityEngine;

namespace Code.Physics
{
    public sealed class ApplyForceEngine : IQueryingEntitiesEngine, IReactOnAddEx<Force>
    {
        private readonly GameObjectManager _objectManager;

        public ApplyForceEngine(GameObjectManager objectManager)
        {
            _objectManager = objectManager;
        }

        public void Ready()
        {
        }

        public EntitiesDB entitiesDB { get; set; }
        public void Add((uint start, uint end) rangeOfEntities, in EntityCollection<Force> entities, ExclusiveGroupStruct groupID)
        {
            var (forces, count) = entities;
            for (var i = rangeOfEntities.start; i < rangeOfEntities.end; i++)
            {
                var force = forces[i];
                var objectHolder = entitiesDB.QueryEntity<ObjectHolder>(i, groupID);
                var go = _objectManager[objectHolder.Index];
                if (go.TryGetComponent(out Rigidbody rb))
                {
                    rb.AddForce(force.X, force.Y, force.Z, ForceMode.Impulse);
                }
            }
        }
    }
}