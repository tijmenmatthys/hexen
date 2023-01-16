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
    public class MainMenuState : State<GameStateType>
    {
        private const string SceneName = "MainMenu";

        private MainMenuView _mainMenuView;

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
            _mainMenuView.PlayClicked += OnPlayClicked;

        public override void OnSuspend() =>
            _mainMenuView.PlayClicked -= OnPlayClicked;

        private void InitMenu()
        {
            _mainMenuView = GameObject.FindObjectOfType<MainMenuView>();
        }

        private void OnPlayClicked(object sender, EventArgs e)
        {
            StateMachine.Transition(GameStateType.Play, true);
        }
    }
}
