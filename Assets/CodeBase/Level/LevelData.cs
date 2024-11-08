using UnityEngine;

namespace CodeBase.Level
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Level/LevelData")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private Bird.Bird[] _birds;
        [SerializeField] private GameObject _pigBasePrefab;
        
        public Bird.Bird[] Birds => _birds;
        public GameObject PigBasePrefab => _pigBasePrefab;
    }
}