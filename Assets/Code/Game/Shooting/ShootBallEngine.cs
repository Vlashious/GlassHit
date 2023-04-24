using Code.Rendering;
using Code.Rendering.Components;
using Svelto.ECS;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Game.Shooting
{
    public sealed class ShootBallEngine : IStepEngine
    {
        private readonly Camera _camera;
        private readonly Inputs _playerInput;
        private readonly IEntityFactory _entityFactory;

        private uint _index;

        public ShootBallEngine(Camera camera, Inputs playerInput, IEntityFactory entityFactory)
        {
            _camera = camera;
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

            var clickPosition = _playerInput.Main.Position.ReadValue<Vector2>();
            var worldClickPosition = _camera.ScreenToWorldPoint(new Vector3(clickPosition.x, clickPosition.y, 10));
            var initializer = _entityFactory.BuildEntity<BallDescriptor>(_index++, Groups.World);
            initializer.Get<Prefab>().Id = PrefabIds.BallId;
            ref var pos = ref initializer.Get<Position>();
            pos.X = worldClickPosition.x;
            pos.Y = worldClickPosition.y;
            pos.Z = worldClickPosition.z;
        }

        public string name => nameof(ShootBallEngine);
    }
}