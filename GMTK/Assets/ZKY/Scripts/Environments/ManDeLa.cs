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
        [SerializeField] private bool _isInteract = false;
        [SerializeField] private string _previewsSceneName;
        [SerializeField] private string _musicName;
        [SerializeField] private float _fadeTime;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_playerTag) && !_isInteract)
            {
                _isPlayerIn = true;
                _UI.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(_playerTag) && !_isInteract)
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
                _isInteract = true;
                _UI.SetActive(false);
                SoundManager.instance.FadeVolumn(_previewsSceneName, 0, _fadeTime);
                Invoke(nameof(LoadNextMusic), _fadeTime);
                // TODO: Interact with ManDeLa`
            }
        }
        private void LoadNextMusic()
        {
            SoundManager.instance.Stop(_previewsSceneName);
            SoundManager.instance.ChangeVolumn(_musicName, 0);
            SoundManager.instance.Play(_musicName);
            SoundManager.instance.FadeVolumn(_musicName, 1, _fadeTime);
        }
    }
}
