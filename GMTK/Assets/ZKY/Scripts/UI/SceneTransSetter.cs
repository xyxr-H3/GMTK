using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ZKY
{
    public class SceneTransSetter : MonoBehaviour
    {
        [SerializeField] private string _sceneToGo;
        [SerializeField] private bool _useTransition;
        private bool _isTransitioning = false;
        [SerializeField] private bool _playAudio;
        [Header("Music Settings")]
        [SerializeField] private string _musicName;
        [SerializeField] private bool _stopMusic;
        [SerializeField] private bool _fadeOutMusic;
        [SerializeField] private float _fadeOutTime;

        private void Update()
        {
            //任意键按下
            if (Input.anyKeyDown && !_isTransitioning)
            {
                _isTransitioning = true;
                if (_playAudio)
                {
                    SoundManager.instance.Play("Start");
                }
                if (_stopMusic)
                {
                    if (_fadeOutMusic)
                    {
                        SoundManager.instance.FadeVolumn(_musicName, 0, _fadeOutTime);
                        Invoke(nameof(StopMusic), _fadeOutTime);
                    }
                    else
                    {
                        SoundManager.instance.Stop(_musicName);
                    }
                }
                SceneLoader.instance.LoadScene(_sceneToGo, _useTransition);
            }
        }

        private void StopMusic()
        {
            SoundManager.instance.Stop(_musicName);
            SoundManager.instance.ChangeVolumn(_musicName, 1);
        }
    }
}
