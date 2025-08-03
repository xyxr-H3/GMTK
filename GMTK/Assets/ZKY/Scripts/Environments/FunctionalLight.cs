using UnityEngine;

namespace ZKY
{
    public class FunctionalLight : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private MyEvents _hurtEvent;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_tag))
            {
                _hurtEvent?.Invoke();
            }
        }
    }
}