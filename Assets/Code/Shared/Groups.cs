using Svelto.ECS;

namespace Code.Shared
{
    public sealed class World : GroupTag<World>
    {
    }

    public sealed class Rigidbodies : GroupTag<Rigidbodies>
    {
    }

    public sealed class PhysicsWorld : GroupCompound<World, Rigidbodies>
    {
    }
}