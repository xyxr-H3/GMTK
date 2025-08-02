using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace ZKY
{
    public class GiftManager : MonoBehaviour
    {
        [SerializeField] private MyEvents _getGiftEvent;
        [SerializeField] private GameObject _closed;
        [SerializeField] private GameObject _opened;
        [SerializeField] private GameObject _giftItm;
        [SerializeField] private GameObject _giftOnScreen;
        [SerializeField] private float _wait;
        [SerializeField] private bool _isOpened = false;
        [SerializeField] private float _giftItemMoveY;
        [SerializeField] private float _giftItemMoveDuration;
        [SerializeField] private float _giftItemMoveDuration2;

        private void OnEnable()
        {
            _getGiftEvent._event += GetGift;
        }

        private void OnDisable()
        {
            _getGiftEvent._event -= GetGift;
        }

        public void GetGift()
        {
            if (_isOpened) return;
            _isOpened = true;
            _closed.SetActive(true);
            StartCoroutine(WaitAndClose());
        }

        IEnumerator WaitAndClose()
        {
            yield return new WaitForSeconds(_wait);
            _closed.SetActive(false);
            _opened.SetActive(true);
            _giftItm.SetActive(true);
            _giftItm.transform.DOMoveY(_giftItemMoveY, _giftItemMoveDuration).onComplete += () =>
            {
                _giftItm.transform.DOScale(Vector3.zero, _giftItemMoveDuration2).SetEase(Ease.InBack);
                _giftOnScreen.SetActive(true);
                _giftOnScreen.transform.localScale = Vector3.zero;
                _giftOnScreen.transform.DOScale(Vector3.one, _giftItemMoveDuration2).SetEase(Ease.OutBack);
            };
            yield return new WaitForSeconds(_giftItemMoveDuration + _giftItemMoveDuration2);
            _opened.SetActive(false);
        }
        // private void Update()
        // {
        //     var rect = transform as RectTransform;
        //     Debug.Log(rect.anchoredPosition3D);
        // }
    }
}
