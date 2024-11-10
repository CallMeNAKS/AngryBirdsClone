using CodeBase.BaseMaterial;

namespace CodeBase.Calculator
{
    public class DefaultScoreCalculator : ScoreCalculator
    {
        public DefaultScoreCalculator(Bird.Bird[] birds, Pig.Pig[] pigs) : base(birds, pigs)
        {
        }

        public override void CalculateBirdsColisionScore(BasePigMaterial material, float force)
        {
            int score = (int)(10 * force);
            OnAddScore(score);
        }

        public override void AddScoreByPig()
        {
            OnAddScore(5000);
        }
    }
}