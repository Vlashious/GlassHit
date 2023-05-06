using System.Collections.Generic;
using Code.Game.Shooting;
using Code.Shared;
using Cysharp.Threading.Tasks;
using Svelto.ECS;
using UnityEngine;

namespace Code.Game
{
    public static class GameContext
    {
        public static UniTask Compose(OrderedList<IStepEngine> updateEngines,
                                      IEntityFactory entityFactory,
                                      Inputs inputs,
                                      Camera camera)
        {
            var shootBallEngine = new ShootBallEngine(camera, inputs, entityFactory);

            updateEngines.Add(shootBallEngine, EngineUpdateGroup.Second);

            return UniTask.CompletedTask;
        }
    }
}