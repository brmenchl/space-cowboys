using Code.Bullets;
using Code.Player.Input;
using Code.Utilities;
using Code.Utilities.ScreenWrap;

namespace Code.Cowboy {
  public class ShootHandler {
    private readonly Bullet.Factory bulletFactory;

    // private readonly Settings settings;
    private readonly SWRigidbody2D rigidbody;

    public ShootHandler(
      SWRigidbody2D rigidbody,
      Bullet.Factory bulletFactory,
      InputHandler inputHandler) {
      this.bulletFactory = bulletFactory;
      this.rigidbody = rigidbody;
      inputHandler.OnShoot += FireRateHelper.ThrottleByRate(Shoot, 5);
    }

    public void Shoot() {
      var bullet = bulletFactory.Create();
      var bTrans = bullet.transform;
      bTrans.position = rigidbody.transform.position + (rigidbody.Transform.up * 10); //settings.muzzleDistance;
      bTrans.rotation = rigidbody.Transform.rotation;
      PushBack();
    }

    private void PushBack() {
      var force = rigidbody.Transform.up * -1 * 10;
      rigidbody.AddForce(force);
    }

    // [Serializable]
    // public class Settings
    // {
    //   public float muzzleDistance;
    //   public float fireRate;
    // }
  }
}