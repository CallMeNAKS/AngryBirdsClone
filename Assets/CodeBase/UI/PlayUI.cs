using CodeBase.Level;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class PlayUI : MonoBehaviour
    {
        private LevelCreator _levelCreator;
        
        [SerializeField] private Button _restartButton;

        public void Init(LevelCreator levelCreator)
        {
            _levelCreator = levelCreator;
            
            _restartButton.onClick.AddListener(Restart);
        }
        
        public void Restart()
        {
            _levelCreator.RestartLevel();
        }
    }
}