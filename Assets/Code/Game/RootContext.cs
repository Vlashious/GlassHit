using System.Collections.Generic;
using Code.GameObjectLayer;
using Code.Physics;
using Code.Rendering;
using Code.Rendering.Engines;
using Code.Shared;
using Svelto.ECS;
using Svelto.ECS.Schedulers;
using UnityEngine;

namespace Code.Game
{
    public sealed class RootContext : MonoBehaviour
    {
        [SerializeField] private PrefabProvider _prefabProvider;
        [SerializeField] private PrefabProvider _windowPrefabProvider;
        [SerializeField] private WorldHierarchyProvider _hierarchyProvider;
        [SerializeField] private Camera _camera;
        [SerializeField] private EntityPositionDebug _positionDebug;

        private readonly SimpleEntitiesSubmissionScheduler _scheduler = new();
        private readonly OrderedList<IStepEngine> _updateEngines = new();
        private readonly OrderedList<IEngine> _engines = new();

        private async void Awake()
        {
            var inputs = new Inputs();
            EnginesRoot root = new(_scheduler);
            var consumerFactory = root.GenerateConsumerFactory();
            var entityFunctions = root.GenerateEntityFunctions();
            var entityFactory = root.GenerateEntityFactory();
            var goManager = new GameObjectManager();
            await GameObjectContext.Compose(goManager, _updateEngines);
            await GameContext.Compose(_updateEngines, entityFactory, inputs, _camera);
            await RenderContext.Compose(goManager,
                                        _prefabProvider,
                                        _windowPrefabProvider,
                                        _hierarchyProvider,
                                        _updateEngines,
                                        _engines);
            await PhysicsContext.Compose(goManager, _engines);

            _engines.Foreach(x => root.AddEngine(x));
            _updateEngines.Foreach(x => root.AddEngine(x));

            _updateEngines.Order();

            root.AddEngine(_positionDebug);

            inputs.Enable();
        }

        private void Update()
        {
            _updateEngines.Foreach(x => x.Step());
            _scheduler.SubmitEntities();
        }
    }
}