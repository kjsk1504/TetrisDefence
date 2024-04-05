using System;
using System.Collections.Generic;

namespace TetrisDefence.Data
{
    /// <summary>
    /// 저장소의 기본 상호작용
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> 
    {
        /// <summary>
        /// 데이터가 변경되었을때 알림 통지. <see langword="int"/>번째 아이템이 <see langword="T"/>로 변경되었음.
        /// </summary>
        event Action<int, T> onItemUpdated;

        /// <summary>
        /// 모든 데이터 순회용 함수
        /// </summary>
        /// <returns> 데이터 리스트 </returns>
        IEnumerable<T> GetAllDatas();

        /// <summary>
        /// 인덱스로 데이터 검색용 함수
        /// </summary>
        /// <param name="id"> 검색할 데이터의 인덱스 </param>
        /// <returns> 검색된 데이터 </returns>
        T GetDataByID(int id);

        /// <summary>
        /// 데이터 삽입용 함수
        /// </summary>
        /// <param name="data"> 삽입할 데이터 </param>
        void InsertData(T data);

        /// <summary>
        /// 데이터 삭제용 함수
        /// </summary>
        /// <param name="data"> 삭제할 데이터 </param>
        void DeleteData(T data);

        /// <summary>
        /// 인덱스에 해당하는 데이터 갱신용 함수
        /// </summary>
        /// <param name="id"> 검색할 인덱스 </param>
        /// <param name="data"> 갱신할 데이터 </param>
        void UpdateData(int id, T data);

        /// <summary>
        /// 데이터 변동내용 DB에 일괄 저장용 함수
        /// </summary>
        void Save();
    }
}