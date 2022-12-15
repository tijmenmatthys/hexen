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
        [SerializeField] private GameObject _staticCard;
        [SerializeField] private LayerMask _dropLayer;

        [SerializeField] private List<Sprite> _sprites;
        [SerializeField] private List<CardType> _types;

        private Camera _camera;
        private RectTransform _rectTransform;
        private Vector3 _startPosition;
        private DeckView _deckView;
        private GameObject _lastObjectBelowMouse;
        private CardType _type;

        public int Index { get; set; }
        public CardType Type
        {
            get { return _type; }
            set
            {
                _type = value;
                Sprite sprite = _sprites[_types.IndexOf(value)];
                GetComponent<Image>().sprite = sprite;
                _staticCard.GetComponent<Image>().sprite = sprite;
            }
        }

        private void Awake()
        {
            _camera = Camera.main;
            _rectTransform = GetComponent<RectTransform>();
            _startPosition = _rectTransform.localPosition;
            _deckView = transform.parent.parent.GetComponent<DeckView>();
        }

        public void Hide(bool hide)
        {
            if (hide) _staticCard.SetActive(false);
            else _staticCard.SetActive(true);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.position = eventData.position;

            if (TryGetGameObjectBelowMouse(out var currentObjectBelowMouse)
                && currentObjectBelowMouse != _lastObjectBelowMouse)
            {
                _lastObjectBelowMouse = currentObjectBelowMouse;
                TileView tile = currentObjectBelowMouse.GetComponent<TileView>();
                _deckView.CardDragged(Index, Type, tile.HexPosition);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _rectTransform.localPosition = _startPosition;

            if (_lastObjectBelowMouse != null)
            {
                TileView tile = _lastObjectBelowMouse.GetComponent<TileView>();
                _deckView.CardDropped(Index, Type, tile.HexPosition);
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
