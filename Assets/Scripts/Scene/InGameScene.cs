using UnityEngine;

public class InGameScene : MonoBehaviour
{
    public MatrixControl matricControl;

    public GameObject portraitCanvas;   // 세로전용 캔버스
    public GameObject landscapeCanvas;  // 가로전용 캔버스

    private void Awake()
    {
        // 글로벌 이벤트 등록
        BroadcastTunnel<string, SCREEN_ORIENTATION_TYPE>.Add("ScreenRotate", OnScreenRotate);

        portraitCanvas.SetActive(ScreenControl.GetScreenOrientation == SCREEN_ORIENTATION_TYPE.Portrait);
        landscapeCanvas.SetActive(ScreenControl.GetScreenOrientation == SCREEN_ORIENTATION_TYPE.Landscape);
    }

    private void Start()
    {
        matricControl.CreateMatrix(DataManager.Instance.GetStageData(DataManager.Instance.GetSelectedNumber));
    }

    // 화면 회전 글로벌 이벤트 리스너
    private void OnScreenRotate(SCREEN_ORIENTATION_TYPE type)
    {
        if(type != SCREEN_ORIENTATION_TYPE.UnKnowun)
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
}