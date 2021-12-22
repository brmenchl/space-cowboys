using Code.Utilities;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Button = UnityEngine.UI.Button;

namespace Code.Menus.MainMenu {
  public class MainMenuPresenter : MonoBehaviour {
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;

    private void Awake() {
      startButton.OnClickAsAsyncEnumerable()
        .SubscribeAwaitThrottled(HandleStartClicked, 300, this.GetCancellationTokenOnDestroy());
      exitButton.OnClickAsAsyncEnumerable()
        .SubscribeAwaitThrottled(HandleExitClicked, 300, this.GetCancellationTokenOnDestroy());
    }

    private void HandleStartClicked(AsyncUnit _) {
      // Enter player select screen
      Debug.Log("Start");
    }

    private static void HandleExitClicked(AsyncUnit _) => AppHelper.Exit();
  }
}