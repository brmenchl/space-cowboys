using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System;
using Zenject;
using Code.Input;
using CodeEcs.Components;
using CodeEcs.Archetypes;

namespace CodeEcs.Systems {
  public class FiringSystem : IEcsRunSystem {
    [Inject] private InputService inputService;
    [Inject] private BulletArchetypeFactory bulletArchetypeFactory;
    private readonly EcsWorldInject world;
    private readonly EcsPoolInject<Controlled> controlledPool;
    private readonly EcsPoolInject<Firing> firingPool;
    private readonly EcsPoolInject<HasGuns> hasGunsPool;
    private readonly EcsPoolInject<Trans> transPool;
    private readonly EcsFilterInject<Inc<Controlled, HasGuns, Trans>, Exc<Firing>> nonFiringControllables;

    public void Run(EcsSystems systems) {
      foreach (var entity in nonFiringControllables.Value) {
        ref var controlData = ref controlledPool.Value.Get(entity);
        if (inputService.GetPrimaryButtonState(controlData.controlScheme)) {
          firingPool.Value.Add(entity);
          ref var firingData = ref firingPool.Value.Get(entity);
          firingData.firedAt = DateTime.UtcNow;
          ref var trans = ref transPool.Value.Get(entity);
          ref var hasGuns = ref hasGunsPool.Value.Get(entity);
          bulletArchetypeFactory.Create(
            world.Value,
            trans.transform.position + (trans.transform.up * hasGuns.muzzleDistance),
            trans.transform.rotation
          );
        }
      }
    }
  }

  public class CleanUpFiringSystem : IEcsRunSystem {
    private readonly EcsPoolInject<Components.Firing> firingPool;
    private readonly EcsFilterInject<Inc<Components.Firing>> firingEntities;

    public void Run(EcsSystems systems) {
      var now = DateTime.UtcNow;
      foreach (var entity in firingEntities.Value) {
        ref var firedComp = ref firingPool.Value.Get(entity);
        if ((now - firedComp.firedAt).TotalSeconds > 0.5f) {
          firingPool.Value.Del(entity);
        }
      }
    }
  }
}
