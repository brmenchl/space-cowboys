using System;
using Code.Bullets;
using Code.Utilities;
using UnityEngine;

namespace Code.Ship {
  public class ShootHandler {
    private readonly Bullet.Factory bulletFactory;
    private readonly Settings settings;
    private readonly ThrottledFunction throttledShoot;
    private readonly Transform transform;

    public ShootHandler(
      Transform transform,
      Settings settings,
      Bullet.Factory bulletFactory) {
      this.bulletFactory = bulletFactory;
      this.settings = settings;
      this.transform = transform;
      throttledShoot = ThrottledFunction.ThrottleByRate(DoShoot, this.settings.fireRate);
    }

    public void Shoot() =>
      throttledShoot.Call();

    private void DoShoot() =>
      bulletFactory.Create(
        transform.position + (transform.up * settings.muzzleDistance),
        transform.rotation
      );

    [Serializable]
    public class Settings {
      public float muzzleDistance;
      public float fireRate;
    }
  }
}