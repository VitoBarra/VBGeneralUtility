using UnityEngine;

namespace VitoBarra.GeneralUtility
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T I;

        protected virtual void Awake()
        {
            if (I == null)
                I = (T)FindObjectOfType(typeof(T));
            else
                Destroy(gameObject);
        }
    }


    public class SingletonDontDestroy<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T I;

        protected virtual void Awake()
        {
            if (I == null)
            {
                I = (T)FindObjectOfType(typeof(T));
                DontDestroyOnLoad(gameObject);
            }
            else
                Destroy(gameObject);
        }
    }
}