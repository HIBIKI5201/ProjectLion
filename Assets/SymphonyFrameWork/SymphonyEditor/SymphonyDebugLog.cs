using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace SymphonyFrameWork.Editor
{
    /// <summary>
    /// �G�f�B�^�p�̃��O�𔭍s����N���X
    /// </summary>
    public static class SymphonyDebugLog
    {
#if UNITY_EDITOR
        private static string _logText = string.Empty;
#endif
        [Conditional("UNITY_EDITOR")]
        public static void DirectLog(string text)
        {
#if UNITY_EDITOR
            Debug.Log(text);
#endif
        }

        [Conditional("UNITY_EDITOR")]
        public static void AddText(string text)
        {
#if UNITY_EDITOR
            _logText += $"{text}\n";
#endif
        }

        [Conditional("UNITY_EDITOR")]
        public static void ClearText()
        {
#if UNITY_EDITOR
            _logText = string.Empty;
#endif
        }

        [Conditional("UNITY_EDITOR")]
        public static void TextLog()
        {
#if UNITY_EDITOR
            Debug.Log(_logText);
#endif
        }
    }
}