using UnityEngine;
using Zenject;

namespace Code.Ship {
  public class ShipInstaller : Installer<ShipInstaller> {
    private readonly Vector3 position;
    private readonly Quaternion rotation;

    public ShipInstaller(Vector3 position, Quaternion rotation) {
      this.position = position;
      this.rotation = rotation;
    }

    public override void InstallBindings() {
      Container.Bind<ShipFacade>().AsSingle();
      Container.Bind<ShipModel>().AsSingle().WithArguments(position, rotation).NonLazy();
      Container.Bind<Rigidbody2D>().FromComponentOnRoot();
      Container.Bind<Transform>().FromComponentOnRoot();

      Container.Bind<MoveHandler>().AsSingle().NonLazy();
      Container.Bind<ShootHandler>().AsSingle().NonLazy();
    }
  }
}