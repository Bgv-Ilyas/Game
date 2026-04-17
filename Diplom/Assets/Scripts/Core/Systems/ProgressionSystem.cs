namespace RusRunner.Core.Systems
{
    public sealed class ProgressionSystem
    {
        private readonly float _baseSpeed;
        private readonly float _speedGainPerSecond;
        private readonly float _maxSpeed;
        private readonly float _difficultyRampPerSecond;

        public float Difficulty01 { get; private set; }

        public ProgressionSystem(float baseSpeed, float speedGainPerSecond, float maxSpeed, float difficultyRampPerSecond)
        {
            _baseSpeed = baseSpeed;
            _speedGainPerSecond = speedGainPerSecond;
            _maxSpeed = maxSpeed;
            _difficultyRampPerSecond = difficultyRampPerSecond;
            Difficulty01 = 0f;
        }

        public void Tick(GameContext context, float deltaTime)
        {
            if (context.CurrentSpeed < _baseSpeed)
            {
                context.CurrentSpeed = _baseSpeed;
            }

            var targetSpeed = context.CurrentSpeed + (_speedGainPerSecond * deltaTime);
            context.CurrentSpeed = targetSpeed > _maxSpeed ? _maxSpeed : targetSpeed;

            Difficulty01 += _difficultyRampPerSecond * deltaTime;
            if (Difficulty01 > 1f)
            {
                Difficulty01 = 1f;
            }
        }
    }
}
