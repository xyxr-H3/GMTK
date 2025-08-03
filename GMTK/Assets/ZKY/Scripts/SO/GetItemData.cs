using UnityEngine;


namespace ZKY

{
    [CreateAssetMenu(fileName = "GetItemData", menuName = "ZKY/ItemData", order = 1)]
    public class GetItemData : ScriptableObject
    {
        public bool isGet;
        private void OnEnable()
        {
            isGet = false;
        }
    }
}