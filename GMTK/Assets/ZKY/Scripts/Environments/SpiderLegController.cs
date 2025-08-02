using UnityEngine;
using System.Collections.Generic;
namespace ZKY
{
    public class SpiderLegController : MonoBehaviour
    {
        [SerializeField] private List<Collider> _spiderLegRegions;
        [SerializeField] private MyEvents _spiderHurtEvent;
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
            }
        }
    }

}