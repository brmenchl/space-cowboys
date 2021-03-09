using UnityEngine;
using Zenject;

namespace Code.Lasso {
  public class LassoInstaller : Installer<LassoInstaller> {
    public override void InstallBindings() {
      Container.Bind<Lasso>().FromComponentOnRoot();
      Container.Bind<LassoEnds>().AsSingle();
      Container.Bind<LineRenderer>().FromComponentOnRoot().WhenInjectedInto<LassoRenderer>();
      Container.BindInterfacesTo<LassoRenderer>().AsSingle().NonLazy();
    }
  }
}