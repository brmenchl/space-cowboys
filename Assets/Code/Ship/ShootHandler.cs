using System;
using Code.Player.Input;
using Code.Utilities.ScreenWrap;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Code.Ship
{
  public class ShootHandler
  {
    private float muzzleDistance = 2;
    private float fireRate = 1;
    private readonly ScreenWrappingRigidbody2D rigidbody;
    private readonly Bullet.Bullet.Factory bulletFactory;

    private bool canShoot = true;

    public ShootHandler(SignalBus signalBus, ScreenWrappingRigidbody2D rigidbody, Bullet.Bullet.Factory bulletFactory)
    {
      this.bulletFactory = bulletFactory;
      this.rigidbody = rigidbody;
      signalBus.Subscribe<ShootSignal>(Shoot);
    }

    private async void Shoot()
    {
      if (!canShoot) return;

      canShoot = false;
      var bullet = bulletFactory.Create();
      var bTrans = bullet.transform;
      bTrans.position = rigidbody.transform.position + rigidbody.Up * muzzleDistance;
      bTrans.rotation = rigidbody.Rotation;
      await UniTask.Delay(TimeSpan.FromSeconds(1 / fireRate));
      canShoot = true;
    }
  }
}
