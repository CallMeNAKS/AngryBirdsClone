using System;
using System.Collections.Generic;
using System.Reflection;

namespace CodeBase.InteractionCore
{
    public class Interactor
    {
        private readonly List<BaseInteraction> _all = new();
        private readonly List<IOnGameLoad> _loadInteractions = new();
        private readonly List<IOnMenu> _menuInteractions = new();
        private readonly List<IOnPlay> _playInteractions = new();

        public void Init()
        {
            var allTypes = FindAllSubclasses<BaseInteraction>();
            foreach (var type in allTypes)
            {
                var instance = Activator.CreateInstance(type) as BaseInteraction;
                _all.Add(instance);

                if (instance is IOnGameLoad gameLoadInteraction)
                    _loadInteractions.Add(gameLoadInteraction);

                if (instance is IOnMenu startGameInteraction)
                    _menuInteractions.Add(startGameInteraction);

                if (instance is IOnPlay shopStateInteraction)
                    _playInteractions.Add(shopStateInteraction);
            }
        }

        private List<Type> FindAllSubclasses<T>()
        {
            var result = new List<Type>();
            foreach (var type in Assembly.GetAssembly(typeof(T)).GetTypes())
            {
                if (type.IsSubclassOf(typeof(T)) && !type.IsAbstract)
                {
                    result.Add(type);
                }
            }

            return result;
        }

        public void TriggerGameLoad()
        {
            foreach (var interaction in _loadInteractions)
            {
                interaction.OnGameLoad();
            }
        }

        public void TriggerStartGame()
        {
            foreach (var interaction in _menuInteractions)
            {
                interaction.OnMenu();
            }
        }

        public void TriggerShopState()
        {
            foreach (var interaction in _playInteractions)
            {
                interaction.OnPlay();
            }
        }
    }
}