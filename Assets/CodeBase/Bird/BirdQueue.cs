using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Bird
{
    public class BirdQueue
    {
        private Bird[] _birds;
        private Vector3 _birdPosition;
        private BirdTransfer _birdTransfer;
        
        private Queue<Bird> _birdQueue;
        private Vector3[] _birdsPositions;

        public BirdQueue(Bird[] birds, Vector3 birdPosition, BirdTransfer birdTransfer)
        {
            _birds = birds;
            _birdPosition = birdPosition;
            _birdTransfer = birdTransfer;
        }

        public void Place()
        {
            CreatePositionForAllBirds();
            _birdQueue = new Queue<Bird>(_birds);
        }

        private void CreatePositionForAllBirds()
        {
            _birdsPositions = new Vector3[_birds.Length];
            _birdsPositions[0] = _birdPosition;
            
            for (int i = 0; i < _birds.Length; i++)
            {
                _birdsPositions[i] = _birdPosition + new Vector3(0 + i, 0, 0);
                _birds[i].transform.position = _birdsPositions[i];
            }
        }

        public async UniTask<Bird> GetNextBird(Vector3 placeForBird)
        {
            if (_birdQueue.Count <= 0)
            {
                return null;
            }
            var nextBird = _birdQueue.Dequeue();
            
            ShiftBirdsPositions();
            await _birdTransfer.TransferBird(nextBird, placeForBird);
    
            return nextBird;
        }

        private void ShiftBirdsPositions()
        {
            int index = 0;
            foreach (var bird in _birdQueue)
            {
                _birdTransfer.TransferBird(bird, _birdsPositions[index]);
                index++;
            }
        }
    }
}