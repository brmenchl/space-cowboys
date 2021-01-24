using Code.Player.Input;
using Zenject;

namespace Code.Player {
  public class PlayerInstaller : Installer<ControlScheme, PlayerInstaller> {
    private readonly ControlScheme controlScheme;
    private readonly IPossessable startingPossessable;

    public PlayerInstaller(ControlScheme controlScheme, IPossessable startingPossessable) {
      this.controlScheme = controlScheme;
      this.startingPossessable = startingPossessable;
    }

    public override void InstallBindings() {
      Container.Bind<PlayerController>().AsSingle().WithArguments(startingPossessable);
      Container.Bind<ControlScheme>().FromInstance(controlScheme).AsSingle();
      Container.Bind<Pawn>().FromComponentOnRoot().WithArguments(controlScheme);
      Container.Bind<InputState>().AsTransient().WhenInjectedInto<Pawn>();
    }
  }
}