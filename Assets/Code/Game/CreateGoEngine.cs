using Code.Rendering;
using Svelto.ECS;
using UnityEngine;

namespace Code.Game
{
    public sealed class CreateGoEngine : IStepEngine
    {
        private readonly IEntityFactory _entityFactory;

        private uint _index;

        public CreateGoEngine(IEntityFactory entityFactory)
        {
            _entityFactory = entityFactory;
        }

        public void Step()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                var init = _entityFactory.BuildEntity<TestDescriptor>(new EGID(_index++, World.Default));
                init.Get<Prefab>().Id = 0;
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                var init = _entityFactory.BuildEntity<TestDescriptor>(new EGID(_index++, World.Default));
                init.Get<Prefab>().Id = 1;
            }
        }

        public string name => nameof(CreateGoEngine);
    }
}