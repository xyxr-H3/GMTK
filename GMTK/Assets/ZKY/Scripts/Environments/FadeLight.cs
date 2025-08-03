using UnityEngine;

namespace ZKY
{
    public class FadeLight : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private MyEvents _hurtEvent;
        [SerializeField] private float _fadeTime = 1f;
        [SerializeField] private float _keepFadeTime = 1f;
        [SerializeField] private float _timer = 0f;
        [SerializeField] private bool _isFading = false;
        [SerializeField] private bool _isWaitting = false;
        [SerializeField] private float _rongCuo = 0.1f;
        [SerializeField] private Material _lightMat;

        private Collider _collider;
        private void Awake()
        {
            _collider = GetComponent<Collider>();
            if (_collider == null)
            {
                Debug.LogError("Collider not found on FadeLight object.");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_tag))
            {
                _hurtEvent?.Invoke();
            }
        }

        private void FixedUpdate()
        {
            _timer += Time.fixedDeltaTime;
            if (_isWaitting)
            {
                if (_timer >= _keepFadeTime)
                {
                    _timer = 0f;
                    _isWaitting = false;
                }
            }
            else
            {
                if (_isFading)
                {
                    _lightMat.SetFloat("_Alpha", Mathf.Lerp(1f, 0f, _timer / _fadeTime));
                    if (_timer >= _fadeTime - _rongCuo)
                    {
                        _collider.enabled = false;
                    }
                    if (_timer >= _fadeTime)
                    {
                        _timer = 0f;
                        _isFading = false;
                        _isWaitting = true;
                    }
                }
                else
                {
                    _lightMat.SetFloat("_Alpha", Mathf.Lerp(0f, 1f, _timer / _fadeTime));
                    if (_timer >= _rongCuo)
                    {
                        _collider.enabled = true;
                    }
                    if (_timer >= _fadeTime)
                    {
                        _timer = 0f;
                        _isFading = true;
                        _isWaitting = true;
                    }
                }
            }

        }
    }
}