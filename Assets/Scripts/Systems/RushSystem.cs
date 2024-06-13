using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class RushSystem : IEcsInitSystem, IEcsRunSystem 
    {
        private EcsPoolInject<RushCmp> _rushCmpPool;
        private EcsPoolInject<UnitCmp> _unitCmpPool;
        private EcsFilter _rushFilter;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _rushFilter = world.Filter<RushCmp>().Inc<UnitCmp>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _rushFilter)
            {
                ref RushCmp rushCmp = ref _rushCmpPool.Value.Get(entity);
                bool actiovation = rushCmp.IsActiovate;

                if (!actiovation)
                    continue;

                UnitView unitView = rushCmp.UnitView;
                UnitCmp unitCmp = _unitCmpPool.Value.Get(entity);

                Vector3 rushVector = unitCmp.NormalizeCurrentDirection * rushCmp.Range;
                unitView.Rush(rushVector);
            }
        }
    }
}