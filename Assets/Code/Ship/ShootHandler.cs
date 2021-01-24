using System;
using Code.Bullets;
using Code.Player.Input;
using Code.Utilities;
using Code.Utilities.ScreenWrap;

namespace Code.Ship {
  public class ShootHandler {
    private readonly Bullet.Factory bulletFactory;
    private readonly SWRigidbody2D rigidbody;
    private readonly Settings settings;

    public ShootHandler(Settings settings,
      SWRigidbody2D rigidbody,
      Bullet.Factory bulletFactory,
      InputHandler inputHandler) {
      this.bulletFactory = bulletFactory;
      this.settings = settings;
      this.rigidbody = rigidbody;
      var throttledShoot = ThrottledFunction.ThrottleByRate(Shoot, this.settings.fireRate);
      inputHandler.OnShoot += throttledShoot.Call;
    }

    private void Shoot() =>
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