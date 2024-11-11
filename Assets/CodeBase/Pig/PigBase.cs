using System;
using CodeBase.Calculator;
using UnityEngine;

namespace CodeBase.Pig
{
    public class PigBase : MonoBehaviour
    {
        [SerializeField] private GameObject _pigParent;
        private ScoreCalculator _scoreCalculator;
        private int _pigCount;
        
        public event Action PigEnded;

        public Pig[] Pigs { get; private set; }


        private void Awake()
        {
            Pigs = _pigParent.GetComponentsInChildren<Pig>();
            foreach (var pig in Pigs)
            {
                pig.Death += OnPigDeath;
            }
            _pigCount = Pigs.Length;
        }

        private void OnPigDeath()
        {
            _pigCount--;
            if (_pigCount <= 0)
            {
                PigEnded?.Invoke();
            }
        }
    }
}