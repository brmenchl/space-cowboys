using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class ShootHandler
{
  private float muzzleDistance = 2;
  private float fireRate = 1;
  private ScreenWrappingRigidbody2D rigidbody;
  private Bullet.Factory bulletFactory;

  private bool canShoot = true;

  public ShootHandler(SignalBus signalBus, ScreenWrappingRigidbody2D rigidbody, Bullet.Factory bulletFactory)
  {
    this.bulletFactory = bulletFactory;
    this.rigidbody = rigidbody;
    signalBus.Subscribe<ShootSignal>(Shoot);
  }

  private async void Shoot()
  {
    if (canShoot)
    {
      canShoot = false;
      var bullet = bulletFactory.Create();
      bullet.transform.position = rigidbody.transform.position + (rigidbody.Up * muzzleDistance);
      bullet.transform.rotation = rigidbody.Rotation;
      Debug.Log($"Rotation: {rigidbody.Up}");
      await UniTask.Delay(TimeSpan.FromSeconds(1 / fireRate));
      canShoot = true;
    }
  }
}
