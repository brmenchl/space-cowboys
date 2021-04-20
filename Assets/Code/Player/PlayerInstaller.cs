using Zenject;

namespace Code.Player {
  public class PlayerInstaller : Installer<PlayerInstaller> {
    public override void InstallBindings() {
      Container.Bind<PlayerState>().AsSingle();
      Container.Bind<PlayerService>().AsSingle();
      Container.Bind<BoardEjectService>().AsSingle();
    }
  }
}