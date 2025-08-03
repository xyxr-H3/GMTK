using UnityEngine;

namespace ZKY
{
    public class SpineLight : MonoBehaviour
    {
        [SerializeField] private float _circleTime;
        [SerializeField] private float _timer;
        [SerializeField] private bool _directionClockwise;

        private void FixedUpdate()
        {
            _timer += Time.fixedDeltaTime;
            if (_directionClockwise)
            {
                transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(360, 0, _timer / _circleTime));
            }
            else{
                transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(0, 360, _timer / _circleTime));
            }
            if (_timer >= _circleTime)
            {
                _timer = 0;
            }
        }
    }
}