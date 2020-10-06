using UnityEngine;
using Zenject;

public class ShipInstaller : Installer<ShipInstaller>
{
  public override void InstallBindings()
  {
    Container.Bind<ShipFacade>().AsSingle();
    Container.Bind<Transform>().FromComponentOnRoot();
    Container.Bind<ScreenWrappingRigidbody2D>().FromComponentOnRoot();

    Container.BindInterfacesTo<MoveHandler>().AsSingle();
    Container.Bind<ShootHandler>().AsSingle().NonLazy();
  }
}
