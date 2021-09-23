using System;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : Singleton<SceneControl>
{
    /// <summary>
    /// ���� �̸� �״�� ���.
    /// </summary>
    public enum SceneType
    {
        /// <summary>
        /// �� ���� ��
        /// </summary>
        Entry,
        /// <summary>
        /// ����ȯ�� ��� �̵��� ��
        /// </summary>
        Load,
        /// <summary>
        /// ���� ��
        /// </summary>
        Main
    }

    private SceneType nextScne = default(SceneType);
    private Action<float> onLoadPercentCallback;
    private Action onComplateCallback;
    private Coroutine asyncCoroutine;   // ��� ���� �Ⱦ��ϰ� ������.. ���� ����� �ε��ϴ� ����ϴ� ����� �ʿ��� ��츦 �����ؼ� �̸� ���� ����.

    /// <summary>
    /// ���� �̵��� ȣ��
    /// </summary>
    /// <param name="_type">�̵��� ���� Ÿ��</param>
    /// <param name="loadPercentCallback">���� �ε尪 �ݹ�(0 ~ 1.0)</param>
    /// <param name="complateCallback">���� �ε� �Ϸ��� �ݹ��� �ʿ��� ���</param>
    public void ChangeScene(SceneType _type, Action<float> loadPercentCallback = null, Action complateCallback = null)
    {
        this.nextScne = _type;
        this.onLoadPercentCallback = loadPercentCallback;
        this.onComplateCallback = complateCallback;
        this.asyncCoroutine = StartCoroutine(LoadAsyncScene());
    }

    private IEnumerator LoadAsyncScene()
    {
        // �⺻ Load ������ �̵��Ѵ�. (��� �� �Ȱɸ����� Load���� ���� ������ �𸣹Ƿ� �񵿱� �ε�� ó���Ѵ�.)
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneType.Load.ToString());
        while (!asyncLoad.isDone)
            yield return null;

        // Load������ �̵� �Ϸ��� ���� �̵��� ���� �ε��Ѵ�.
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
