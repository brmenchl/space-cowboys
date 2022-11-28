using System;
using CodeEcs.Components;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace CodeEcs.Archetypes {
  public class BulletArchetypeFactory {
    [Inject] Settings settings;
    // TODO: memory pool

    public int Create(EcsWorld world, Vector2 position, Quaternion rotation) {
      var entity = world.NewEntity();
      var bulletGo = GameObject.Instantiate(settings.prefab);

      ref var trans = ref world.GetPool<Trans>().Add(entity);
      trans.transform = bulletGo.transform;
      trans.transform.SetPositionAndRotation(position, rotation);

      ref var moveForward = ref world.GetPool<MoveForward>().Add(entity);
      moveForward.velocity = settings.velocity;

      ref var destroyAfterTime = ref world.GetPool<DestroyAfterTime>().Add(entity);
      destroyAfterTime.SetLifetime(bulletGo, settings.lifetime);

      return entity;
    }

    [Serializable]
    public struct Settings {
      public float velocity;
      public float lifetime;
      public float damage;
      public GameObject prefab;
    }
  }
}