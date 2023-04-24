using System.Collections.Generic;
using Code.Game.Shooting;
using Cysharp.Threading.Tasks;
using Svelto.ECS;

namespace Code.Game
{
    public static class GameContext
    {
        public static UniTask Compose(IList<IStepEngine> updateEngines,
                                      IEntityFactory entityFactory,
                                      Inputs inputs)
        {
            var shootBallEngine = new ShootBallEngine(inputs, entityFactory);

            updateEngines.Add(shootBallEngine);

            return UniTask.CompletedTask;
        }
    }
}