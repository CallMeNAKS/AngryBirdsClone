using System;
using CodeBase.Bird;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Slingshot
{
    public class Slingshot : MonoBehaviour
    {
        [SerializeField] private float _power = 30;
        private BirdQueue _birdQueue;
        private Bird.Bird _bird;

        [Header("Dependencies")] [SerializeField]
        private ShootPoint _shotPoint;

        public event Action BirdsEnded;

        private async void Awake()
        {
            await GetBird();
        }
        
        public void Init(BirdQueue birdQueue)
        {
            _birdQueue = birdQueue;
        }

        private async UniTask GetBird()
        {
            _bird = await _birdQueue.GetNextBird(_shotPoint.transform.position);
            if (_bird == null)
            {
                BirdsEnded?.Invoke();
            }
            await WaitShot();
        }

        private async UniTask WaitShot()
        {
            var done = false;
        
            void Shot(Vector3 direction)
            {
                done = true;
                _bird.Launch(-direction * _power);
            }
        
            _shotPoint.Release += Shot;
            
            while (done == false)
            {
                _bird.transform.position = _shotPoint.transform.position;
                await UniTask.Yield();
            }
        
            _shotPoint.Release -= Shot;
            
            // await 
                GetBird();
        }
    }
}