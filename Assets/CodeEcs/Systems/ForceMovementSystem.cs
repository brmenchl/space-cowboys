using Code.Input;
using CodeEcs.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace CodeEcs.Systems {
  public class ForceMovementSystem : IEcsRunSystem {
    [Inject] private InputService inputService;
    private readonly EcsPoolInject<PhysicsBody> physicsBodyPool;
    private readonly EcsPoolInject<Trans> transPool;
    private readonly EcsPoolInject<Controlled> controlledPool;
    private readonly EcsPoolInject<PhysicsMovement> physicsMovementPool;
    private readonly EcsFilterInject<Inc<PhysicsBody, Controlled, PhysicsMovement, Trans>> toMove;

    public void Run(EcsSystems systems) {
      foreach (var entity in toMove.Value) {
        ref var physicsBody = ref physicsBodyPool.Value.Get(entity);
        ref var physicsMovement = ref physicsMovementPool.Value.Get(entity);
        ref var controlData = ref controlledPool.Value.Get(entity);
        ref var trans = ref transPool.Value.Get(entity);
        var movement = inputService.GetMovementState(controlData.controlScheme);

        physicsBody.rigidBody.AddForce(trans.transform.up * physicsMovement.thrustForce * movement.y);
        physicsBody.rigidBody.AddTorque(physicsMovement.turnForce * -movement.x);
      }
    }
  }
}