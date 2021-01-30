using System;
using Code.Bullets;
using Code.Utilities;
using Code.Utilities.ScreenWrap;

namespace Code.Ship {
  public class ShootHandler {
    private readonly Bullet.Factory bulletFactory;
    private readonly SWRigidbody2D rigidbody;
    private readonly Settings settings;
    private readonly ThrottledFunction throttledShoot;

    public ShootHandler(Settings settings,
      SWRigidbody2D rigidbody,
      Bullet.Factory bulletFactory) {
      this.bulletFactory = bulletFactory;
      this.settings = settings;
      this.rigidbody = rigidbody;
      throttledShoot = ThrottledFunction.ThrottleByRate(DoShoot, this.settings.fireRate);
    }

    public void Shoot() =>
      throttledShoot.Call();

    private void DoShoot() =>
      bulletFactory.Create(
        rigidbody.transform.position + (rigidbody.Transform.up * settings.muzzleDistance),
        rigidbody.Transform.rotation
      );

    [Serializable]
    public class Settings {
      public float muzzleDistance;
      public float fireRate;
    }
  }
}