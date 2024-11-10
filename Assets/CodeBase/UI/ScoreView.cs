using System;
using CodeBase.Calculator;
using TMPro;
using UnityEngine;

namespace CodeBase.UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        private ScoreCalculator _scoreCalculator;
        private int _score;

        public void Init(ScoreCalculator scoreCalculator)
        {
            _scoreCalculator = scoreCalculator;
            _scoreCalculator.AddScore += AddNewScore;
        }
        
        private void AddNewScore(int score)
        {
            _score += score;
            _scoreText.text = _score.ToString();
        }

        private void OnDisable()
        {
            _scoreCalculator.AddScore += AddNewScore;
        }
    }
}