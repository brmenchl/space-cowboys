namespace Code.Input {
  public interface IControllable {
    void Thrust(float amount);
    void Turn(float amount);

    void Shoot();
    void Alt();
  }
}