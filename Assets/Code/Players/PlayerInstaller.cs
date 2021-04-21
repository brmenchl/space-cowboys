using Zenject;

namespace Code.Players {
  public class PlayerInstaller : Installer<PlayerInstaller> {
    public override void InstallBindings() {
      Container.Bind<PlayerState>().AsSingle();
      Container.Bind<PlayerService>().AsSingle();
      Container.Bind<PlayerStreams>().AsSingle();
      Container.Bind<BoardEjectService>().AsSingle();
    }
  }
}