using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZKY
{
    public class ManDeLa : MonoBehaviour
    {
        [SerializeField] private GameObject _UI;
        [SerializeField] private bool _isPlayerIn;
        [SerializeField] private KeyCode _interactKeyCode;
        [SerializeField] private string _playerTag;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_playerTag))
            {
                _isPlayerIn = true;
                _UI.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(_playerTag))
            {
                _isPlayerIn = false;
                _UI.SetActive(false);
            }
        }

        private void Update()
        {
            if (_isPlayerIn && Input.GetKeyDown(_interactKeyCode))
            {
                Debug.Log("Interact with ManDeLa");
                // TODO: Interact with ManDeLa`
            }
        }
    }
}
