using System;
using Code.Bullets;
using Code.Utilities.ScreenWrap;
using Cysharp.Threading.Tasks;

namespace Code.Ship
{
  public class ShootHandler
  {
    private readonly Settings settings;
    private readonly ScreenWrappingRigidbody2D rigidbody;
    private readonly Bullet.Factory bulletFactory;

    private bool canShoot = true;

    public ShootHandler(Settings settings,
      ScreenWrappingRigidbody2D rigidbody,
      Bullet.Factory bulletFactory)
    {
      this.bulletFactory = bulletFactory;
      this.settings = settings;
      this.rigidbody = rigidbody;
    }

    public async void Shoot()
    {
      if (!canShoot) return;

      canShoot = false;
      var bullet = bulletFactory.Create();
      var bTrans = bullet.transform;
      bTrans.position = rigidbody.transform.position + rigidbody.Transform.up * settings.muzzleDistance;
      bTrans.rotation = rigidbody.Transform.rotation;
      await UniTask.Delay(TimeSpan.FromSeconds(1 / settings.fireRate));
      canShoot = true;
    }

    [Serializable]
    public class Settings
    {
      public float muzzleDistance;
      public float fireRate;
    }
  }
}
