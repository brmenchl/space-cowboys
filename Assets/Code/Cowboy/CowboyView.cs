using Code.Ship;
using External.Option;
using UnityEngine;
using Zenject;

namespace Code.Cowboy {
  public class CowboyView : MonoBehaviour {
    private CowboyModel model;
    public CowboyFacade Facade { get; private set; }

    public void OnCollisionEnter2D(Collision2D other) =>
      other.gameObject
        .TryGetComponent<ShipView>()
        .MatchSome(controllable => model.TryBoard(controllable.Facade));

    [Inject]
    private void Inject(CowboyModel model, CowboyFacade facade) {
      this.model = model;
      Facade = facade;
    }
  }
}