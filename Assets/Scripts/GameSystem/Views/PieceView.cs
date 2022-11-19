using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.Views
{
    public class PieceView : MonoBehaviour
    {
        public Vector3 WorldPosition => transform.position;

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
