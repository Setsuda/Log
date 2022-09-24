using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log
{
    class Const
    {
        #region const
        public static string SPACE_TYPE { get; set; } = "\t";
        public static string NEW_LINE { get; set; } = $"{Environment.NewLine}";

        public static string CONDITION_INFO { get; } = $"[INFO]{SPACE_TYPE}";
        public static string CONDITION_WARRING { get; } = $"[WARRING]{SPACE_TYPE}";
        public static string CONDITION_ERROR { get; } = $"[ERROR]{SPACE_TYPE}";

        public static int DEPTH_LEVEL_MAX { get; set; } = 5;

        /// <summary>
        /// DateTimeの日出力フォーマット
        /// </summary>
        public static string DATE_FORMAT_DAY { get; } = "dd";
        /// <summary>
        /// リスト基本型タイプ
        /// </summary>
        public static string LIST_TYPE_STRING { get; } = "System.Collections.Generic.List";
        /// <summary>
        /// リスト中のリスト
        /// </summary>
        public static string LIST_ON_LIST_STRING { get; } = "System.Collections.Generic.List`1[System.Collections.Generic.List";
        /// <summary>
        /// リストがNullの場合メッセージ
        /// </summary>
        public static string LIST_IS_NULL { get; } = "リスト：[null]";
        /// <summary>
        /// タブ変換用文字列
        /// </summary>
        public static string CHANGE_TAB_STRING { get; } = "hamChangeTabPoint";
        /// <summary>
        /// {
        /// </summary>
        public static string DAI_KAKKO { get; } = "{";
        /// <summary>
        /// }
        /// </summary>
        public static string DAI_KAKKO_TOJI { get; } = "}";
        #endregion const
    }
}
