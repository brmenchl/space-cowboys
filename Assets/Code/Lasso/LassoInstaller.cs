using UnityEngine;
using Zenject;

namespace Code.Lasso {
  public class LassoInstaller : MonoInstaller<LassoInstaller> {
    [SerializeField] private GameObject lassoTipPrefab;

    public override void InstallBindings() {
      Container.Bind<LassoEnds>().AsSingle();
      Container.Bind<LineRenderer>().FromComponentOnRoot().WhenInjectedInto<LassoRenderer>();
      Container.Bind<DistanceJoint2D>().FromComponentOnRoot().WhenInjectedInto<Lasso>();
      Container.BindInterfacesTo<LassoRenderer>().AsSingle().NonLazy();
      Container.BindFactory<Vector2, Quaternion, LassoTip, LassoTip.Factory>()
        .FromComponentInNewPrefab(lassoTipPrefab);
    }
  }
}