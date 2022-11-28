using System;
using Code.Input;
using CodeEcs.Archetypes;
using CodeEcs.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace CodeEcs.Systems {
  public class LassoingSystem : IEcsRunSystem {
    [Inject] private InputService inputService;
    [Inject] private LassoArchetypeFactory lassoArchetypeFactory;
    private readonly EcsWorldInject world;
    private readonly EcsFilterInject<Inc<Controlled, HasLasso, Trans>, Exc<Lassoing>> readyToLassoControllables;

    public void Run(EcsSystems systems) {
      foreach (var entity in readyToLassoControllables.Value) {
        ref var controlled = ref world.Value.GetPool<Controlled>().Get(entity);
        if (inputService.GetAltButtonState(controlled.controlScheme)) {
          ref var lassoing = ref world.Value.GetPool<Lassoing>().Add(entity);
          ref var trans = ref world.Value.GetPool<Trans>().Get(entity);
          var lassoEntity = lassoArchetypeFactory.Create(world.Value, trans.transform);
          lassoing.lasso = world.Value.PackEntity(lassoEntity);
        }
      }
    }
  }

  public class CleanUpLassoingSystem : IEcsRunSystem {
    private readonly EcsWorldInject world;
    private readonly EcsPoolInject<Lassoing> lassoingPool;
    private readonly EcsFilterInject<Inc<Lassoing>> lassoingEntities;

    public void Run(EcsSystems systems) {
      var now = DateTime.UtcNow;
      foreach (var entity in lassoingEntities.Value) {
        ref var lassoing = ref lassoingPool.Value.Get(entity);
        if (!lassoing.lasso.Unpack(world.Value, out int _)) {
          lassoingPool.Value.Del(entity);
        }
      }
    }
  }
}