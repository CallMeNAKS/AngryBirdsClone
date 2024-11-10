using UnityEngine;

namespace CodeBase.Bird.BirdFactory
{
    public class BirdFactory
    {
        private Bird[] _birds;
        private Slingshot.Slingshot _slingshot;

        public BirdFactory(Bird[] birds, Slingshot.Slingshot slingshot)
        {
            _birds = birds;
            _slingshot = slingshot;
        }

        public Bird[] CreateBird()
        {
            var birds = new Bird[_birds.Length];
    
            for (int i = 0; i < _birds.Length; i++)
            {
                var createdBird = GameObject.Instantiate(_birds[i]);
                birds[i] = createdBird;
            }

            return birds;
        }
    }
}