using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Svelto.ECS;

namespace Code.Game
{
    public static class GameContext
    {
        public static UniTask Compose(IList<IStepEngine> updateEngines,
                                      IEntityFactory entityFactory)
        {
            var createGoEngine = new CreateGoEngine(entityFactory);

            updateEngines.Add(createGoEngine);

            return UniTask.CompletedTask;
        }
    }
}