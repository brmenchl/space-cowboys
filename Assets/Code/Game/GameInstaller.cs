namespace Code.Game {
  using Cowboy;

  using Player;

  using Ship;

  using UnityEngine;

  using Zenject;

  public class GameInstaller : MonoInstaller {
    [SerializeField] private GameObject shipPrefab;
    [SerializeField] private GameObject cowboyPrefab;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject pawnPrefab;

    public override void InstallBindings() {
      Container.BindInterfacesTo<GameRunner>().AsSingle();

      Container.BindFactory<Vector3, Quaternion, ShipFacade, ShipFacade.Factory>()
        .FromSubContainerResolve()
        .ByNewPrefabInstaller<ShipInstaller>(shipPrefab);

      Container.BindFactory<Vector3, Quaternion, CowboyFacade, CowboyFacade.Factory>()
        .FromSubContainerResolve()
        .ByNewPrefabInstaller<CowboyInstaller>(cowboyPrefab);

      Container.BindInstance(bulletPrefab).WhenInjectedInto<ShipInstaller>();
      Container.BindInstance(bulletPrefab).WhenInjectedInto<CowboyInstaller>();

      Container.BindFactory<string, Pawn, Pawn.Factory>().FromComponentInNewPrefab(pawnPrefab);
    }
  }
}