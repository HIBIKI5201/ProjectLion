using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SymphonyFrameWork.CoreSystem
{
    public static class SceneLoader
    {
        private static Dictionary<string, Scene> _sceneDict = new();

        public static Scene? GetExistScene(string sceneName)
        {
            bool result = _sceneDict.TryGetValue(sceneName, out Scene scene);
            return result ? scene : null;
        }

        /// <summary>
        /// シーンをロードする
        /// </summary>
        /// <param name="sceneName">シーン名</param>
        /// <param name="loadingAction">ロードの進捗率を引数にしたメソッド</param>
        /// <returns>ロードに成功したか</returns>
        public static async Task<bool> LoadScene(string sceneName, Action<float> loadingAction = null)
        {
            if (_sceneDict.ContainsKey(sceneName))
            {
                Debug.LogWarning($"{sceneName}シーンは既にロードされています");
                return false;
            }

            var operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            if (operation == null)
            {
                Debug.LogError($"{sceneName}シーンは登録されていません");
                return false;
            }

            while (operation.isDone)
            {
                loadingAction?.Invoke(operation.progress);
                await Awaitable.NextFrameAsync();
            }

            Scene loadedScene = SceneManager.GetSceneByName(sceneName);
            if (loadedScene.IsValid() && loadedScene.isLoaded)
            {
                _sceneDict.TryAdd(sceneName, loadedScene);
                return true;
            }
            else return false;
        }

        /// <summary>
        /// シーンをアンロードする
        /// </summary>
        /// <param name="sceneName">シーン名</param>
        /// <param name="loadingAction">ロードの進捗率を引数にしたメソッド</param>
        /// <returns>アンロードに成功したか</returns>
        public static async Task<bool> UnloadScene(string sceneName, Action<float> loadingAction = null)
        {
            if (!_sceneDict.ContainsKey(sceneName))
            {
                Debug.LogWarning($"{sceneName}シーンはロードされていません");
                return false;
            }

            var operation = SceneManager.UnloadSceneAsync(sceneName);
            if (operation == null)
            {
                Debug.LogError($"{sceneName}シーンは登録されていません");
                return false;
            }

            while (!operation.isDone)
            {
                loadingAction?.Invoke(operation.progress);
                await Awaitable.NextFrameAsync();
            }

            return true;
        }
    }
}
