using Code.Input;
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
      InputInstaller.Install(Container, controlScheme);
      Container.Bind<PlayerFacade>().AsSingle();
      Container.Bind<HealthManager>().AsSingle();
      Container.Bind<EjectionManager>().AsSingle();
      Container.Bind<ControllableState>().AsSingle().WithArguments(startingControllable);
      Container.Bind<ControllableListener>().AsSingle().NonLazy();
    }
  }
}