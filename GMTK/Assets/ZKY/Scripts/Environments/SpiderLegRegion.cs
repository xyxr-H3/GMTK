using UnityEngine;

namespace ZKY
{
    public class SpiderLegRegion : MonoBehaviour
    {
        [SerializeField] private MyEvents _hurtEvent;
        [SerializeField] private MyEvents _spiderHurtEvent;
        [SerializeField] private Animator _animator;
        [SerializeField] private SpiderLegAnim _spiderLegAnim;
        private void Awake()
        {
            _spiderLegAnim = GetComponentInChildren<SpiderLegAnim>();
        }
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _hurtEvent.Invoke();
                _spiderLegAnim.StickPlayer();
            }
            if (other.CompareTag("Spon"))
            {
                _spiderHurtEvent.Invoke();
                _spiderLegAnim.GetHurt();
            }
        }
    }
}