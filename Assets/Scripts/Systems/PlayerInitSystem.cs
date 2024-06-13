using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class PlayerInitSystem : IEcsInitSystem 
    {
        private EcsWorldInject _worldInject;
        private EcsPoolInject<UnitCmp> _unitCmpPool;
        private EcsPoolInject<PlayerTag> _playerTagPool;
        private EcsPoolInject<RushCmp> _rushCmpPool;
        private EcsCustomInject<SceneService> _sceneData;

        private int _playerEntity;

        public void Init(IEcsSystems systems)
        {
            _playerEntity = _worldInject.Value.NewEntity();
            _playerTagPool.Value.Add(_playerEntity);
            
            ref UnitCmp playerCmp = ref _unitCmpPool.Value.Add(_playerEntity);
            playerCmp.UnitView = _sceneData.Value.PlayerView;

            ref RushCmp rushCmp = ref _rushCmpPool.Value.Add(_playerEntity);
            rushCmp.Range = _sceneData.Value.RangeRush;
            rushCmp.UnitView = _sceneData.Value.PlayerView;
        }
    }
}