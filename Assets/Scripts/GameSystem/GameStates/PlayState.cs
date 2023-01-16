using BoardSystem;
using GameSystem.Views;
using Helper;
using HexenSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystem.GameStates
{
    public class PlayState : State<GameStateType>
    {
        private const string SceneName = "Game";

        private BoardView _boardView;
        private DeckView _deckView;
        private PlayMenuView _playMenuView;

        private Board<PieceView> _board;
        private Deck<CardType> _deck;
        private Engine<PieceView> _engine;

        private bool _showMenuAfterLoad = false;

        public PlayState(bool showMenuAfterFirstLoad = false)
        {
            _showMenuAfterLoad = showMenuAfterFirstLoad;
        }

        public override void OnEnter()
        {
            var sceneLoading = SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive);
            sceneLoading.completed += (op) =>
            {
                InitViews();
                SubscribeViewEvents();
                InitModels();
                if (_showMenuAfterLoad)
                {
                    _showMenuAfterLoad = false;
                    StateMachine.Transition(GameStateType.MainMenu);
                }
            };
        }

        public override void OnExit()
        {
            UnsubscribeViewEvents();
            SceneManager.UnloadSceneAsync(SceneName);
        }

        public override void OnResume()
        {
            SubscribeViewEvents();
            _deckView.gameObject.SetActive(true);
        }

        public override void OnSuspend()
        {
            UnsubscribeViewEvents();
            _deckView.gameObject.SetActive(false);
        }

        private void CardDropped(object sender, CardViewEventArgs e)
        {
            Debug.Log($"Card {e.Type} dropped over {e.Position}");

            if (_engine.PlayCard(e.Position, e.Type))
                _deck.RemoveCard(e.Index);

            _boardView.DeHighlightTiles();
        }

        private void CardDragged(object sender, CardViewEventArgs e)
        {
            Debug.Log($"Card {e.Type} dragged over {e.Position}");

            CardType cardType = e.Type;
            Hex hoverPosition = e.Position;
            if (!_engine.TryGetPlayerPosition(out Hex playerPosition)) return;

            var validDropPositions = _engine.MoveSets[cardType].GetValidDropPositions(playerPosition);
            if (!validDropPositions.Contains(hoverPosition))
                _boardView.HighlightValidDropTiles(validDropPositions);
            else
            {
                var influencePositions = _engine.MoveSets[cardType].GetInfluencePositions(playerPosition, hoverPosition);
                _boardView.HighlightInfluenceTiles(influencePositions);
            }
        }

        private void BoardClicked(object sender, BoardClickEventArgs e)
        {
            // not needed for Hexen, but keep it in case we need it later
        }

        private void OnMainMenuClicked(object sender, EventArgs e)
        {
            StateMachine.Transition(GameStateType.MainMenu);
        }

        private void SubscribeViewEvents()
        {
            _boardView.Click += BoardClicked;
            _deckView.CardDrag += CardDragged;
            _deckView.CardDrop += CardDropped;
            _playMenuView.MainMenuClicked += OnMainMenuClicked;
        }

        private void UnsubscribeViewEvents()
        {
            _boardView.Click -= BoardClicked;
            _deckView.CardDrag -= CardDragged;
            _deckView.CardDrop -= CardDropped;
            _playMenuView.MainMenuClicked -= OnMainMenuClicked;
        }

        private void InitViews()
        {
            _boardView = GameObject.FindObjectOfType<BoardView>();
            _deckView = GameObject.FindObjectOfType<DeckView>();
            _playMenuView = GameObject.FindObjectOfType<PlayMenuView>();
        }

        private void InitModels()
        {
            // Create Board model & subscribe to events
            _board = new Board<PieceView>(_boardView.Size);
            _board.PiecePlaced += (s, e)
                => e.Piece.Placed(e.Position.WorldPosition);
            _board.PieceMoved += (s, e)
                => e.Piece.Moved(e.FromPosition.WorldPosition, e.ToPosition.WorldPosition);
            _board.PieceTaken += (s, e)
                => e.Piece.Taken();

            // Link PieceViews to the board model
            var pieceViews = GameObject.FindObjectsOfType<PieceView>();
            foreach (var pieceView in pieceViews)
                _board.Place(Hex.HexPosition(pieceView.WorldPosition), pieceView);

            // Create Deck model & subscribe to events
            _deck = new Deck<CardType>(_deckView.Size);
            _deck.Change += _deckView.OnDeckChanged;
            foreach (CardType card in _deckView.StartingCards)
                _deck.AddCard(card);

            // Create Engine
            _engine = new Engine<PieceView>(_board);
        }
    }
}
