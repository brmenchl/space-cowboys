using Code.Cowboy;
using Code.Game;
using Code.Player.Input;
using UnityEngine;
using Zenject;

namespace Code.Player {
  public class PlayerInstaller : Installer<ControlScheme, PlayerInstaller> {
    private readonly ControlScheme controlScheme;
    private readonly IPossessable startingPossessable;
    private readonly GameObject cowboyPrefab;

    public PlayerInstaller(ControlScheme controlScheme,
      IPossessable startingPossessable,
      PrefabRegistry prefabRegistry) {
      this.controlScheme = controlScheme;
      this.startingPossessable = startingPossessable;
      cowboyPrefab = prefabRegistry.cowboyPrefab;
    }

    public override void InstallBindings() {
      Container.Bind<PlayerController>().AsSingle().WithArguments(startingPossessable);
      Container.Bind<ControlScheme>().FromInstance(controlScheme).AsSingle();
      Container.Bind<Pawn>().FromComponentOnRoot().WithArguments(controlScheme);
      Container.Bind<InputState>().AsTransient().WhenInjectedInto<Pawn>();

      Container.BindFactory<Vector3, Quaternion, CowboyFacade, CowboyFacade.Factory>()
        .FromSubContainerResolve()
        .ByNewPrefabInstaller<CowboyInstaller>(cowboyPrefab);
    }
  }
}