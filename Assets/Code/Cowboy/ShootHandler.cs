using System;
using Code.Bullets;
using Code.Utilities;
using UnityEngine;

namespace Code.Cowboy {
  public class ShootHandler {
    private readonly Bullet.Factory bulletFactory;
    private readonly Rigidbody2D rigidbody;
    private readonly Settings settings;
    private readonly ThrottledFunction throttledShoot;
    private readonly Transform transform;

    public ShootHandler(
      Transform transform,
      Rigidbody2D rigidbody,
      Bullet.Factory bulletFactory,
      Settings settings) {
      this.transform = transform;
      this.rigidbody = rigidbody;
      this.bulletFactory = bulletFactory;
      this.settings = settings;
      throttledShoot = ThrottledFunction.ThrottleByRate(DoShoot, settings.fireRate);
    }

    public void Shoot() => throttledShoot.Call();

    private void DoShoot() {
      bulletFactory.Create(
        rigidbody.transform.position + (transform.up * settings.muzzleDistance),
        transform.rotation
      );
      PushBack();
    }

    private void PushBack() => rigidbody.AddForce(transform.up * -1 * settings.pushBackForce);

    [Serializable]
    public class Settings {
      public float muzzleDistance;
      public float fireRate;
      public float pushBackForce;
    }
  }
}