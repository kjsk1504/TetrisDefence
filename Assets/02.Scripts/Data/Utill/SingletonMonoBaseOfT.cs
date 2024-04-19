using UnityEngine;

namespace TetrisDefence.Data.Utill
{
    /// <summary>
    /// 상속된 자식 클래스를 유니티의 <see cref="MonoBehaviour"/>인 싱글톤(<see cref="SingletonBase{T}"/>)패턴으로 만들기 위한 부모 클래스
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletonMonoBase<T> : MonoBehaviour
        where T : SingletonMonoBase<T>
    {
        /// <summary>
        /// 싱글톤 클래스를 참조하기 위한 인스턴스
        /// </summary>
        public static T Instance
        {
            get
            {
                if (s_instance == null)
                {
                    s_instance = new GameObject(typeof(T).Name).AddComponent<T>();
                }

                return s_instance;
            }
        }

        protected virtual void Awake()
        {
            if (s_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            s_instance = (T)this;
            DontDestroyOnLoad(gameObject);
        }

        /// <summary> 정적 인스턴스 필드 </summary>
        private static T s_instance;
    }
}
