using System;
using CodeEcs.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace CodeEcs.Systems {
  public class DestroyAfterTimeSystem : IEcsRunSystem {
    private readonly EcsWorldInject world;
    private readonly EcsPoolInject<DestroyAfterTime> destroyAfterTimePool;
    private readonly EcsFilterInject<Inc<DestroyAfterTime>> filter;

    public void Run(EcsSystems systems) {
      var now = DateTime.UtcNow;
      foreach (var entity in filter.Value) {
        ref var destroyAfterTimeData = ref destroyAfterTimePool.Value.Get(entity);
        if (now > destroyAfterTimeData.destroyTime) {
          GameObject.Destroy(destroyAfterTimeData.gameObject);
          world.Value.DelEntity(entity);
        }
      }
    }
  }
}