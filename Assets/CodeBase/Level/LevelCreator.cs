using CodeBase.Bird;
using UnityEngine;

namespace CodeBase.Level
{
    public class LevelCreator
    {
        private Transform _slingshotPosition;
        private Transform _pigBasePosition;
        private Slingshot.Slingshot _slingshot;

        public LevelCreator(Transform slingshotPosition, Transform pigBasePosition, Slingshot.Slingshot slingshot)
        {
            _slingshotPosition = slingshotPosition;
            _pigBasePosition = pigBasePosition;
            _slingshot = slingshot;
        }

        public void CreateLevel(LevelData levelData)
        {
            var slingshot = GameObject.Instantiate(_slingshot, _slingshotPosition);

            var birds = CreateBirds(levelData);
        }

        private Bird.Bird[] CreateBirds(LevelData levelData)
        {
            var birdCreator = new BirdCreator();
            var birds = birdCreator.CreateBirds(levelData, _slingshot);
            return birds;
        }
    }
}