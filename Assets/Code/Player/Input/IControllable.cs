namespace Code.Player.Input {
  public interface IControllable {
    void Thrust(float amount);
    void Turn(float amount);

    void Shoot();
  }
}