using System;
using CodeEcs.Components;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace CodeEcs.Archetypes {
  public class LassoArchetypeFactory {
    [Inject] Settings settings;

    public int Create(EcsWorld world, Transform lassoParent) {
      var entity = world.NewEntity();
      var lassoGo = GameObject.Instantiate(settings.prefab, lassoParent);

      ref var trans = ref world.GetPool<Trans>().Add(entity);
      trans.transform = lassoGo.transform;

      ref var lasso = ref world.GetPool<Lasso>().Add(entity);
      lasso.ends = new Code.Lasso.LassoEnds { start = lassoParent, end = lassoGo.transform };
      lasso.lineRenderer = lassoGo.GetComponent<LineRenderer>();

      ref var moveForward = ref world.GetPool<MoveForward>().Add(entity);
      moveForward.velocity = settings.velocity;

      ref var destroyAfterTime = ref world.GetPool<DestroyAfterTime>().Add(entity);
      destroyAfterTime.SetLifetime(lassoGo, settings.lifetime);

      return entity;
    }

    [Serializable]
    public struct Settings {
      public float velocity;
      public float lifetime;
      public GameObject prefab;
    }
  }
}