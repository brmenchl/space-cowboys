namespace Code.Player.Input {
  public interface IPossessable {
    void Possess(Pawn pawn);

    void Depossess();
  }
}