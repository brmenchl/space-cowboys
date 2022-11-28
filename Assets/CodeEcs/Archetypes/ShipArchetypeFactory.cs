using System;
using Code.Game;
using CodeEcs.Components;
using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace CodeEcs.Archetypes {
  public class ShipArchetypeFactory {
    [Inject] Settings settings;

    public int Create(EcsWorld world, Vector2 position, Quaternion rotation) {
      var entity = world.NewEntity();
      var shipGo = GameObject.Instantiate(settings.prefab);

      ref var trans = ref world.GetPool<Trans>().Add(entity);
      trans.transform = shipGo.transform;

      ref var hasGuns = ref world.GetPool<HasGuns>().Add(entity);
      hasGuns.muzzleDistance = settings.muzzleDistance;

      ref var piloted = ref world.GetPool<Piloted>().Add(entity);
      piloted.ejectDistance = settings.ejectDistance;
      piloted.ejectForce = settings.ejectForce;

      ref var physicsBody = ref world.GetPool<PhysicsBody>().Add(entity);
      physicsBody.rigidBody = shipGo.GetComponent<Rigidbody2D>();
      physicsBody.rigidBody.transform.SetPositionAndRotation(position, rotation);

      ref var physicsMovement = ref world.GetPool<ThrustMovement>().Add(entity);
      physicsMovement.force = settings.thrustForce;

      ref var torqueTurning = ref world.GetPool<TorqueTurning>().Add(entity);
      torqueTurning.torque = settings.turnTorque;
      return entity;
    }

    [Serializable]
    public struct Settings {
      public float thrustForce;
      public float turnTorque;
      public float muzzleDistance;
      public float ejectDistance;
      public float ejectForce;
      public GameObject prefab;
    }
  }
}