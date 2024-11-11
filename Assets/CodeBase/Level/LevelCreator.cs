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
        private LevelData _currentLevelData;
        
        private ScoreView _scoreView = Resources.Load<ScoreView>("UI/ScoreUI");
        private PlayUI _playUI = Resources.Load<PlayUI>("UI/PlayUI");
        
        private LevelReseter _levelReseter;

        public LevelCreator(Transform slingshotPosition, Transform pigBasePosition, Transform birdPosition,  Slingshot.Slingshot slingshot)
        {
            _slingshotPosition = slingshotPosition;
            _pigBasePosition = pigBasePosition;
            _birdPosition = birdPosition;
            _slingshot = slingshot;
        }

        public void CreateLevel(LevelData levelData)
        {
            _currentLevelData = levelData;

            BuildLevel(levelData);
        }

        public void RestartLevel()
        {
            _levelReseter.Reset();
            BuildLevel(_currentLevelData);
        }

        private void BuildLevel(LevelData levelData)
        {
            var birds = CreateBirds(levelData);
            var birdsQueue = CreateBirdServices(birds);
            var slingshot = CreateSlingshot(birdsQueue);
            var pigBase = CreatePigBase(levelData);
            
            var scoreCalculator = CreateCalculator(birds, pigBase);

            var scoreView = CreateScoreUI(scoreCalculator);
            var playUI = CreatePlayUI();

            var gameLoop = CreateGameLoop(slingshot, pigBase);

            _levelReseter = new LevelReseter(birds, slingshot, pigBase, scoreView, playUI);
        }

        private PlayUI CreatePlayUI()
        {
            var playUI = GameObject.Instantiate(_playUI);
            playUI.Init(this);
            return playUI;
        }

        private GameLoop.GameLoop CreateGameLoop(Slingshot.Slingshot slingshot, PigBase pigBase)
        {
            var gameLoop = new GameLoop.GameLoop(slingshot, pigBase, this);
            gameLoop.Init();
            
            return gameLoop;
        }

        private DefaultScoreCalculator CreateCalculator(Bird.Bird[] birds, PigBase pigBase)
        {
            var scoreCalculator = new DefaultScoreCalculator(birds, pigBase.Pigs);
            scoreCalculator.Init();
            return scoreCalculator;
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

        private Slingshot.Slingshot CreateSlingshot(BirdQueue birdsQueue)
        {
            _slingshot.gameObject.SetActive(false); // Нормальное ли решение? 
            
            var slingshot = GameObject.Instantiate(_slingshot, _slingshotPosition);
            slingshot.Init(birdsQueue);
            slingshot.gameObject.SetActive(true);
            
            _slingshot.gameObject.SetActive(true);

            return slingshot;
        }

        private ScoreView CreateScoreUI(ScoreCalculator scoreCalculator)
        {
            var scoreView = GameObject.Instantiate(_scoreView);
            scoreView.Init(scoreCalculator);

            return scoreView;
        }
    }
}