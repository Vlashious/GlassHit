using System.Collections.Generic;
using Code.Rendering;
using Code.Windows;
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

        private readonly SimpleEntitiesSubmissionScheduler _scheduler = new();
        private readonly IList<IStepEngine> _updateEngines = new List<IStepEngine>();
        private readonly IList<IEngine> _engines = new List<IEngine>();

        private async void Awake()
        {
            EnginesRoot root = new(_scheduler);
            var entityFunctions = root.GenerateEntityFunctions();
            var entityFactory = root.GenerateEntityFactory();
            var goManager = new GameObjectManager();
            await RenderContext.Compose(goManager,
                                        _prefabProvider,
                                        _windowPrefabProvider,
                                        _hierarchyProvider,
                                        _updateEngines,
                                        _engines);
            await GameContext.Compose(_updateEngines, entityFactory);
            await WindowsContext.Compose(goManager, entityFactory, _engines);

            foreach (var engine in _engines)
            {
                root.AddEngine(engine);
            }

            foreach (var engine in _updateEngines)
            {
                root.AddEngine(engine);
            }
        }

        private void Update()
        {
            for (int i = 0; i < _updateEngines.Count; i++)
            {
                _updateEngines[i].Step();
            }

            _scheduler.SubmitEntities();
        }
    }
}