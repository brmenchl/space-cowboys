using Code.Player;
using Code.Player.Input;
using Code.Ship;
using UnityEngine;
using Zenject;

namespace Code.Game {
  public class GameInstaller : MonoInstaller {
    [SerializeField] private PrefabRegistry prefabRegistry;

    public override void InstallBindings() {
      Container.BindInterfacesTo<GameRunner>().AsSingle();
      Container.Bind<PrefabRegistry>().FromInstance(prefabRegistry);

      Container.BindFactory<ControlScheme, IPossessable, PlayerController, PlayerController.Factory>()
        .FromSubContainerResolve()
        .ByNewPrefabInstaller<PlayerInstaller>(prefabRegistry.pawnPrefab)
        .UnderTransformGroup("Players");

      Container.BindFactory<Vector3, Quaternion, ShipFacade, ShipFacade.Factory>()
        .FromSubContainerResolve()
        .ByNewPrefabInstaller<ShipInstaller>(prefabRegistry.shipPrefab);
    }
  }
}