using System;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : Singleton<SceneControl>
{
    /// <summary>
    /// 씬의 이름 그대로 사용.
    /// </summary>
    public enum SceneType
    {
        /// <summary>
        /// 앱 진입 씬
        /// </summary>
        Entry,
        /// <summary>
        /// 씬전환시 잠시 이동할 씬
        /// </summary>
        Load,
        /// <summary>
        /// 메인 씬
        /// </summary>
        Main
    }

    private SceneType nextScne = default(SceneType);
    private Action<float> onLoadPercentCallback;
    private Action onComplateCallback;
    private Coroutine asyncCoroutine;   // 얘는 실제 안쓰일것 같지만.. 게임 기능중 로드하다 취소하는 기능이 필요할 경우를 상정해서 미리 만들어만 두자.

    /// <summary>
    /// 씬의 이동시 호출
    /// </summary>
    /// <param name="_type">이동할 씬의 타입</param>
    /// <param name="loadPercentCallback">씬의 로드값 콜백(0 ~ 1.0)</param>
    /// <param name="complateCallback">씬의 로드 완료후 콜백이 필요할 경우</param>
    public void ChangeScene(SceneType _type, Action<float> loadPercentCallback = null, Action complateCallback = null)
    {
        this.nextScne = _type;
        this.onLoadPercentCallback = loadPercentCallback;
        this.onComplateCallback = complateCallback;
        this.asyncCoroutine = StartCoroutine(LoadAsyncScene());
    }

    private IEnumerator LoadAsyncScene()
    {
        // 기본 Load 씬으로 이동한다. (요건 얼마 안걸리지만 Load씬에 뭐가 들어올지 모르므로 비동기 로드로 처리한다.)
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneType.Load.ToString());
        while (!asyncLoad.isDone)
            yield return null;

        // Load씬으로 이동 완료후 실제 이동할 씬을 로드한다.
        asyncLoad = SceneManager.LoadSceneAsync(nextScne.ToString());
        while (!asyncLoad.isDone)
        {
            this.onLoadPercentCallback?.Invoke(asyncLoad.progress);
            yield return null;
        }

        this.onComplateCallback?.Invoke();
        this.InitValue();
    }

    private void InitValue()
    {
        this.nextScne = default(SceneType);
        this.onLoadPercentCallback = null;
        this.onComplateCallback = null;
        this.asyncCoroutine = null;
    }
}
