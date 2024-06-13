using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class PlayerNewInputSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private EcsPoolInject<UnitCmp> _unitCmpPool;
        private EcsPoolInject<RushCmp> _rushCmpPool;
        private EcsFilterInject<Inc<PlayerTag>> _playersFiltr;
        private InputControls _playerInput;
        private bool isActiovateRush = false;

        public void Init (IEcsSystems systems) 
        {
            _playerInput = new InputControls();
            _playerInput.Enable();
            _playerInput.Player.Rush.performed += context => isActiovateRush = true;
            _playerInput.Player.Rush.canceled += context => isActiovateRush = false;
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _playersFiltr.Value)
            {
                ref UnitCmp unitCmp = ref _unitCmpPool.Value.Get(entity);
                unitCmp.Velocity = _playerInput.Player.Move.ReadValue<Vector2>();

                ref RushCmp rushCmp = ref _rushCmpPool.Value.Get(entity);
                rushCmp.IsActiovate = isActiovateRush;
            }
        }

        public void Destroy(IEcsSystems systems)
        {
            _playerInput.Disable();
        }
    }
}