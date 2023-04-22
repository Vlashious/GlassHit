using Code.Rendering.Components;
using Code.Windows.StartScreen;
using Cysharp.Threading.Tasks;
using Svelto.ECS;

namespace Code.Windows
{
    public static class WindowsContext
    {
        public static UniTask Compose(IEntityFactory entityFactory)
        {
            var startScreenInit = entityFactory.BuildEntity<StartScreenDescriptor>(WindowEGIDs.StartScreen);
            startScreenInit.Get<Prefab>().Id = 0;
            return UniTask.CompletedTask;
        }
    }
}