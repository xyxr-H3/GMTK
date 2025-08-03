using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace ZKY
{
    public class BloomAnimation : MonoBehaviour
    {
        [SerializeField] private float _minThreathold = 1f;
        [SerializeField] private float _maxThreshold = 0.7f;
        [SerializeField] private float _minIntensity = 2f;
        [SerializeField] private float _maxIntensity = 5f;
        [SerializeField] private float _duration;
        [SerializeField] private bool _isIncreasing;
        private float _timer;
        private Bloom bloom;
        private void Awake()
        {
            bloom = GetComponent<PostProcessVolume>().profile.GetSetting<Bloom>();
            bloom.enabled.value = true;
            bloom.threshold.value = _minThreathold;
            bloom.intensity.value = _minIntensity;
        }

        private void FixedUpdate()
        {
            _timer += Time.fixedDeltaTime;
            if (_isIncreasing)
            {
                bloom.threshold.value = Mathf.Lerp(_minThreathold, _maxThreshold, _timer / _duration);
                bloom.intensity.value = Mathf.Lerp(_minIntensity, _maxIntensity, _timer / _duration);
            }
            else
            {
                bloom.threshold.value = Mathf.Lerp(_maxThreshold, _minThreathold, _timer / _duration);
                bloom.intensity.value = Mathf.Lerp(_maxIntensity, _minIntensity, _timer / _duration);
            }
            if (_timer >= _duration)
            {
                _isIncreasing = !_isIncreasing;
                _timer = 0f;
            }
        }

    }
}
