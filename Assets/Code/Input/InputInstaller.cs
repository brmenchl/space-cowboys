using UnityEngine;
using Zenject;

namespace Code.Input {
  public class InputInstaller : Installer<GameObject, InputInstaller> {
    private GameObject controllerPrefab;

    public InputInstaller(GameObject controllerPrefab) {
      this.controllerPrefab = controllerPrefab;
    }

    public override void InstallBindings() {
      Container.Bind<InputState>().AsSingle();
      Container.Bind<InputService>().AsSingle();

      Container.BindFactory<ControlScheme, Controller, Controller.Factory>()
        .FromComponentInNewPrefab(controllerPrefab.gameObject)
        .UnderTransformGroup("Controllers");
    }
  }
}