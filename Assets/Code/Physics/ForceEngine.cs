using Svelto.ECS;
using UnityEngine;

namespace Code.Physics
{
    public sealed class ForceEngine : IQueryingEntitiesEngine, IStepEngine
    {
        public EntitiesDB entitiesDB { get; set; }
        public string name => nameof(ForceEngine);

        public void Ready()
        {
        }

        public void Step()
        {
            var groupsWithForces = entitiesDB.FindGroups<Force, Position>();
            foreach (var ((forces, positions, count), _) in entitiesDB.QueryEntities<Force, Position>(groupsWithForces))
            {
                for (int i = 0; i < count; i++)
                {
                    ref var force = ref forces[i];
                    ref var position = ref positions[i];
                    var delta = Time.deltaTime;

                    position.X += force.X * delta;
                    position.Y += force.Y * delta;
                    position.Z += force.Z * delta;

                    force.Y -= 9.81f * delta;
                }
            }
        }
    }
}