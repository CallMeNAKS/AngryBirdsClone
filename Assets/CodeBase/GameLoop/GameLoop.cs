using CodeBase.Level;
using CodeBase.Pig;
using UnityEngine;

namespace CodeBase.GameLoop
{
    public class GameLoop
    {
        private LoseView _loseView = Resources.Load<LoseView>("UI/LoseUI");
        private WinView _winView = Resources.Load<WinView>("UI/WinUI");

        private Slingshot.Slingshot _slingshot;
        private PigBase _pigBase;
        private LevelCreator _levelCreator;
        
        private LoseView _loseViewInstance;
        private WinView _winViewInstance;

        public GameLoop(Slingshot.Slingshot slingshot, PigBase pigBase, LevelCreator levelCreator)
        {
            _slingshot = slingshot;
            _pigBase = pigBase;
            _levelCreator = levelCreator;
        }

        public void Init()
        {
            _slingshot.BirdsEnded += OnBirdsEnded;
            _pigBase.PigEnded += OnPigEnded;
        }

        private void OnBirdsEnded()
        {
            _loseViewInstance = Object.Instantiate(_loseView);
            _loseView.Init(this);
        }

        private void OnPigEnded()
        {
            _slingshot.BirdsEnded -= OnBirdsEnded;
            _winViewInstance = Object.Instantiate(_winView);
            _winView.Init(_levelCreator);
        }

        public void Restart()
        {
            _levelCreator.RestartLevel();
            
            Object.Destroy(_loseViewInstance);
            Object.Destroy(_winViewInstance);
        }
    }
}