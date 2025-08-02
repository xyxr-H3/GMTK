using UnityEngine;

namespace ZKY
{
    public class SpiderLegAnim : MonoBehaviour
    {
        [SerializeField] private Animator _animator;


        public void StickPlayer()
        {
            _animator.SetTrigger("StickPlayer");
        }

        public void GetHurt()
        {
            _animator.SetTrigger("GetHurt");
        }
    }
}