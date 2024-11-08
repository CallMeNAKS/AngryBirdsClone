using CodeBase.Bird.BirdFactory;

namespace CodeBase.Level
{
    public class BirdCreator
    {
        public Bird.Bird[] CreateBirds(LevelData levelData, Slingshot.Slingshot slingshot)
        {
            var factory = new BirdFactory(levelData.Birds, slingshot);
            Bird.Bird[] createdBirds = factory.CreateBird();
            return createdBirds;
        }
    }
}