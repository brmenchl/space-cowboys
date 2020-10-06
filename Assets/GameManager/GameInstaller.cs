using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
  [SerializeField] private GameObject shipPrefab;
  [SerializeField] private GameObject bulletPrefab;

  public override void InstallBindings()
  {
    Container.BindInterfacesTo<GameRunner>().AsSingle();

    Container.BindFactory<ShipFacade, ShipFacade.Factory>()
        .FromSubContainerResolve().ByNewPrefabInstaller<ShipInstaller>(shipPrefab);

    Container.BindFactory<Bullet, Bullet.Factory>()
      .FromMonoPoolableMemoryPool(x =>
        x
          .WithInitialSize(10)
          .FromComponentInNewPrefab(bulletPrefab)
          .UnderTransformGroup("Bullets")
      );
  }
}
