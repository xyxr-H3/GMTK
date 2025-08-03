using UnityEngine;
namespace ZKY
{
    public class WinCheckErea : MonoBehaviour
    {
        public GetItemData _data;
        [SerializeField] private string _winSceneName;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && _data.isGet)
            {
                SoundManager.instance.FadeVolumn("Intense", 0, 1f);
                Invoke("Stop", 1f);
                SceneLoader.instance.LoadScene("WinScene", true);
            }
        }
        private void Stop()
        {
            SoundManager.instance.Stop("Intense");
        }
    }
}