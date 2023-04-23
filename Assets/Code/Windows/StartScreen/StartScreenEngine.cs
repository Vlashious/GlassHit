using System.Threading;
using Code.Rendering;
using Code.Rendering.Components;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Svelto.ECS;
using UnityEngine;

namespace Code.Windows.StartScreen
{
    public sealed class StartScreenEngine : IQueryingEntitiesEngine, IReactOnAddEx<StartScreen>
    {
        private readonly GameObjectManager _gameObjectManager;

        public StartScreenEngine(GameObjectManager gameObjectManager)
        {
            _gameObjectManager = gameObjectManager;
        }

        public EntitiesDB entitiesDB { get; set; }

        public void Ready()
        {
        }

        public void Add((uint start, uint end) rangeOfEntities,
                        in EntityCollection<StartScreen> entities,
                        ExclusiveGroupStruct groupID)
        {
            var objectHolder = entitiesDB.QueryUniqueEntity<ObjectHolder>(Groups.Windows);
            _gameObjectManager[objectHolder.Index].TryGetComponent(out StartScreenView view);

            view.ButtonClicks.ForEachAsync(_ => Debug.Log($"Button click!"));
        }
    }
}