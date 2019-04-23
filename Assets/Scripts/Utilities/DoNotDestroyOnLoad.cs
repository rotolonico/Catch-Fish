using UnityEngine;

namespace Utilities
{
    public class DoNotDestroyOnLoad : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
