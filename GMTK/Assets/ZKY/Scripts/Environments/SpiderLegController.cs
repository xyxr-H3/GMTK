using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace ZKY
{
    public class SpiderLegController : MonoBehaviour
    {
        [SerializeField] private List<Collider> _spiderLegRegions;
        [SerializeField] private MyEvents _spiderHurtEvent;
        [SerializeField] private List<Material> _spiderLegMaterials;
        [SerializeField] private List<GameObject> _spiderLiegs;
        [SerializeField] private float _spiderDisappearTime = 1f;
        [SerializeField] private float _waitTime = 2f;

        private void Awake()
        {
            foreach (var mat in _spiderLegMaterials)
            {
                mat.SetFloat("_Intensity", 0);
            }
        }
        private void OnEnable()
        {
            _spiderHurtEvent._event += OnSpiderHurt;
        }

        private void OnDisable()
        {
            _spiderHurtEvent._event -= OnSpiderHurt;
        }

        private void OnSpiderHurt()
        {
            foreach (var item in _spiderLegRegions)
            {
                item.enabled = false;
                StartCoroutine(SpiderLegDisappear());
            }
        }

        private IEnumerator SpiderLegDisappear()
        {
            yield return new WaitForSeconds(_waitTime);
            float _timer = 0f;
            while (_timer < _spiderDisappearTime)
            {
                _timer += Time.deltaTime;
                foreach (var mat in _spiderLegMaterials)
                {
                    mat.SetFloat("_Intensity", Mathf.Lerp(0f, 1f, _timer / _spiderDisappearTime));
                }
                yield return null;
            }
            foreach (var leg in _spiderLiegs)
            {
                leg.SetActive(false);
            }
        }
    }
}