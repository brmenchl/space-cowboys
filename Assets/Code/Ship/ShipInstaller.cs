using UnityEngine;
using Zenject;

namespace Code.Ship {
  public class ShipInstaller : MonoInstaller<ShipInstaller> {
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Vector3 position;
    private Quaternion rotation;

    [Inject]
    public void Inject(Vector3 position, Quaternion rotation) {
      this.position = position;
      this.rotation = rotation;
    }

    public override void InstallBindings() {
      Container.BindInterfacesAndSelfTo<ShipFacade>().AsSingle();
      Container.Bind<ShipModel>().AsSingle().WithArguments(position, rotation).NonLazy();
      Container.Bind<Rigidbody2D>().FromComponentOnRoot();
      Container.Bind<Transform>().FromComponentOnRoot();
      Container.Bind<Sprite>().FromInstance(spriteRenderer.sprite);

      Container.Bind<MoveHandler>().AsSingle().NonLazy();
      Container.Bind<ShootHandler>().AsSingle().NonLazy();
    }
  }
}