using Code.Cowboy;
using Code.Player;
using Code.Player.Input;
using Code.Ship;
using UnityEngine;
using Zenject;

namespace Code.Game {
  public class GameInstaller : MonoInstaller {
    [SerializeField] private GameObject shipPrefab;
    [SerializeField] private GameObject cowboyPrefab;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject pawnPrefab;

    public override void InstallBindings() {
      Container.BindInterfacesTo<GameRunner>().AsSingle();

      Container.BindFactory<ControlScheme, IPossessable, PlayerController, PlayerController.Factory>()
        .FromSubContainerResolve()
        .ByNewPrefabInstaller<PlayerInstaller>(pawnPrefab);

      Container.BindFactory<Vector3, Quaternion, ShipFacade, ShipFacade.Factory>()
        .FromSubContainerResolve()
        .ByNewPrefabInstaller<ShipInstaller>(shipPrefab);

      Container.BindFactory<Vector3, Quaternion, CowboyFacade, CowboyFacade.Factory>()
        .FromSubContainerResolve()
        .ByNewPrefabInstaller<CowboyInstaller>(cowboyPrefab);

      Container.BindInstance(bulletPrefab).WhenInjectedInto<ShipInstaller>();
      Container.BindInstance(bulletPrefab).WhenInjectedInto<CowboyInstaller>();
    }
  }
}