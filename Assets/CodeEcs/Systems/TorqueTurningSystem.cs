using Code.Input;
using CodeEcs.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace CodeEcs.Systems {
  public class TorqueTurningSystem : IEcsRunSystem {
    [Inject] private InputService inputService;
    private readonly EcsPoolInject<PhysicsBody> physicsBodyPool;
    private readonly EcsPoolInject<Trans> transPool;
    private readonly EcsPoolInject<Controlled> controlledPool;
    private readonly EcsPoolInject<TorqueTurning> torqueTurningPool;
    private readonly EcsFilterInject<Inc<PhysicsBody, Controlled, TorqueTurning, Trans>> toMove;

    public void Run(EcsSystems systems) {
      foreach (var entity in toMove.Value) {
        ref var physicsBody = ref physicsBodyPool.Value.Get(entity);
        ref var physicsMovement = ref torqueTurningPool.Value.Get(entity);
        ref var controlData = ref controlledPool.Value.Get(entity);
        ref var trans = ref transPool.Value.Get(entity);
        var movement = inputService.GetMovementState(controlData.controlScheme);

        physicsBody.rigidBody.AddTorque(physicsMovement.torque * -movement.x);
      }
    }
  }
}