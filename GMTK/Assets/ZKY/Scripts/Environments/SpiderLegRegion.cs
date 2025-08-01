using UnityEngine;

namespace ZKY
{
    public class SpiderLegRegion : MonoBehaviour
    {
        [SerializeField] private MyEvents _hurtEvent;
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") || other.CompareTag("Dianzi"))
            {
                _hurtEvent.Invoke();
            }
        }
    }
}