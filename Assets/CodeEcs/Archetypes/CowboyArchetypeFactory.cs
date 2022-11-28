using System;
using CodeEcs.Components;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace CodeEcs.Archetypes {
  public class CowboyArchetypeFactory {
    [Inject] Settings settings;

    public int Create(EcsWorld world, Vector2 position, Quaternion rotation, Vector2 ejectForce) {
      var entity = world.NewEntity();
      var cowboyGo = GameObject.Instantiate(settings.prefab);

      ref var trans = ref world.GetPool<Trans>().Add(entity);
      trans.transform = cowboyGo.transform;

      ref var hasGuns = ref world.GetPool<HasGuns>().Add(entity);
      hasGuns.muzzleDistance = settings.muzzleDistance;

      ref var physicsBody = ref world.GetPool<PhysicsBody>().Add(entity);
      physicsBody.rigidBody = cowboyGo.GetComponent<Rigidbody2D>();
      physicsBody.rigidBody.transform.SetPositionAndRotation(position, rotation);
      physicsBody.rigidBody.AddForce(ejectForce, ForceMode2D.Impulse);

      ref var physicsMovement = ref world.GetPool<PhysicsMovement>().Add(entity);
      physicsMovement.thrustForce = 0;
      physicsMovement.turnForce = settings.turnForce;
      return entity;
    }

    [Serializable]
    public struct Settings {
      public float turnForce;
      public float muzzleDistance;
      public GameObject prefab;
    }
  }
}