using Code.Player.Input;
using Code.Utilities.ScreenWrap;
using Zenject;

namespace Code.Cowboy
{
    public class MoveHandler : ITickable
    {
        private readonly InputHandler inputHandler;
        private readonly ScreenWrappingRigidbody2D rigidbody;

        private MoveHandler(InputHandler inputHandler, ScreenWrappingRigidbody2D rigidbody)
        {
            this.inputHandler = inputHandler;
            this.rigidbody = rigidbody;
        }

        public void Tick()
        {
            if (!inputHandler.IsPossessed) return;
            Turn(inputHandler.Movement.x);
        }

        private void Turn(float amount)
        {
            var torque = 3 * -amount;
            rigidbody.AddTorque(torque);
        }
    }
}
