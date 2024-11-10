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

        public GameLoop(Slingshot.Slingshot slingshot, PigBase pigBase)
        {
            _slingshot = slingshot;
            _pigBase = pigBase;
        }

        public void Init()
        {
            _slingshot.BirdsEnded += OnBirdsEnded;
        }

        private void OnBirdsEnded()
        {
            GameObject.Instantiate(_loseView);
        }

        private void OnPigEnded()
        {
            GameObject.Instantiate(_winView);
        }
    }
}