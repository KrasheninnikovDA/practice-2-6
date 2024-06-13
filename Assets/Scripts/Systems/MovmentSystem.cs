using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class MovmentSystem : IEcsRunSystem
    {    
        private EcsPoolInject<UnitCmp> _unitCmp;
        private EcsFilterInject<Inc<UnitCmp>> _unitFilter;

        public void Run (IEcsSystems systems) 
        {
            foreach (int entity in _unitFilter.Value)
            {
                ref UnitCmp unitCmp = ref _unitCmp.Value.Get(entity);
                UnitView unitView = unitCmp.UnitView;
                Vector3 velocity = unitCmp.Velocity;

                if (velocity == Vector3.zero)
                    continue;

                unitView.SetDirection(velocity);
                unitView.Move(velocity * Time.deltaTime);
                velocity.Normalize();
                unitCmp.NormalizeCurrentDirection = velocity;


            }
        }
    }
}