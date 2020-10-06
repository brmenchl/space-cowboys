using System;
using Code.Bullets;
using Code.Player.Input;
using Code.Utilities.ScreenWrap;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Code.Ship
{
  public class ShootHandler
  {
    private readonly Settings settings;
    private readonly ScreenWrappingRigidbody2D rigidbody;
    private readonly Bullet.Factory bulletFactory;

    private bool canShoot = true;

    public ShootHandler(SignalBus signalBus, Settings settings, ScreenWrappingRigidbody2D rigidbody,
      Bullet.Factory bulletFactory)
    {
      this.bulletFactory = bulletFactory;
      this.settings = settings;
      this.rigidbody = rigidbody;
      signalBus.Subscribe<ShootSignal>(Shoot);
    }

    private async void Shoot()
    {
      if (!canShoot) return;

      canShoot = false;
      var bullet = bulletFactory.Create();
      var bTrans = bullet.transform;
      bTrans.position = rigidbody.transform.position + rigidbody.Up * settings.muzzleDistance;
      bTrans.rotation = rigidbody.Rotation;
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
