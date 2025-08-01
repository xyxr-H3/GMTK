using UnityEngine;
using UnityEngine.UI;
namespace ZKY
{
    public class ExitButton : MonoBehaviour
    {
        private Button _button;
        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnExitButtonClick);
        }

        private void OnExitButtonClick()
        {
            Application.Quit();
        }
    }
}