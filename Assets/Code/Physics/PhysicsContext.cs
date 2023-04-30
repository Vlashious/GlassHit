using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Svelto.ECS;

namespace Code.Physics
{
    public static class PhysicsContext
    {
        public static async UniTask Compose(IList<IStepEngine> stepEngines)
        {
            var forceEngine = new ForceEngine();
            stepEngines.Add(forceEngine);
            return;
        }
    }
}