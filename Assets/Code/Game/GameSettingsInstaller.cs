namespace Code.Game {
  using Bullets;

  using Ship;

  using UnityEngine;

  using Zenject;

  [CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
  public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller> {
    public Bullet.Settings bullet;
    public MoveHandler.Settings moving;
    public ShootHandler.Settings shooting;
    public HealthHandler.Settings health;

    public override void InstallBindings() {
      Container.BindInstance(bullet).IfNotBound();
      Container.BindInstance(moving).IfNotBound();
      Container.BindInstance(shooting).IfNotBound();
      Container.BindInstance(health).IfNotBound();
    }
  }
}