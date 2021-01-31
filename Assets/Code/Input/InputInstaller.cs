using Zenject;

namespace Code.Input {
  public class InputInstaller : Installer<ControlScheme, InputInstaller> {
    private readonly ControlScheme controlScheme;

    public InputInstaller(ControlScheme controlScheme) => this.controlScheme = controlScheme;

    public override void InstallBindings() {
      Container.Bind<ControlScheme>().FromInstance(controlScheme).AsSingle();
      Container.Bind<Pawn>().FromComponentOnRoot();
      Container.Bind<InputState>().AsSingle().WhenInjectedInto<Pawn>();
      Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle();
    }
  }
}