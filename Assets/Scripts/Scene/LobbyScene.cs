using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class LobbyScene : MonoBehaviour
{
    private readonly int PORTRAIT_ITEM_COUNT = 25;
    private readonly int LANDSCAPE_ITEM_COUNT = 30;

    public GameObject portraitCanvas;   // 세로전용 캔버스
    public GameObject landscapeCanvas;  // 가로전용 캔버스
    public GameObject stageItemPrefab;  // 스테이지 선택 전용 프리팹
    public Transform portraitParent;    // 프리팹 생성 위치(세로)
    public Transform landscapeParent;   // 프리팹 생성 위치(가로)
    public GameObject portraitLeftButton;   // 왼쪽 페이지 이동 버튼 (세로)
    public GameObject portraitRightButton;  // 오른쪽 페이지 이동 버튼 (세로)
    public GameObject landscapeLeftButton;  // 오른쪽 페이지 이동 버튼 (가로)
    public GameObject landscapeRightButton;  // 오른쪽 페이지 이동 버튼 (가로)

    private List<StageButtonItem> portraitItems;
    private List<StageButtonItem> landscapeItems;

    private int currentPage = 1;    // 현재 스테이지 전환 버튼
    private int maxPortraitPage = 0;
    private int maxLandscapePage = 0;

    #region MonoBehaviour
    private void Awake()
    {
        // 글로벌 이벤트 등록
        BroadcastTunnel<string, SCREEN_ORIENTATION_TYPE>.Add("ScreenRotate", OnScreenRotate);

        CreateItems();
        ClacMaxPage();
    }

    private void Start()
    {
        SetPageState();

        portraitCanvas.SetActive(ScreenControl.GetScreenOrientation == SCREEN_ORIENTATION_TYPE.Portrait);
        landscapeCanvas.SetActive(ScreenControl.GetScreenOrientation == SCREEN_ORIENTATION_TYPE.Landscape);
    }
    #endregion

    #region Private
    /// <summary>
    /// 스테이지용 아이템 생성 (씬 진입후 1회 사용)
    /// </summary>
    private void CreateItems()
    {
        portraitItems = new List<StageButtonItem>();
        for (int i = 0; i < PORTRAIT_ITEM_COUNT; i++)
        {
            GameObject _instance = GameObject.Instantiate(stageItemPrefab, portraitParent) as GameObject;
            portraitItems.Add(_instance.GetComponent<StageButtonItem>());
        }

        landscapeItems = new List<StageButtonItem>();
        for (int i = 0; i < LANDSCAPE_ITEM_COUNT; i++)
        {
            GameObject _instance = GameObject.Instantiate(stageItemPrefab, landscapeParent) as GameObject;
            landscapeItems.Add(_instance.GetComponent<StageButtonItem>());
        }
    }

    /// <summary>
    /// maxPage 수치 구하기.
    /// </summary>
    private void ClacMaxPage()
    {
        int[] stages = DataManager.Instance.GetStages();

        maxPortraitPage = 1;
        if (stages.Length > PORTRAIT_ITEM_COUNT)
        {
            maxPortraitPage = (stages.Length / PORTRAIT_ITEM_COUNT) + (stages.Length % PORTRAIT_ITEM_COUNT > 0 ? 1 : 0);
            // maxPortraitPage--;
        }

        maxLandscapePage = 1;
        if (stages.Length > LANDSCAPE_ITEM_COUNT)
        {
            maxLandscapePage = (stages.Length / LANDSCAPE_ITEM_COUNT) + (stages.Length % LANDSCAPE_ITEM_COUNT > 0 ? 1 : 0);
            // maxLandscapePage--;
        }
    }

    /// <summary>
    /// 현재 페이지 숫자에 따른 아이템의 상태를 설정
    /// </summary>
    private void SetPageState()
    {
        int[] stages = DataManager.Instance.GetStages();

        int startPortraitIndex = (PORTRAIT_ITEM_COUNT * currentPage) - PORTRAIT_ITEM_COUNT;
        int startLandscapeIndex = (LANDSCAPE_ITEM_COUNT * currentPage) - LANDSCAPE_ITEM_COUNT;

        for (int i = 0; i < portraitItems.Count; i++)
        {
            if (startPortraitIndex < stages.Length)
            {
                portraitItems[i].GetGameObject.SetActive(true);
                portraitItems[i].SetData(stages[startPortraitIndex++]);
            }
            else
            {
                portraitItems[i].GetGameObject.SetActive(false);
            }
        }

        for (int i = 0; i < landscapeItems.Count; i++)
        {
            if (startLandscapeIndex < stages.Length)
            {
                landscapeItems[i].GetGameObject.SetActive(true);
                landscapeItems[i].SetData(stages[startLandscapeIndex++]);
            }
            else
            {
                landscapeItems[i].GetGameObject.SetActive(false);
            }
        }

        if (maxPortraitPage == 1)
        {
            portraitLeftButton.SetActive(false);
            portraitRightButton.SetActive(false);
        }
        else if(maxPortraitPage > 1)
        {
            if(currentPage == 1)
            {
                portraitLeftButton.SetActive(false);
                portraitRightButton.SetActive(true);
            }
            else if(currentPage == maxPortraitPage)
            {
                portraitLeftButton.SetActive(true);
                portraitRightButton.SetActive(false);
            }
            else
            {
                portraitLeftButton.SetActive(true);
                portraitRightButton.SetActive(true);
            }
        }

        if (maxLandscapePage == 1)
        {
            landscapeLeftButton.SetActive(false);
            landscapeRightButton.SetActive(false);
        }
        else if (maxLandscapePage > 1)
        {
            if (currentPage == 1)
            {
                landscapeLeftButton.SetActive(false);
                landscapeRightButton.SetActive(true);
            }
            else if (currentPage == maxLandscapePage)
            {
                landscapeLeftButton.SetActive(true);
                landscapeRightButton.SetActive(false);
            }
            else
            {
                landscapeLeftButton.SetActive(true);
                landscapeRightButton.SetActive(true);
            }
        }
    }

    /// <summary>
    /// 화면 회전 글로벌 이벤트 리스너
    /// </summary>
    /// <param name="type">현재 화면의 회전 상태</param>
    private void OnScreenRotate(SCREEN_ORIENTATION_TYPE type)
    {
        if (type != SCREEN_ORIENTATION_TYPE.UnKnowun)
        {
            portraitCanvas.SetActive(type == SCREEN_ORIENTATION_TYPE.Portrait);
            landscapeCanvas.SetActive(type == SCREEN_ORIENTATION_TYPE.Landscape);
        }
    }

    private void OnDestroy()
    {
        // 글로벌 이벤트 해제
        BroadcastTunnel<string, SCREEN_ORIENTATION_TYPE>.RemoveAllByKey("ScreenRotate");
    }
    #endregion

    #region Public
    /// <summary>
    /// 왼쪽 페이지 이동 버튼 함수
    /// </summary>
    public void OnClickLeft()
    {
        currentPage--;
        SetPageState();
    }

    /// <summary>
    /// 오른쪽 페이지 이동 버튼 함수
    /// </summary>
    public void OnClickRight()
    {
        currentPage++;
        SetPageState();
    }
    #endregion


}