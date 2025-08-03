using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        [SerializeField] private MyEvents _trapped;
        [SerializeField] private bool _isTrapped;

        private void OnEnable()
        {
            _trapped._event += OnTrapped;
        }
        private void OnDisable()
        {
            _trapped._event -= OnTrapped;
        }

        private void OnTrapped()
        {
            _isPushing = false;
            _textGO.SetActive(false);
            _canPush = false;
            GetComponent<Collider>().enabled = false;
            _isTrapped = true;
        }
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
                    _textGO.GetComponentInChildren<TextMeshProUGUI>().text = "Press Space to Stop Pushing";
                    _releventPos = _player.transform.position - transform.position;
                }
                else
                {
                    _textGO.GetComponentInChildren<TextMeshProUGUI>().text = "Press Space to Push";
                }
            }
            if (_isPushing && !_isTrapped)
            {
                transform.position = _player.transform.position - _releventPos;
            }
        }
    }
}
