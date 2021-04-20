using Code.Game;
using Code.Lasso;
using UnityEngine;
using Zenject;

namespace Code.Cowboy {
  public class CowboyInstaller : MonoInstaller<CowboyInstaller> {
    private GameObject lassoPrefab;
    private Vector3 position;
    private Quaternion rotation;

    [Inject]
    public void Inject(Vector3 position, Quaternion rotation, PrefabRegistry prefabRegistry) {
      this.position = position;
      this.rotation = rotation;
      lassoPrefab = prefabRegistry.lassoPrefab;
    }

    public override void InstallBindings() {
      Container.BindInterfacesAndSelfTo<CowboyFacade>().AsSingle();
      Container.Bind<CowboyModel>().AsSingle().WithArguments(position, rotation).NonLazy();
      Container.Bind<Rigidbody2D>().FromComponentOnRoot();
      Container.Bind<Transform>().FromComponentOnRoot();
      Container.Bind<MoveHandler>().AsSingle().NonLazy();
      Container.Bind<ShootHandler>().AsSingle().NonLazy();
      Container.BindFactory<Lasso.Lasso, Lasso.Lasso.Factory>()
        .FromSubContainerResolve()
        .ByNewPrefabInstaller<LassoInstaller>(lassoPrefab);
    }
  }
}