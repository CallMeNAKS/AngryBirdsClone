using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Bird
{
    [Serializable]
    public class BirdTransfer
    {
        [SerializeField] private float _jumpPower = 1f;
        [SerializeField] private float _duration = 2f;
        
        public async UniTask TransferBird(Bird bird, Vector3 birdNewPlace)
        {
            await bird.transform.DOJump(
                birdNewPlace,
                _jumpPower,
                1,
                _duration
            )!.AsyncWaitForCompletion();
        }

        public void SwitchBirds()
        {
        }
    }
}