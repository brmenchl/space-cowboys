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
    private readonly EcsFilterInject<Inc<Controlled, HasGuns, Trans>, Exc<Firing>> nonFiringControllables;

    public void Run(EcsSystems systems) {
      foreach (var entity in nonFiringControllables.Value) {
        ref var controlData = ref world.Value.GetPool<Controlled>().Get(entity);
        if (inputService.GetPrimaryButtonState(controlData.controlScheme)) {
          ref var firing = ref world.Value.GetPool<Firing>().Add(entity);
          firing.firedAt = DateTime.UtcNow;
          ref var trans = ref world.Value.GetPool<Trans>().Get(entity);
          ref var hasGuns = ref world.Value.GetPool<HasGuns>().Get(entity);
          bulletArchetypeFactory.Create(
            world.Value,
            trans.transform.position + (trans.transform.up * hasGuns.muzzleDistance),
            trans.transform.rotation
          );
          if (world.Value.GetPool<CanKickback>().Has(entity)) {
            world.Value.GetPool<Kickback>().Add(entity);
          }
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
