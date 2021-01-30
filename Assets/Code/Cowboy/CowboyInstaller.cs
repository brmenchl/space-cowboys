using UnityEngine;
using Zenject;

namespace Code.Cowboy {
  public class CowboyInstaller : Installer<CowboyInstaller> {
    private readonly Vector3 position;
    private readonly Quaternion rotation;

    public CowboyInstaller(Vector3 position, Quaternion rotation) {
      this.position = position;
      this.rotation = rotation;
    }

    public override void InstallBindings() {
      Container.Bind<CowboyFacade>().AsSingle();
      Container.Bind<CowboyModel>().AsSingle().WithArguments(position, rotation).NonLazy();
      Container.Bind<Rigidbody2D>().FromComponentOnRoot();
      Container.Bind<Transform>().FromComponentOnRoot();

      Container.Bind<MoveHandler>().AsSingle().NonLazy();
      Container.Bind<ShootHandler>().AsSingle().NonLazy();
    }
  }
}