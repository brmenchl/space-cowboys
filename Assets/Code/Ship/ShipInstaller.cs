using Code.Bullets;
using Code.Utilities.ScreenWrap;
using UnityEngine;
using Zenject;

namespace Code.Ship
{
  public class ShipInstaller : Installer<ShipInstaller>
  {
    private GameObject bulletPrefab;

    [Inject]
    private void Inject(GameObject bulletPrefab)
    {
      this.bulletPrefab = bulletPrefab;
    }

    public override void InstallBindings()
    {
      Container.Bind<ShipFacade>().AsSingle();
      Container.Bind<ScreenWrappingRigidbody2D>().FromComponentOnRoot();

      Container.BindInterfacesTo<MoveHandler>().AsSingle();
      Container.Bind<ShootHandler>().AsSingle().NonLazy();

      Container.BindFactory<Bullet, Bullet.Factory>()
        .FromMonoPoolableMemoryPool(x =>
          x
            .WithInitialSize(10)
            .FromComponentInNewPrefab(bulletPrefab)
            .UnderTransformGroup("Bullets")
        );
    }
  }
}
