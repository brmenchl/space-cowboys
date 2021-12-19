using Code.Input;
using UnityEngine;
using Zenject;

namespace Code.Players {
  public class PlayerInstaller : Installer<PlayerInstaller> {
    public override void InstallBindings() {
      Container.Bind<PlayerState>().AsSingle();
      Container.Bind<PlayerService>().AsSingle();
      Container.BindInterfacesAndSelfTo<PlayerStreams>().AsSingle();
      Container.Bind<BoardEjectService>().AsSingle();

      Container.BindFactory<ControlScheme, Color, Player, Player.Factory>();
    }
  }
}