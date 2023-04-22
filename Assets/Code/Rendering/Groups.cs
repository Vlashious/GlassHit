using Svelto.ECS;

namespace Code.Rendering
{
    public static class Groups
    {
        public static ExclusiveGroup World = new();
        public static ExclusiveGroup Windows = new();
        public static ExclusiveGroup Popups = new();
    }
}