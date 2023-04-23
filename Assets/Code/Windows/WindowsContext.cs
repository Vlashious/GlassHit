using System.Collections.Generic;
using Code.Rendering;
using Code.Rendering.Components;
using Code.Windows.StartScreen;
using Cysharp.Threading.Tasks;
using Svelto.ECS;

namespace Code.Windows
{
    public static class WindowsContext
    {
        public static UniTask Compose(GameObjectManager gameObjectManager,
                                      IEntityFactory entityFactory,
                                      IList<IEngine> engines)
        {
            var startScreenInit = entityFactory.BuildEntity<StartScreenDescriptor>(WindowEGIDs.StartScreen);
            startScreenInit.Get<Prefab>().Id = 0;
            var startScreenEngine = new StartScreenEngine(gameObjectManager);
            engines.Add(startScreenEngine);
            return UniTask.CompletedTask;
        }
    }
}