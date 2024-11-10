using CodeBase.InteractionCore;
using CodeBase.Level;
using CodeBase.Slingshot;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    private Interactor _interactor;

    [SerializeField] private Transform _slingshotPosition;
    [SerializeField] private Transform _pigBasePosition;
    [SerializeField] private Transform _birdPosition;
    
    [SerializeField] private Slingshot _slingshot;
    
    [SerializeField] private LevelData _LevelData;
    

    private void Awake()
    {
        _interactor = new Interactor();
        _interactor.Init();

        CreateLevel();
    }

    private void CreateLevel()
    {
        var level = new LevelCreator(_slingshotPosition, _pigBasePosition, _birdPosition, _slingshot);
        level.CreateLevel(_LevelData);
    }
}