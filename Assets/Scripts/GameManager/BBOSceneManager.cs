using System;
using System.Collections;
using UnityEngine.SceneManagement;

namespace BBO.BBO.GameManagement
{
    public static class BBOSceneManager
    {
        public static void LoadScene(int index)
        {
            SceneManager.LoadScene(index);
        }

        public static void LoadScene(string name)
        {
            SceneManager.LoadScene(name);
        }

        public static IEnumerator LoadSceneAsync(string name, Action finishAction)
        {
            var async = SceneManager.LoadSceneAsync(name);

            while (!async.isDone)
            {
                yield return null;
            }

            finishAction?.Invoke();
        }
    }
}