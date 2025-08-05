using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ZKY
{
    public class InfoTag : MonoBehaviour
    {
        [SerializeField] private float _disableTime;
        [SerializeField] private float _animTime;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _image;

        IEnumerator Start()
        {
            var textcolor = _text.color;
            var imagecolor = _image.color;
            _text.color = new Color(0, 0, 0, 0);
            _image.color = new Color(0, 0, 0, 0);
            _text.DOColor(textcolor, _animTime).SetEase(Ease.InOutQuad);
            _image.DOColor(imagecolor, _animTime).SetEase(Ease.InOutQuad);
            yield return new WaitForSeconds(_animTime);
            yield return new WaitForSeconds(_disableTime);
            _text.DOColor(Color.clear, _animTime).SetEase(Ease.InOutQuad);
            _image.DOColor(Color.clear, _animTime).SetEase(Ease.InOutQuad);
            yield return new WaitForSeconds(_animTime);
            gameObject.SetActive(false);
        }
    }
}
