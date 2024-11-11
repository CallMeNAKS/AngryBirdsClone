using CodeBase.Bird;
using CodeBase.Calculator;
using CodeBase.Pig;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Level
{
    public class LevelReseter
    {
        private Bird.Bird[] _birds;
        private Slingshot.Slingshot _slingshot;
        private PigBase _pigBase;
        private ScoreView _scoreView;
        private PlayUI _playUI;
        private GameLoop.GameLoop _gameLoop;

        public LevelReseter(
            Bird.Bird[] birds,
            Slingshot.Slingshot slingshot,
            PigBase pigBase,
            ScoreView scoreView,
            PlayUI playUI)
        {
            _birds = birds;
            _slingshot = slingshot;
            _pigBase = pigBase;
            _scoreView = scoreView;
            _playUI = playUI;
        }

        public void Reset()
        {
            foreach (Bird.Bird bird in _birds)
                GameObject.Destroy(bird.gameObject);
            
            GameObject.Destroy(_slingshot.gameObject);
            GameObject.Destroy(_pigBase.gameObject);
            GameObject.Destroy(_scoreView.gameObject);
            GameObject.Destroy(_playUI.gameObject);
        }
    }
}