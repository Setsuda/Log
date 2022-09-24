using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace Log
{
    /// <summary>
    /// 로그 메인 클레스
    /// </summary>
    public class Log : ILog
    {

        #region const

        #endregion const

        #region 멤버

        /// <summary>로그 파일명</summary>
        public string FileName { get; set; } = "Log";
        /// <summary>로그 파일 패스</summary>
        public string FilePath { get; set; } = $@"{Environment.CurrentDirectory}\Logs";
        /// <summary>로그 파일명의 일시</summary>
        public string FileNameDate { get; set; } = $"_{DateTime.Now.ToString("yyyyMMddHHmmss")}";
        /// <summary>로그 파일명</summary>
        public string FileExtension { get; set; } = ".log";
        /// <summary>로그 확장자</summary>
        public string FullPath { get { return $@"{FilePath}\{FileName}{FileNameDate}{FileExtension}"; } set { FilePath = Path.GetDirectoryName(value); FileName = Path.GetFileNameWithoutExtension(value); FileExtension = Path.GetExtension(value); } }

        /// <summary>로그 출력 시간 포멧</summary>
        public string OutputTimeFormat { get; set; } = "[yyyy/MM/dd HH:mm:ss]";
        /// <summary>로그 시간 출력 플러그</summary>
        public bool IsOutputTimeWrite { get; set; } = true;
        /// <summary>로그 네임스패이`스 출력 플러그</summary>
        public bool IsNamespaceWrite { get; set; } = true;
        /// <summary>로그 파일명용 날짜</summary>
        public int CurrentDay { get; set; } = DateTime.Now.Day;

        /// <summary>리스트 출력용 심층 맥스</summary>
        public int DepthLevelMax { get { return Const.DEPTH_LEVEL_MAX; } set { Const.DEPTH_LEVEL_MAX = value; } }
        #endregion 멤버


        #region 초기화
        /// <summary>
        /// 초기화 : 파일명 어플리케이션\Logs\Log_yyyyMMddHHmmss.log
        /// </summary>
        public Log()
        {
        }
        /// <summary>
        /// 초기화 : 파일명 어플리케이션\Logs\{fileName}_yyyyMMddHHmmss.log
        /// </summary>
        /// <param name="fileName">파일명</param>
        public Log(string fileName)
        {
            FileName = FileName;
        }
        /// <summary>
        /// 초기화 : 파일명 {filePath}\{fileName}_yyyyMMddHHmmss.log
        /// </summary>
        /// <param name="fileName">파일명</param>
        /// <param name="filePath">패스</param>
        public Log(string fileName, string filePath)
        {
            FileName = FileName;
            FilePath = filePath;
        }

        /// <summary>
        /// 초기화 : 파일명 {filePath}\{fileName}_yyyyMMddHHmmss.{확장자}
        /// </summary>
        /// <param name="fileName">파일명</param>
        /// <param name="filePath">패스</param>
        /// <param name="fileExtension">확장자</param>
        public Log(string fileName, string filePath, string fileExtension)
        {
            FileName = FileName;
            FilePath = filePath;
            FileExtension = fileExtension.IndexOf(".") == 0 ? fileExtension : $".{fileExtension}";
        }
        #endregion 초기화

        #region 로그처리

        /// <summary>
        /// 로그출력
        /// </summary>
        /// <param name="msg">메세지</param>
        /// <param name="isConsoleWrite">콘솔 출력 플러그</param>
        public void Line(object msg, bool isConsoleWrite = false)
        {
            string tmpMsg = LogUtil.AddNewLineAndNullToBlank(msg);
            Write(tmpMsg, "", isConsoleWrite);
        }

        /// <summary>
        /// 정보 로그출력
        /// </summary>
        /// <param name="msg">메세지</param>
        /// <param name="isConsoleWrite">콘솔 출력 플러그</param>
        public void Info(object msg, bool isConsoleWrite = false)
        {
            string tmpMsg = LogUtil.AddNewLineAndNullToBlank(msg);
            Write(tmpMsg, Const.CONDITION_INFO, isConsoleWrite);
        }

        /// <summary>
        /// 경고 로그출력
        /// </summary>
        /// <param name="msg">메세지</param>
        /// <param name="isConsoleWrite">콘솔 출력 플러그</param>
        public void Warring(object msg, bool isConsoleWrite = false)
        {
            string tmpMsg = LogUtil.AddNewLineAndNullToBlank(msg);
            Write(tmpMsg, Const.CONDITION_WARRING, isConsoleWrite);
        }
        /// <summary>
        /// 에러 로그출력
        /// </summary>
        /// <param name="msg">메세지</param>
        /// <param name="isConsoleWrite">콘솔 출력 플러그</param>
        public void Error(object msg, bool isConsoleWrite = false)
        {
            string tmpMsg = LogUtil.AddNewLineAndNullToBlank(msg);
            Write(tmpMsg, Const.CONDITION_ERROR, isConsoleWrite);
        }

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
        public void List<T>(IList<T> list, object msg = null, int depthCount = 0, bool isConsoleWrite = false)
        {
            string tmpMsg = LogUtil.AddNewLineAndNullToBlank(msg);
            tmpMsg = LogUtil.BaseList((List<T>)list, tmpMsg, depthCount);
            Write(tmpMsg, "", isConsoleWrite);
        }


        /// <summary>
        /// 배열 내용물 출력
        /// </summary>
        /// <typeparam name="T">型</typeparam>
        /// <param name="array">배열</param>
        /// <param name="msg">메세지</param>
        /// <param name="depthCount">심층 카운트</param>
        /// <param name="isConsoleWrite">콘솔 출력 플러그</param>
        public void Array<T>(T[] array, object msg = null, int depthCount = 0, bool isConsoleWrite = false)
        {
            string tmpMsg = LogUtil.AddNewLineAndNullToBlank(msg);
            tmpMsg = LogUtil.BaseList(array != null ? array.ToList() : null, tmpMsg, depthCount);
            Write(tmpMsg, "", isConsoleWrite);
        }


        /// <summary>
        /// 오브젝트 내용물 출력
        /// </summary>
        /// <param name="obj">오브젝트</param>
        /// <param name="msg">메세지</param>
        /// <param name="depthCount">심층 카운트</param>
        /// <param name="isConsoleWrite">콘솔 출력 플러그</param>
        public void Object(object obj, object msg = null, int depthCount = 0, bool isConsoleWrite = false)
        {
            string tmpMsg = LogUtil.AddNewLineAndNullToBlank(msg);
            tmpMsg = LogUtil.GetObjectMsg(obj, tmpMsg, depthCount);
            Write(tmpMsg, "", isConsoleWrite);
        }



        #endregion 그외 처리


        #region 파일 출력

        /// <summary>
        /// 로그 파일 출력
        /// </summary>
        /// <param name="msg">메세지</param>
        /// <param name="condition">출력 종류</param>
        /// <param name="isConsoleWrite">출력 종류</param>
        private void Write(string msg, string condition, bool isConsoleWrite)
        {
            if (!Directory.Exists(FilePath)) Directory.CreateDirectory(FilePath);

            if (CurrentDay != DateTime.Now.Day)
            {
                CurrentDay = DateTime.Now.Day;
                FileNameDate = $"_{DateTime.Now.ToString("yyyyMMddHHmmss")}";
            }

            string text = $"{condition}{msg}";
            string tmp = "";
            if (IsNamespaceWrite)
            {
                string methodName = new StackFrame(2, true).GetMethod().Name;
                string methodNamespace = new StackTrace().GetFrame(2).GetMethod().ReflectedType.FullName;
                tmp = $"{methodNamespace}.{methodName}{Const.SPACE_TYPE}";
            }
            
            if (IsOutputTimeWrite) tmp = $"{DateTime.Now.ToString(OutputTimeFormat)}{Const.SPACE_TYPE}{tmp}";
            text = $"{tmp}{text}";
            File.AppendAllText(FullPath, text);
            if (isConsoleWrite) System.Console.WriteLine(text);

        }

        #endregion 파일 출력




    }
}
