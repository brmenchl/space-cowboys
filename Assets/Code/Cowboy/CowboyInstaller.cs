using Code.Lasso;
using UnityEngine;
using Zenject;

namespace Code.Cowboy {
  public class CowboyInstaller : MonoInstaller<CowboyInstaller> {
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject lassoPrefab;

    private Vector3 position;
    private Quaternion rotation;

    [Inject]
    public void Inject(Vector3 position, Quaternion rotation) {
      this.position = position;
      this.rotation = rotation;
    }

    public override void InstallBindings() {
      Container.BindInterfacesAndSelfTo<CowboyFacade>().AsSingle();
      Container.Bind<CowboyModel>().AsSingle().WithArguments(position, rotation).NonLazy();
      Container.Bind<Rigidbody2D>().FromComponentOnRoot();
      Container.Bind<Transform>().FromComponentOnRoot();
      Container.Bind<MoveHandler>().AsSingle().NonLazy();
      Container.Bind<ShootHandler>().AsSingle().NonLazy();
      Container.BindInstance(spriteRenderer);
      Container.Bind<DamageableView>().FromNewComponentOnRoot().AsSingle().NonLazy();
      Container.Bind<CowboyView>().FromNewComponentOnRoot().AsSingle().NonLazy();

      Container.BindFactory<Lasso.Lasso, Lasso.Lasso.Factory>()
        .FromSubContainerResolve()
        .ByNewPrefabInstaller<LassoInstaller>(lassoPrefab);
    }
  }
}