using Code.Rendering;
using Code.Rendering.Components;
using Svelto.ECS;
using UnityEngine.EventSystems;

namespace Code.Game.Shooting
{
    public sealed class ShootBallEngine : IStepEngine
    {
        private readonly Inputs _playerInput;
        private readonly IEntityFactory _entityFactory;

        private uint _index;

        public ShootBallEngine(Inputs playerInput, IEntityFactory entityFactory)
        {
            _playerInput = playerInput;
            _entityFactory = entityFactory;
        }

        public void Step()
        {
            if (!_playerInput.Main.Shoot.WasPerformedThisFrame())
            {
                return;
            }

            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            var initializer = _entityFactory.BuildEntity<BallDescriptor>(_index++, Groups.World);
            initializer.Get<Prefab>().Id = PrefabIds.BallId;
        }

        public string name => nameof(ShootBallEngine);
    }
}