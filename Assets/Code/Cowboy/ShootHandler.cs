using System;
using Code.Bullets;
using Code.Player.Input;
using Code.Utilities.ScreenWrap;
using Cysharp.Threading.Tasks;

namespace Code.Cowboy {
  public class ShootHandler {
    private readonly Bullet.Factory bulletFactory;

    // private readonly Settings settings;
    private readonly SWRigidbody2D rigidbody;

    private bool canShoot = true;

    public ShootHandler(
      SWRigidbody2D rigidbody,
      Bullet.Factory bulletFactory,
      InputHandler inputHandler) {
      this.bulletFactory = bulletFactory;
      this.rigidbody = rigidbody;
      inputHandler.OnShoot += Shoot;
    }

    public async void Shoot() {
      if (!canShoot) return;

      canShoot = false;
      var bullet = bulletFactory.Create();
      var bTrans = bullet.transform;
      bTrans.position = rigidbody.transform.position + (rigidbody.Transform.up * 10); //settings.muzzleDistance;
      bTrans.rotation = rigidbody.Transform.rotation;
      await UniTask.Delay(TimeSpan.FromSeconds(0.2));
      canShoot = true;
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