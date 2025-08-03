using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZKY
{
    public class ShowInfo : MonoBehaviour
    {
        [SerializeField] private GameObject _infoPanel;
        [SerializeField] private string _playerTag = "Player";
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_playerTag))
            {
                _infoPanel.SetActive(true);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(_playerTag))
            {
                _infoPanel.SetActive(false);
            }
        }
    }
}
