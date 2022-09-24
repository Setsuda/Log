using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log
{
    static class LogUtil
    {



        /// <summary>
        /// 개행 혹은 공객을 반환
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string AddNewLineAndNullToBlank(this object msg)
        {
            string rtn = "";

            try
            {
                if (msg != null && msg.ToString() != "") rtn = msg.ToString() + Const.NEW_LINE;
            }
            catch (Exception e)
            {
                System.Console.WriteLine($"메세지가 null일 경우 공백을, 아닐경우 개행을 추가하는 처리에 실패했습니다.");
                System.Console.WriteLine($"[Error]:{e}");

            }

            return rtn;
        }


        /// <summary>
        /// 리스트를 문서화 출력
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="msg"></param>
        /// <param name="depthCount">심층 카운트</param>
        /// <returns></returns>
        public static string BaseList<T>(this List<T> list, string msg, int depthCount)
        {

            msg = $"{msg}";

            if (list == null)
            {
                msg += "[null]";
            }
            else
            {
                if (list.Count == 0)
                {
                    msg += "요소가 없습니다." + Const.NEW_LINE;
                }
                else
                {

                    if (typeof(T) == typeof(string[]))
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            if (i > 0) msg += Const.NEW_LINE;
                            msg += $"List<string[]> : [{(i + 1)}]번 : ";
                            msg += GetStringsMsg((string[])(object)list[i], "", depthCount);
                        }
                    }
                    else if (typeof(T) == typeof(string))
                    {
                        List<string> tmpList = new List<string>();
                        for (int i = 0; i < list.Count; i++)
                        {
                            tmpList.Add(list[i].ToString());
                        }
                        msg += GetStringsMsg(tmpList.ToArray(), "", depthCount);
                    }
                    else
                    {
                        msg = GetListMsg(list, msg, depthCount);
                    }
                }
            }
            string tmpMsg = msg == "" ? "이 리스트에는 아무것도 없습니다." : msg;

            return tmpMsg;
        }

        /// <summary>
        /// String의 배열을 문자열로 반환
        /// </summary>
        /// <param name="strs">String배열</param>
        /// <param name="msg">메세지</param>
        /// <param name="depthCount">심층 카운트</param>
        /// <returns></returns>
        public static string GetStringsMsg(this string[] strs, string msg, int depthCount)
        {


            string depthTab = "";
            for (int i = 0; i < depthCount; i++)
            {
                depthTab += "\t";
            }
            string firstMsg = AddNewLineAndNullToBlank(msg);
            string tmpMsg = "";

            string objName = Const.NEW_LINE + depthTab + "string[] 출력\r\n";

            try
            {

                for (int i = 0; i < strs.Length; i++)
                {
                    tmpMsg += depthTab + (i + 1).ToString() + "번 : [";

                    try
                    {
                        tmpMsg += strs[i] + "]\r\n";
                    }
                    catch (System.NullReferenceException)
                    {
                        tmpMsg += "null]\r\n";
                    }
                }

                tmpMsg = firstMsg + objName + (tmpMsg == "" ? "이 String배열은 출력할 값이 없습니다." : tmpMsg);
            }
            catch (System.NullReferenceException)
            {
                tmpMsg += firstMsg + "[null]\r\n";
            }
            catch (Exception)
            {
                tmpMsg = msg + objName + "이 String벼열의 출력에 실패했습니다.";
            }

            return tmpMsg;
        }




        /// <summary>
        /// 리스트를 메세지로 변환
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="msg"></param>
        /// <param name="depthCount">심층 카운트</param>
        /// <param name="isFirst">초기 플러그</param>
        /// <returns></returns>
        public static string GetListMsg<T>(List<T> list, string msg, int depthCount, bool isFirst = true)
        {
            msg = AddNewLineAndNullToBlank(msg);

            if (list == null) return msg + "null";

            string depthTab = CreateDepthTab(depthCount);

            if (list.Count == 0) return msg += depthTab + "요소가 없습니다." + Const.NEW_LINE;

            for (int i = 0; i < list.Count; i++)
            {
                msg += depthTab;
                msg += $"{i + 1}番目 : ";
                msg += GetObjectMsg(list[i], "", 0, isFirst);
            }
            return msg;
        }



        /// <summary>
        /// 심층수 만큼 탭
        /// </summary>
        /// <param name="depthCount">심층 카운트</param>
        /// <returns></returns>
        public static string CreateDepthTab(int depthCount)
        {
            string rtn = "";
            for (int i = 0; i < depthCount; i++)
            {
                rtn += "\t";
            }

            return rtn;
        }





        /// <summary>
        /// 오브젝트의 내용물을 메세지에 격납
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="depthCount">심층 카운트</param>
        /// <param name="msg"></param>
        /// <param name="isFirst">초기 플러그</param>
        /// <returns></returns>
        public static string GetObjectMsg<T>(T obj, string msg = "", int depthCount = 0, bool isFirst = true)
        {

            string title = AddNewLineAndNullToBlank(msg);

            string depthTab = "";
            string depthTabNext = "";
#if DEBUG
            System.Console.WriteLine($"depthCount : {depthCount}");
#endif
            depthTab = CreateDepthTab(depthCount);
            depthTabNext = CreateDepthTab(depthCount + 1);

            string tmpMsg = title;

#if DEBUG
            System.Console.WriteLine($"[obj]:{obj}");
#endif

            if (obj == null) return tmpMsg = title + depthTab + "[null]\r\n";

            if (depthCount > Const.DEPTH_LEVEL_MAX) return title + depthTab + $"Warring : 심층출력 제한에 해당합니다.(제한 : 하위{Const.DEPTH_LEVEL_MAX}계층까지)" + Const.NEW_LINE;

            Type objType = obj.GetType();

            string objName = depthTab + $"오브젝트 멤버출력[타입 : {objType}]{Const.NEW_LINE}" + depthTab + "오브젝트명 : " + objType.Name + Const.NEW_LINE;

            if (isFirst == true)
            {
                tmpMsg += objName;
            }

            try
            {

                //오브젝트가 클레스가 아니거나 string, StringBuild일 경우
                if (objType.IsClass != true || objType == typeof(string) || objType == typeof(System.Text.StringBuilder))
                {
                    tmpMsg += $"{depthTab}[{obj}]{Const.NEW_LINE}";
                }
                //리스트일 경우
                else if (objType.ToString().Contains(Const.LIST_TYPE_STRING) == true)
                {

                    string listName = depthTabNext + $"오브젝트 멤버 출력[타입 : {objType}]{Const.NEW_LINE}" + depthTabNext + "오브젝트명 : " + objType.Name + Const.NEW_LINE;

                    string tmp = GetNoneListMsg(obj, listName, depthCount + 1, false);
#if DEBUG
                    System.Console.WriteLine($"[List]:{tmp}");
#endif
                    tmpMsg += Const.NEW_LINE + depthTabNext + "{" + Const.NEW_LINE + tmp + depthTabNext + "}\r\n";
                }
                //배열일 경우
                else if (objType.IsArray)
                {
                    if (objType == typeof(int[]))
                    {
                        int[] ints = (int[])(object)obj;
                        tmpMsg += GetListMsg(ints.ToList(), "", depthCount, false);
                    }
                    else if (objType == typeof(bool[]))
                    {
                        bool[] bools = (bool[])(object)obj;
                        tmpMsg += GetListMsg(bools.ToList(), "", depthCount, false);
                    }
                    else if (objType == typeof(byte[]))
                    {
                        byte[] bytes = (byte[])(object)obj;
                        tmpMsg += GetListMsg(bytes.ToList(), "", depthCount, false);
                    }
                    else if (objType == typeof(sbyte[]))
                    {
                        sbyte[] sbytes = (sbyte[])(object)obj;
                        tmpMsg += GetListMsg(sbytes.ToList(), "", depthCount, false);
                    }
                    else if (objType == typeof(char[]))
                    {
                        char[] chars = (char[])(object)obj;
                        tmpMsg += GetListMsg(chars.ToList(), "", depthCount, false);
                    }
                    else if (objType == typeof(decimal[]))
                    {
                        decimal[] decimals = (decimal[])(object)obj;
                        tmpMsg += GetListMsg(decimals.ToList(), "", depthCount, false);
                    }
                    else if (objType == typeof(double[]))
                    {
                        double[] doubles = (double[])(object)obj;
                        tmpMsg += GetListMsg(doubles.ToList(), "", depthCount, false);
                    }
                    else if (objType == typeof(float[]))
                    {
                        float[] floats = (float[])(object)obj;
                        tmpMsg += GetListMsg(floats.ToList(), "", depthCount, false);
                    }
                    else if (objType == typeof(uint[]))
                    {
                        uint[] uints = (uint[])(object)obj;
                        tmpMsg += GetListMsg(uints.ToList(), "", depthCount, false);
                    }
                    else if (objType == typeof(long[]))
                    {
                        long[] longs = (long[])(object)obj;
                        tmpMsg += GetListMsg(longs.ToList(), "", depthCount, false);
                    }
                    else if (objType == typeof(ulong[]))
                    {
                        ulong[] ulongs = (ulong[])(object)obj;
                        tmpMsg += GetListMsg(ulongs.ToList(), "", depthCount, false);
                    }
                    else if (objType == typeof(short[]))
                    {
                        short[] shorts = (short[])(object)obj;
                        tmpMsg += GetListMsg(shorts.ToList(), "", depthCount, false);
                    }
                    else if (objType == typeof(ushort[]))
                    {
                        ushort[] ushorts = (ushort[])(object)obj;
                        tmpMsg += GetListMsg(ushorts.ToList(), "", depthCount, false);
                    }
                    else
                    {

                        IEnumerable<T> ts = (IEnumerable<T>)obj;
                        tmpMsg += $"{depthTab}{objType}\r\n" + depthTab + "{\r\n";
                        if (ts.Count() == 0)
                        {
                            tmpMsg += depthTab + "이 배열은 요소가 없습니다.\r\n";
                        }
                        else
                        {
                            int count = 0;
                            foreach (var t in ts)
                            {
                                tmpMsg += GetObjectMsg(t, "", depthCount, false);
                                count++;
                            }

                            tmpMsg += depthTab + "}\r\n";
                        }
                    }

                }
                //오브젝트가 클레스일 경우
                else
                {
                    string fieldName = "";

                    System.Reflection.FieldInfo[] fieldInfos = objType.GetFields();



                    foreach (System.Reflection.FieldInfo f in fieldInfos)
                    {
                        tmpMsg += depthTab + f.Name + " : ";
                        Type pType = f.FieldType;
                        fieldName = depthTabNext + $"오브젝트 멤버 출력[타입 : {pType}]";

                        try
                        {

#if DEBUG
                            System.Console.WriteLine($"[Type]:{pType}");
#endif

                            //리스트일 경우
                            if (pType.ToString().Contains(Const.LIST_TYPE_STRING) == true)
                            {
                                string tmp = GetNoneListMsg(f.GetValue(obj), fieldName, depthCount + 1, false);
#if DEBUG
                                System.Console.WriteLine($"[List]:{tmp}");
#endif
                                tmpMsg += Const.NEW_LINE + depthTabNext + "{" + Const.NEW_LINE + tmp + depthTabNext + "}\r\n";
                            }
                            else if (pType.IsClass != true || pType == typeof(string) || pType == typeof(System.Text.StringBuilder))
                            {

                                tmpMsg += GetObjectMsg(f.GetValue(obj), "", 0, false);
                            }
                            else
                            {
                                tmpMsg += Const.NEW_LINE + GetObjectMsg(f.GetValue(obj), fieldName, depthCount + 1, false);
                            }
                        }
                        catch (System.Reflection.TargetParameterCountException)
                        {
                            tmpMsg += "[" + pType + "]\r\n";
                        }
                        catch (System.NullReferenceException)
                        {
                            tmpMsg += "[null]\r\n";
                        }
                    }

                    string propertyName = "";
                    System.Reflection.PropertyInfo[] propertyInfos = objType.GetProperties();
                    foreach (System.Reflection.PropertyInfo p in propertyInfos)
                    {
                        tmpMsg += depthTab + p.Name + " : ";
                        Type pType = p.PropertyType;
                        propertyName = depthTabNext + $"오브젝트 멤버 출력[타입 : {pType}]";

                        try
                        {

#if DEBUG
                            System.Console.WriteLine($"[Type]:{pType}");
#endif

                            //リストの場合
                            if (pType.ToString().Contains(Const.LIST_TYPE_STRING) == true)
                            {
                                string tmp = GetNoneListMsg(p.GetValue(obj), propertyName, depthCount + 1, false);
#if DEBUG
                                System.Console.WriteLine($"[List]:{tmp}");
#endif
                                tmpMsg += Const.NEW_LINE + depthTabNext + "{" + Const.NEW_LINE + tmp + depthTabNext + "}\r\n";
                            }
                            else if (pType.IsClass != true || pType == typeof(string) || pType == typeof(System.Text.StringBuilder))
                            {

                                tmpMsg += GetObjectMsg(p.GetValue(obj), "", 0, false);
                            }
                            else
                            {
                                tmpMsg += Const.NEW_LINE + GetObjectMsg(p.GetValue(obj), propertyName, depthCount + 1, false);
                            }
                        }
                        catch (System.Reflection.TargetParameterCountException)
                        {
                            tmpMsg += "[" + pType + "]\r\n";
                        }
                        catch (System.NullReferenceException)
                        {
                            tmpMsg += "[null]\r\n";
                        }
                    }

                }


            }
            catch (Exception)
            {
                tmpMsg = depthTab + $"이 오브젝트 출력에 실패했습니다.[{obj.GetType().Name}]{Const.NEW_LINE}";
            }




            return tmpMsg;
        }




        /// <summary>
        ///  리스트가 아닐 경우?
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="msg"></param>
        /// <param name="depthCount"></param>
        /// <param name="isFirst">초기 플러그</param>
        /// <returns></returns>
        public static string GetNoneListMsg<T>(T list, string msg = "", int depthCount = 0, bool isFirst = true)
        {
            string tmpMsg = AddNewLineAndNullToBlank(msg);

            if (list == null) return tmpMsg += $"{CreateDepthTab(depthCount)}[null]{Const.NEW_LINE}";


            try
            {

                IEnumerable<T> ts = (IEnumerable<T>)list;
                if (ts.Count() == 0)
                {
                    tmpMsg += $"{CreateDepthTab(depthCount)}요소가 없습니다.{Const.NEW_LINE}";
                }
                else
                {
                    foreach (var t in ts)
                    {
                        tmpMsg += GetObjectMsg(t, "", depthCount, isFirst);
                    }
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine($"로그를 출력할수 없습니다.");
                System.Console.WriteLine($"[Error]:{e}");
            }

            return tmpMsg;
        }


    }
}
