using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ZKY
{
    [CreateAssetMenu(fileName = "Events", menuName = "ZKY/Events", order = 1)]
    public class MyEvents : ScriptableObject
    {
        public UnityAction _event;
        public void Invoke()
        {
            _event?.Invoke();
        }
    }

}
