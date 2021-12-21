using System;
using Code.Utilities.Extensions;

namespace Code.Players {
  public readonly struct Health {
    private readonly float maxValue;
    public readonly float value;
    public float Percent => (value / maxValue).Clamp(0, 1);

    public Health(float value, float maxValue) {
      this.value = value;
      this.maxValue = maxValue;
      if (value > maxValue) throw new Exception("Health cannot be greater than max");
    }
  }
}