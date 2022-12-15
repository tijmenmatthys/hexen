using HexenSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.Views
{
    public class PieceView : MonoBehaviour, IPiece
    {
        [SerializeField] private PlayerType _playerType;
        [SerializeField] private Material _playerMaterial;
        [SerializeField] private Material _enemyMaterial;

        public PlayerType PlayerType => _playerType;

        public Vector3 WorldPosition => transform.position;

        private void OnValidate()
        {
            if (_playerType == PlayerType.Player) GetComponentInChildren<Renderer>().material = _playerMaterial;
            if (_playerType == PlayerType.Enemy) GetComponentInChildren<Renderer>().material = _enemyMaterial;
        }

        public void Placed(Vector3 worldPosition)
        {
            transform.position = worldPosition;
            gameObject.SetActive(true);
        }
        public void Moved(Vector3 fromWorldPosition, Vector3 toWorldPosition)
        {
            transform.position = toWorldPosition;
        }
        public void Taken()
        {
            gameObject.SetActive(false);
        }
    }
}
