using System;
using CodeBase.BaseMaterial;

namespace CodeBase.Calculator
{
    public abstract class ScoreCalculator
    {
        private Bird.Bird[] _birds;
        private Pig.Pig[] _pigs;
        
        public event Action<int> AddScore;

        public ScoreCalculator(Bird.Bird[] birds, Pig.Pig[] pigs)
        {
            _birds = birds;
            _pigs = pigs;
        }

        public void Init()
        {
            foreach (var bird in _birds)
            {
                bird.CollisionNotifier.OnBirdCollision += CalculateBirdsColisionScore;
            }

            foreach (var pig in _pigs)
            {
                pig.Death += AddScoreByPig;
            }
        }

        public abstract void CalculateBirdsColisionScore(BasePigMaterial material, float force);
        public abstract void AddScoreByPig();

        protected void OnAddScore(int score)
        {
            AddScore?.Invoke(score);
        }
    }
}