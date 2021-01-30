using Code.Game;
using UnityEngine;
using Zenject;

namespace Code.Bullets {
  public class BulletInstaller : Installer<BulletInstaller> {
    private readonly GameObject bulletPrefab;

    public BulletInstaller(PrefabRegistry prefabRegistry) => bulletPrefab = prefabRegistry.bulletPrefab;

    public override void InstallBindings() =>
      Container.BindFactory<Vector3, Quaternion, Bullet, Bullet.Factory>()
        .FromMonoPoolableMemoryPool(x =>
          x
            .WithInitialSize(30)
            .FromComponentInNewPrefab(bulletPrefab)
            .UnderTransformGroup("Bullets")
        );
  }
}