using Code.Bullets;
using Code.Ship;
using UnityEngine;
using Zenject;

namespace Code.Game {
  [CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
  public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller> {
    public Bullet.Settings bullet;
    public MoveHandler.Settings shipMovement;
    public Cowboy.MoveHandler.Settings cowboyMovement;
    public ShootHandler.Settings shipShooting;
    public Cowboy.ShootHandler.Settings cowboyShooting;
    public Player.HealthManager.Settings playerHealth;
    public ShipModel.Settings shipHealth;

    public override void InstallBindings() {
      Container.BindInstance(bullet).IfNotBound();
      Container.BindInstance(shipMovement).IfNotBound();
      Container.BindInstance(cowboyMovement).IfNotBound();
      Container.BindInstance(shipShooting).IfNotBound();
      Container.BindInstance(cowboyShooting).IfNotBound();
      Container.BindInstance(cowboyShooting).IfNotBound();
      Container.BindInstance(playerHealth).IfNotBound();
      Container.BindInstance(shipHealth).IfNotBound();
    }
  }
}