using Code.Utilities.ScreenWrap;
using Zenject;

namespace Code.Ship
{
  public class ShipInstaller : Installer<ShipInstaller>
  {
    public override void InstallBindings()
    {
      Container.Bind<ShipFacade>().AsSingle();
      Container.Bind<ScreenWrappingRigidbody2D>().FromComponentOnRoot();

      Container.BindInterfacesTo<MoveHandler>().AsSingle();
      Container.Bind<ShootHandler>().AsSingle().NonLazy();
    }
  }
}
