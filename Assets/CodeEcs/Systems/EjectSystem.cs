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
        ref var controlData = ref controlledPool.Value.Get(entity);
        if (inputService.GetAltButtonState(controlData.controlScheme)) {
          ref var pilotedData = ref pilotedPool.Value.Get(entity);
          ref var trans = ref transPool.Value.Get(entity);
          ref var physicsBody = ref physicsBodyPool.Value.Get(entity);

          cowboyArchetypeFactory.Create(
            world.Value,
            RandomPositionNear(trans.transform.position, pilotedData.ejectDistance),
            RandomRotation(),
            pilotedData.ejectForce * UnityEngine.Random.insideUnitCircle.normalized
          );
          pilotedPool.Value.Del(entity);
        }
      }
    }
  }
}
