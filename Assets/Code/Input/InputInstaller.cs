using Code.Game;
using Zenject;

namespace Code.Input {
  public class InputInstaller : Installer<InputInstaller> {
    private readonly PrefabRegistry prefabRegistry;

    public InputInstaller(PrefabRegistry prefabRegistry) => this.prefabRegistry = prefabRegistry;

    public override void InstallBindings() {
      Container.Bind<InputState>().AsSingle();
      Container.Bind<InputService>().AsSingle();

      Container.BindFactory<ControlScheme, Controller, Controller.Factory>()
        .FromComponentInNewPrefab(prefabRegistry.controllerPrefab)
        .UnderTransformGroup("Players");
    }
  }
}