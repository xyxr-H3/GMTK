using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ZKY
{
    public class DeadScene : MonoBehaviour
    {
        private Motion _motion;
        [SerializeField] private float _waitTime;
        private float _speed;
        // [SerializeField] private float _fadeDuration = 0.5f;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private MyEvents _deadEvent;
        private void Awake()
        {
            _motion = FindAnyObjectByType<Motion>();
            _speed = _motion.speed;
        }

        private void OnEnable()
        {
            _deadEvent._event += OnDead;
        }

        private void OnDisable()
        {
            _deadEvent._event -= OnDead;
        }

        public void OnDead()
        {
            StartCoroutine(OnShow());
        }

        private IEnumerator OnShow()
        {
            _motion.speed = 0;
            yield return new WaitForSeconds(0.5f);
            _canvas.gameObject.SetActive(true);
            yield return new WaitForSeconds(_waitTime);
            while (true)
            {
                yield return null;
                if (Input.anyKeyDown)
                {
                    _motion.speed = _speed;
                    _canvas.gameObject.SetActive(false);
                    yield break;
                }
            }
        }
    }
}
