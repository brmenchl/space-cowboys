using System;
using Code.Input;
using Cysharp.Threading.Tasks;
using External.Option;
using UnityEngine;
using Zenject;

namespace Code.Players {
  public class Player {
    public readonly Guid id;
    public readonly ControlScheme controlScheme;
    public readonly Color theme;
    public readonly float maxHealth;

    public readonly AsyncReactiveProperty<Option<IControllable>> controllable =
      new AsyncReactiveProperty<Option<IControllable>>(Option.None<IControllable>());

    public readonly AsyncReactiveProperty<float> health;

    public Player(ControlScheme controlScheme, Color theme, Settings settings) {
      id = Guid.NewGuid();
      this.controlScheme = controlScheme;
      this.theme = theme;
      maxHealth = settings.startingHealth;
      health = new AsyncReactiveProperty<float>(maxHealth);
    }

    public class Factory : PlaceholderFactory<ControlScheme, Color, Player> { }

    [Serializable]
    public class Settings {
      public float startingHealth;
    }
  }
}