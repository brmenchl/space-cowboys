using System;
using Code.Bullets;
using Code.Player.Input;
using Code.Utilities.ScreenWrap;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Code.Ship {
  public class ShootHandler : ITickable {
    private readonly Bullet.Factory bulletFactory;
    private readonly InputHandler inputHandler;
    private readonly SWRigidbody2D rigidbody;
    private readonly Settings settings;

    private bool canShoot = true;

    public ShootHandler(Settings settings,
      SWRigidbody2D rigidbody,
      Bullet.Factory bulletFactory,
      InputHandler inputHandler) {
      this.bulletFactory = bulletFactory;
      this.inputHandler = inputHandler;
      this.settings = settings;
      this.rigidbody = rigidbody;
    }

    public void Tick() =>
      inputHandler.IfPossessed(state => {
        if (state.isShooting) Shoot().Forget();
      });

    private async UniTaskVoid Shoot() {
      if (!canShoot) return;

      canShoot = false;
      var bullet = bulletFactory.Create();
      var bTrans = bullet.transform;
      bTrans.position = rigidbody.transform.position + (rigidbody.Transform.up * settings.muzzleDistance);
      bTrans.rotation = rigidbody.Transform.rotation;
      await UniTask.Delay(TimeSpan.FromSeconds(1 / settings.fireRate));
      canShoot = true;
    }

    [Serializable]
    public class Settings {
      public float muzzleDistance;
      public float fireRate;
    }
  }
}