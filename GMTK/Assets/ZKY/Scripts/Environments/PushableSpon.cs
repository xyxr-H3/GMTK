using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZKY
{
    public class PushableSpon : MonoBehaviour
    {
        [SerializeField] private string _playerTag;
        [SerializeField] private bool _canPush;
        [SerializeField] private bool _isPushing;
        [SerializeField] private GameObject _textGO;
        [SerializeField] private KeyCode _pushKey = KeyCode.Space;
        [SerializeField] private Vector3 _releventPos;
        [SerializeField] private GameObject _player;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_playerTag))
            {
                _player = other.gameObject;
                _textGO.SetActive(true);
                _canPush = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(_playerTag))
            {
                _textGO.SetActive(false);
                _canPush = false;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(_pushKey))
            {
                _isPushing = !_isPushing;
                if (_isPushing)
                {
                    _releventPos = _player.transform.position - transform.position;
                }
            }
            if (_isPushing)
            {
                transform.position = _player.transform.position - _releventPos;
            }
        }
    }
}
