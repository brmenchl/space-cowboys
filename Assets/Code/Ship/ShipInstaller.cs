using Code.Bullets;
using Code.Game;
using Code.Player.Input;
using Code.Utilities.ScreenWrap;
using UnityEngine;
using Zenject;

namespace Code.Ship {
  public class ShipInstaller : Installer<ShipInstaller> {
    private GameObject bulletPrefab;
    private Vector3 position;
    private Quaternion rotation;

    [Inject]
    private void Inject(PrefabRegistry prefabRegistry, Vector3 position, Quaternion rotation) {
      bulletPrefab = prefabRegistry.bulletPrefab;
      this.position = position;
      this.rotation = rotation;
    }

    public override void InstallBindings() {
      Container.Bind<ShipFacade>().AsSingle();
      Container.Bind<ShipModel>().AsSingle().WithArguments(position, rotation).NonLazy();
      Container.Bind<SWRigidbody2D>().FromComponentOnRoot();

      Container.Bind<MoveHandler>().AsSingle().NonLazy();
      Container.Bind<ShootHandler>().AsSingle().NonLazy();

      Container.BindFactory<Vector3, Quaternion, Bullet, Bullet.Factory>()
        .FromMonoPoolableMemoryPool(x =>
          x
            .WithInitialSize(10)
            .FromComponentInNewPrefab(bulletPrefab)
            .UnderTransformGroup("Bullets")
        );
    }
  }
}