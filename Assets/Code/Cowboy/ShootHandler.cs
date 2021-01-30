using Code.Bullets;
using Code.Utilities;
using UnityEngine;

namespace Code.Cowboy {
  public class ShootHandler {
    private readonly Bullet.Factory bulletFactory;
    private readonly Rigidbody2D rigidbody;
    private readonly ThrottledFunction throttledShoot;
    private readonly Transform transform;

    public ShootHandler(
      Transform transform,
      Rigidbody2D rigidbody,
      Bullet.Factory bulletFactory) {
      this.transform = transform;
      this.rigidbody = rigidbody;
      this.bulletFactory = bulletFactory;
      throttledShoot = ThrottledFunction.ThrottleByRate(DoShoot, 5);
    }

    public void Shoot() =>
      throttledShoot.Call();

    private void DoShoot() {
      bulletFactory.Create(rigidbody.transform.position + (transform.up * 10), transform.rotation);
      PushBack();
    }

    private void PushBack() {
      var force = transform.up * -1 * 10;
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