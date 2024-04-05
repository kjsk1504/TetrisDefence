using System;

namespace TetrisDefence.Data.Utill
{
    /// <summary>
    /// 상속된 자식 클래스를 싱글톤 패턴으로 만들기 위한 부모 클래스
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletonBase<T>
        where T : SingletonBase<T>
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
                    // ConstructorInfo constructorInfo = typeof(T).GetConstructor(new Type[] { });
                    // _instance = (T)constructorInfo.Invoke(new object[] { });

                    s_instance = (T)Activator.CreateInstance(typeof(T));
                }

                return s_instance;
            }
        }

        /// <summary> 정적 인스턴스 필드 </summary>
        private static T s_instance;
    }
}