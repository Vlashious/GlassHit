using System.Collections.Generic;
using Code.Game.Shooting;
using Cysharp.Threading.Tasks;
using Svelto.ECS;
using UnityEngine;

namespace Code.Game
{
    public static class GameContext
    {
        public static UniTask Compose(IList<IStepEngine> updateEngines,
                                      IEntityFactory entityFactory,
                                      Inputs inputs,
                                      Camera camera)
        {
            var shootBallEngine = new ShootBallEngine(camera, inputs, entityFactory);

            updateEngines.Add(shootBallEngine);

            return UniTask.CompletedTask;
        }
    }
}