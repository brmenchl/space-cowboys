using Code.Cowboy;
using Code.Game;
using Code.Player.Input;
using UnityEngine;
using Zenject;

namespace Code.Player {
  public class PlayerInstaller : Installer<ControlScheme, PlayerInstaller> {
    private readonly ControlScheme controlScheme;
    private readonly IControllable startingControllable;
    private readonly GameObject cowboyPrefab;

    public PlayerInstaller(ControlScheme controlScheme,
      IControllable startingControllable,
      PrefabRegistry prefabRegistry) {
      this.controlScheme = controlScheme;
      this.startingControllable = startingControllable;
      cowboyPrefab = prefabRegistry.cowboyPrefab;
    }

    public override void InstallBindings() {
      Container.Bind<PlayerController>().AsSingle().WithArguments(startingControllable);
      Container.Bind<ControlScheme>().FromInstance(controlScheme).AsSingle();
      Container.Bind<Pawn>().FromComponentOnRoot();
      Container.Bind<InputState>().AsSingle().WhenInjectedInto<Pawn>();
      Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle();

      Container.BindFactory<Vector3, Quaternion, CowboyFacade, CowboyFacade.Factory>()
        .FromSubContainerResolve()
        .ByNewPrefabInstaller<CowboyInstaller>(cowboyPrefab);
    }
  }
}