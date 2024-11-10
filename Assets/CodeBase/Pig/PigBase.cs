using System;
using CodeBase.Calculator;
using UnityEngine;

namespace CodeBase.Pig
{
    public class PigBase : MonoBehaviour
    {
        [SerializeField] private GameObject _pigParent;
        private ScoreCalculator _scoreCalculator;

        public Pig[] Pigs { get; private set; }


        private void Awake()
        {
            Pigs = _pigParent.GetComponentsInChildren<Pig>();
        }
    }
}