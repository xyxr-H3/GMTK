using UnityEngine;
using UnityEngine.Events;
namespace ZKY
{
    public class BaseLight : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private MyEvents _hurtEvent;
        [Header("Movement")]
        [SerializeField] private Vector3 _Pos1;
        [SerializeField] private Vector3 _Pos2;
        [SerializeField] private float _moveTime;
        [SerializeField] private float _timer;
        [SerializeField] private bool _isMovingToPos1;

        private void FixedUpdate()
        {
            if (_isMovingToPos1)
            {
                _timer += Time.fixedDeltaTime;
                transform.position = Vector3.Lerp(_Pos2, _Pos1, _timer / _moveTime);
                if (_timer >= _moveTime)
                {
                    _timer = 0;
                    _isMovingToPos1 = false;
                }
            }
            else
            {
                _timer += Time.fixedDeltaTime;
                transform.position = Vector3.Lerp(_Pos1, _Pos2, _timer / _moveTime);
                if (_timer >= _moveTime)
                {
                    _timer = 0;
                    _isMovingToPos1 = true;
                }
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_tag))
            {
                if (_hurtEvent == null)
                {
                    Debug.LogWarning("Hurt event is not assigned in " + gameObject.name);
                    return;
                }
                _hurtEvent.Invoke();
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_Pos1, 0.5f);
            Gizmos.DrawSphere(_Pos2, 0.5f);
            Gizmos.DrawLine(_Pos1, _Pos2);
        }

    }
}