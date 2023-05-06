using System;
using Code.Shared;
using Svelto.ECS;
using UnityEngine;

namespace Code.Rendering.Engines
{
    public sealed class EntityPositionDebug : MonoBehaviour, IQueryingEntitiesEngine
    {
        public EntitiesDB entitiesDB { get; set; }

        private void OnDrawGizmos()
        {
            foreach (var ((positions, count), _) in entitiesDB.QueryEntities<Position>())
            {
                for (int i = 0; i < count; i++)
                {
                    var pos = positions[i];
                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere(new Vector3(pos.X, pos.Y, pos.Z), 0.1f);
                }
            }
        }

        public void Ready()
        {
        }
    }
}