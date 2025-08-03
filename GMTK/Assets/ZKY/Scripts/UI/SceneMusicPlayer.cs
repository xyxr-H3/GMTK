using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZKY
{
    public class SceneMusicPlayer : MonoBehaviour
    {
        [SerializeField] private string _audioName;
        [SerializeField] private bool _isFade;
        [SerializeField] private float _fadeTime = 1f;
        // Start is called before the first frame update
        void Start()
        {
            if (_isFade)
            {
                SoundManager.instance.ChangeVolumn(_audioName, 0);
                SoundManager.instance.Play(_audioName);
                SoundManager.instance.FadeVolumn(_audioName,1,_fadeTime);
            }
            else
            {
                SoundManager.instance.Play(_audioName);
            }
        }
    }    
}
