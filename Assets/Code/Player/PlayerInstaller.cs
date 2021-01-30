using Code.Player.Input;
using Zenject;

namespace Code.Player {
  public class PlayerInstaller : Installer<ControlScheme, PlayerInstaller> {
    private readonly ControlScheme controlScheme;
    private readonly IControllable startingControllable;

    public PlayerInstaller(
      ControlScheme controlScheme,
      IControllable startingControllable
    ) {
      this.controlScheme = controlScheme;
      this.startingControllable = startingControllable;
    }

    public override void InstallBindings() {
      Container.Bind<PlayerController>().AsSingle().WithArguments(startingControllable);
      Container.Bind<ControlScheme>().FromInstance(controlScheme).AsSingle();
      Container.Bind<Pawn>().FromComponentOnRoot();
      Container.Bind<InputState>().AsSingle().WhenInjectedInto<Pawn>();
      Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle();
    }
  }
}