using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.GameLoop
{
    public class LoseView : MonoBehaviour
    {
        private GameLoop _gameLoop;

        [SerializeField] private Button _restartButton;

        public void Init(GameLoop gameLoop)
        {
            _gameLoop = gameLoop;

            _restartButton.onClick.AddListener(Restart);
        }

        public void Restart()
        {
            _gameLoop.Restart();
        }
    }
}