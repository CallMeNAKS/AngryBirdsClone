using System;
using CodeBase.Bird;
using CodeBase.Calculator;
using CodeBase.Pig;
using CodeBase.UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Level
{
    public class LevelCreator
    {
        private Transform _slingshotPosition;
        private Transform _pigBasePosition;
        private Transform _birdPosition;
        private Slingshot.Slingshot _slingshot;
        
        private ScoreView _scoreView = Resources.Load<ScoreView>("UI/ScoreUI");

        public LevelCreator(Transform slingshotPosition, Transform pigBasePosition, Transform birdPosition,  Slingshot.Slingshot slingshot)
        {
            _slingshotPosition = slingshotPosition;
            _pigBasePosition = pigBasePosition;
            _birdPosition = birdPosition;
            _slingshot = slingshot;
        }

        public void CreateLevel(LevelData levelData)
        {
            var birds = CreateBirds(levelData);
            var birdsQueue = CreateBirdServices(birds);
            CreateSlingshot(birdsQueue);
            var pigBase = CreatePigBase(levelData);

            var scoreCalculator = new DefaultScoreCalculator(birds, pigBase.Pigs);
            scoreCalculator.Init();

            CreateUI(scoreCalculator);
        }

        private PigBase CreatePigBase(LevelData levelData)
        {
            PigBase pigBase = Object.Instantiate(levelData.PigBasePrefab, _pigBasePosition);
            return pigBase;
        }

        private BirdQueue CreateBirdServices(Bird.Bird[] birds)
        {
            var birdTransfer = new BirdTransfer();
            var birdQueue = new BirdQueue(birds, _birdPosition.position, birdTransfer);
            birdQueue.Place();
            return birdQueue;
        }

        private Bird.Bird[] CreateBirds(LevelData levelData)
        {
            var birdCreator = new BirdCreator();
            var birds = birdCreator.CreateBirds(levelData, _slingshot);

            return birds;
        }

        private void CreateSlingshot(BirdQueue birdsQueue)
        {
            _slingshot.gameObject.SetActive(false); // Нормальное ли решение? 
            
            var slingshot = GameObject.Instantiate(_slingshot, _slingshotPosition);
            slingshot.Init(birdsQueue);
            slingshot.gameObject.SetActive(true);
            
            _slingshot.gameObject.SetActive(true);
        }

        private void CreateUI(ScoreCalculator scoreCalculator)
        {
            var scoreView = GameObject.Instantiate(_scoreView);
            scoreView.Init(scoreCalculator);
        }
    }
}