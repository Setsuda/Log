using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log
{
    /// <summary>
    /// 로그
    /// </summary>
    public interface ILog
    {

        #region 로그처리

        /// <summary>
        /// 로그출력
        /// </summary>
        /// <param name="msg">메세지</param>
        /// <param name="isConsoleWrite">콘솔 출력 플러그</param>
        void Line(object msg, bool isConsoleWrite = false);

        /// <summary>
        /// 정보 로그출력
        /// </summary>
        /// <param name="msg">메세지</param>
        /// <param name="isConsoleWrite">콘솔 출력 플러그</param>
        void Info(object msg, bool isConsoleWrite = false);

        /// <summary>
        /// 경고 로그출력
        /// </summary>
        /// <param name="msg">메세지</param>
        /// <param name="isConsoleWrite">콘솔 출력 플러그</param>
        void Warring(object msg, bool isConsoleWrite = false);

        /// <summary>
        /// 에러 로그출력
        /// </summary>
        /// <param name="msg">메세지</param>
        /// <param name="isConsoleWrite">콘솔 출력 플러그</param>
        void Error(object msg, bool isConsoleWrite = false);
        #endregion 로그처리




        #region 그외 처리

        /// <summary>
        /// 리스트 내용물 출력
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">리스트</param>
        /// <param name="msg">메세지</param>
        /// <param name="depthCount">심층 카운트</param>
        /// <param name="isConsoleWrite">콘솔 출력 플러그</param>
        void List<T>(IList<T> list, object msg = null, int depthCount = 0, bool isConsoleWrite = false);

        /// <summary>
        /// 배열 내용물 출력
        /// </summary>
        /// <typeparam name="T">型</typeparam>
        /// <param name="array">배열</param>
        /// <param name="msg">메세지</param>
        /// <param name="depthCount">심층 카운트</param>
        /// <param name="isConsoleWrite">콘솔 출력 플러그</param>
        void Array<T>(T[] array, object msg = null, int depthCount = 0, bool isConsoleWrite = false);


        /// <summary>
        /// 오브젝트 내용물 출력
        /// </summary>
        /// <param name="obj">오브젝트</param>
        /// <param name="msg">메세지</param>
        /// <param name="depthCount">심층 카운트</param>
        /// <param name="isConsoleWrite">콘솔 출력 플러그</param>
        void Object(object obj, object msg = null, int depthCount = 0, bool isConsoleWrite = false);


        #endregion 그외 처리



    }
}
