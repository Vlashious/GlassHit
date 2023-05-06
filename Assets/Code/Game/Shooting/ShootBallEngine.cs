using Code.GameObjectLayer.Components;
using Code.Rendering;
using Code.Shared;
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
            var initializer = _entityFactory.BuildEntity<BallDescriptor>(_index++, RigidbodiesInWorld.BuildGroup);
            initializer.Init(new ObjectHolder
            {
                PrefabId = 0,
                Position = Position.FromVector3(worldClickPosition)
            });
        }

        public string name => nameof(ShootBallEngine);
    }
}