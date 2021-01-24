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
      inputHandler.OnShoot += FireRateHelper.ThrottleByRate(Shoot, this.settings.fireRate);
    }

    private void Shoot() {
      var bullet = bulletFactory.Create();
      var bTrans = bullet.transform;
      bTrans.position = rigidbody.transform.position + (rigidbody.Transform.up * settings.muzzleDistance);
      bTrans.rotation = rigidbody.Transform.rotation;
    }

    [Serializable]
    public class Settings {
      public float muzzleDistance;
      public float fireRate;
    }
  }
}