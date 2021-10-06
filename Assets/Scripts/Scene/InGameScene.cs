using UnityEngine;

public class InGameScene : MonoBehaviour
{
    public MatrixControl matricControl;

    private void Awake()
    {
        // 글로벌 이벤트 등록
        BroadcastTunnel<string, SCREEN_ORIENTATION_TYPE>.Add("ScreenRotate", OnScreenRotate);
    }

    private void Start()
    {
        // matricControl.CreateMatrix(MATRIX_TYPE.NINE_NINE);
    }

    // 화면 회전 글로벌 이벤트 리스너
    private void OnScreenRotate(SCREEN_ORIENTATION_TYPE type)
    {
        Debug.Log(type.ToString());
    }

    private void OnDestroy()
    {
        // 글로벌 이벤트 해제
        BroadcastTunnel<string, SCREEN_ORIENTATION_TYPE>.RemoveAllByKey("ScreenRotate");
    }
}