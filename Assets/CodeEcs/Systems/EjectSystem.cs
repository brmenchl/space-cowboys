using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;
using Code.Input;
using CodeEcs.Components;
using CodeEcs.Archetypes;
using static Code.Utilities.TransformHelpers;

namespace CodeEcs.Systems {
  public class EjectSystem : IEcsRunSystem {
    [Inject] private InputService inputService;
    [Inject] private CowboyArchetypeFactory cowboyArchetypeFactory;
    private readonly EcsWorldInject world;
    private readonly EcsPoolInject<Controlled> controlledPool;
    private readonly EcsPoolInject<PhysicsBody> physicsBodyPool;
    private readonly EcsPoolInject<Trans> transPool;
    private readonly EcsPoolInject<Piloted> pilotedPool;
    private readonly EcsFilterInject<Inc<Controlled, Piloted>> pilotedControllables;

    public void Run(EcsSystems systems) {
      foreach (var entity in pilotedControllables.Value) {
        var controlScheme = controlledPool.Value.Get(entity).controlScheme;

        if (inputService.GetAltButtonState(controlScheme)) {
          ref var trans = ref transPool.Value.Get(entity);
          ref var physicsBody = ref physicsBodyPool.Value.Get(entity);
          ref var piloted = ref pilotedPool.Value.Get(entity);

          var cowboyEntity = cowboyArchetypeFactory.Create(
            world.Value,
            RandomPositionNear(trans.transform.position, piloted.ejectDistance),
            RandomRotation(),
            piloted.ejectForce * UnityEngine.Random.insideUnitCircle.normalized
          );
          pilotedPool.Value.Del(entity);

          controlledPool.Value.Del(entity);
          ref var controlledCowboy = ref controlledPool.Value.Add(cowboyEntity);
          controlledCowboy.controlScheme = controlScheme;
        }
      }
    }
  }
}
