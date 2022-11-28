using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using CodeEcs.Components;

namespace CodeEcs.Systems {
  public class KickbackSystem : IEcsRunSystem {
    private readonly EcsWorldInject world;
    private readonly EcsPoolInject<CanKickback> canKickbackPool;
    private readonly EcsPoolInject<Kickback> kickbackPool;
    private readonly EcsPoolInject<PhysicsBody> physicsBodyPool;
    private readonly EcsFilterInject<Inc<CanKickback, Kickback, PhysicsBody>> shouldKickback;

    public void Run(EcsSystems systems) {
      foreach (var entity in shouldKickback.Value) {
        ref var kickbackData = ref canKickbackPool.Value.Get(entity);
        ref var physicsBody = ref physicsBodyPool.Value.Get(entity);
        physicsBody.rigidBody.AddForce(physicsBody.rigidBody.transform.up * -1 * kickbackData.force);
        kickbackPool.Value.Del(entity);
      }
    }
  }
}
