using HexenSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameSystem.Views
{
    public class CardView : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Sprite sprite;

        [SerializeField] private GameObject _staticCard;
        [SerializeField] private LayerMask _dropLayer;

        private Camera _camera;
        private RectTransform _rectTransform;
        private Vector3 _startPosition;
        private DeckView _deckView;
        private GameObject _lastObjectBelowMouse;

        public int CardNumber { get; set; }

        private void Awake()
        {
            _camera = Camera.main;
            _rectTransform = GetComponent<RectTransform>();
            _startPosition = _rectTransform.localPosition;
            _deckView = transform.parent.parent.GetComponent<DeckView>();
        }

        private void OnValidate()
        {
            GetComponent<Image>().sprite = sprite;
            _staticCard.GetComponent<Image>().sprite = sprite;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.position = eventData.position;

            if (TryGetGameObjectBelowMouse(out var gameObject)
                && gameObject != _lastObjectBelowMouse)
            {
                _lastObjectBelowMouse = gameObject;
                TileView tile = gameObject.GetComponent<TileView>();
                _deckView.CardDragged(CardNumber, tile.HexPosition);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _rectTransform.localPosition = _startPosition;

            if (_lastObjectBelowMouse != null)
            {
                TileView tile = _lastObjectBelowMouse.GetComponent<TileView>();
                _deckView.CardDropped(CardNumber, tile.HexPosition);
            }
        }

        private bool TryGetGameObjectBelowMouse(out GameObject gameObject)
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, _camera.farClipPlane, _dropLayer))
            {
                gameObject = hitInfo.collider.gameObject;
                return true;
            }

            gameObject = null;
            return false;
        }
    }
}
