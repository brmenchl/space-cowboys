using Zenject;

namespace Code.Player.Input
{
  public class InputInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      SignalBusInstaller.Install(Container);

      Container.Bind<InputState>().AsSingle().NonLazy();

      Container.DeclareSignal<ShootSignal>();
    }
  }
}
