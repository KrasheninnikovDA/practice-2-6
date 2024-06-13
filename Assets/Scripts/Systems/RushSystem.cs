using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class RushSystem : IEcsRunSystem 
    {
        private EcsPoolInject<RushCmp> _rushCmpPool;
        private EcsPoolInject<UnitCmp> _unitCmpPool;
        private EcsFilterInject<Inc<RushCmp>> _rushFilter;

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _rushFilter.Value)
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