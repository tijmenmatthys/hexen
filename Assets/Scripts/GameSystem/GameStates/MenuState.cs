using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystem.GameStates
{
    public class MenuState : State<GameStateType>
    {
        private const string SceneName = "Menu";

        private MenuView _menuView;

        public override void OnEnter()
        {
            var sceneLoad = SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive);
            sceneLoad.completed += op =>
            {
                InitMenu();
                OnResume();
            };
        }

        public override void OnExit()
        {
            OnSuspend();
            SceneManager.UnloadSceneAsync(SceneName);
        }

        public override void OnResume() =>
            _menuView.PlayClicked += OnPlayClicked;

        public override void OnSuspend() =>
            _menuView.PlayClicked -= OnPlayClicked;

        private void InitMenu()
        {
            _menuView = GameObject.FindObjectOfType<MenuView>();
        }

        private void OnPlayClicked(object sender, EventArgs e)
        {
            StateMachine.Transition(GameStateType.Play, true);
        }
    }
}
