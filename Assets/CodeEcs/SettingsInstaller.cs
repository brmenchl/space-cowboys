using CodeEcs.Archetypes;
using Zenject;

namespace CodeEcs {
  public class SettingsInstaller : ScriptableObjectInstaller<SettingsInstaller> {
    public ShipArchetypeFactory.Settings shipArchetype;
    public CowboyArchetypeFactory.Settings cowboyArchetype;
    public BulletArchetypeFactory.Settings bulletArchetype;
    public LassoArchetypeFactory.Settings lassoArchetype;
    public GameConfig config;

    public override void InstallBindings() {
      Container.BindInstance(config).IfNotBound();
      Container.BindInstance(shipArchetype).IfNotBound();
      Container.BindInstance(cowboyArchetype).IfNotBound();
      Container.BindInstance(bulletArchetype).IfNotBound();
      Container.BindInstance(lassoArchetype).IfNotBound();
    }
  }
}