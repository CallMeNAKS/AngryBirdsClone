using CodeBase.Pig;
using UnityEngine;

namespace CodeBase.Level
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Level/LevelData")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private Bird.Bird[] _birds;
        [SerializeField] private PigBase _pigBasePrefab;
        
        public Bird.Bird[] Birds => _birds;
        public PigBase PigBasePrefab => _pigBasePrefab;
    }
}