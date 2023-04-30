using Code.Physics;
using Code.Rendering;
using Code.Rendering.Components;
using Svelto.ECS;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Game.Shooting
{
    public sealed class ShootBallEngine : IStepEngine, IQueryingEntitiesEngine
    {
        private readonly Camera _camera;
        private readonly Inputs _playerInput;
        private readonly IEntityFactory _entityFactory;

        public EntitiesDB entitiesDB { get; set; }

        private uint _index;

        public ShootBallEngine(Camera camera, Inputs playerInput, IEntityFactory entityFactory)
        {
            _camera = camera;
            _playerInput = playerInput;
            _entityFactory = entityFactory;
        }

        public void Ready()
        {
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
            initializer.Init(new Prefab
            {
                Id = 0
            });
            initializer.Init(new Position
            {
                X = worldClickPosition.x,
                Y = worldClickPosition.y,
                Z = worldClickPosition.z
            });
            var forceVector = (worldClickPosition - _camera.transform.position).normalized * 10;
            initializer.Init(new Force()
            {
                X = forceVector.x,
                Y = forceVector.y,
                Z = forceVector.z
            });
        }

        public string name => nameof(ShootBallEngine);
    }
}