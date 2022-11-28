using CodeEcs.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace CodeEcs.Systems {
  public class MoveForwardSystem : IEcsRunSystem {
    private readonly EcsPoolInject<MoveForward> moveForwardPool;
    private readonly EcsPoolInject<Trans> transPool;
    private readonly EcsFilterInject<Inc<MoveForward, Trans>> filter;

    public void Run(EcsSystems systems) {
      foreach (var entity in filter.Value) {
        ref var moveForward = ref moveForwardPool.Value.Get(entity);
        ref var t = ref transPool.Value.Get(entity);
        t.transform.Translate(t.transform.up * (moveForward.velocity * Time.deltaTime), Space.World);
      }
    }
  }
}