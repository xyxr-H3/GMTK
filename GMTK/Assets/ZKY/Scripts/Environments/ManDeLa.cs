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
        [SerializeField] private float _waitReadTime;
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _chaseLight;
        [SerializeField] private MyEvents _getMandera;
        [SerializeField] private GameObject _giftItem;
        [SerializeField] private GetItemData _getItemData;

        private void Awake()
        {
            _animator = GetComponent<Animator>();

        }

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
                _getItemData.isGet = true;
                Debug.Log("Interact with ManDeLa");
                _isInteract = true;
                _UI.SetActive(false);
                _giftItem.SetActive(true);
                _getMandera.Invoke();
                StartCoroutine(changeMusic());
                _animator.SetTrigger("Pull");
            }
        }

        private IEnumerator changeMusic()
        {
            SoundManager.instance.FadeVolumn(_previewsSceneName, 0, _fadeTime);
            yield return new WaitForSeconds(_fadeTime);
            SoundManager.instance.Stop(_previewsSceneName);
            yield return new WaitForSeconds(_waitReadTime);
            _chaseLight.SetActive(true);
            var fadelights = FindObjectsByType<FadeLight>(sortMode: FindObjectsSortMode.None);
            var spinlights = FindObjectsByType<SpineLight>(sortMode: FindObjectsSortMode.None);

            foreach (var light in fadelights)
            {
                light.gameObject.SetActive(false);
            }
            foreach (var light in spinlights)
            {
                light.gameObject.SetActive(false);
            }
            SoundManager.instance.ChangeVolumn(_musicName, 0);
            SoundManager.instance.Play(_musicName);
            SoundManager.instance.FadeVolumn(_musicName, 1, _fadeTime);
        }
    }
}
