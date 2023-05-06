using Svelto.ECS;
using UnityEngine;

namespace Code.Shared
{
    public struct Position : IEntityComponent
    {
        public float X;
        public float Y;
        public float Z;

        public Vector3 ToVector3() => new Vector3(X, Y, Z);

        public static Position FromVector3(Vector3 vector3)
        {
            return new Position
            {
                X = vector3.x,
                Y = vector3.y,
                Z = vector3.z
            };
        }
    }
}