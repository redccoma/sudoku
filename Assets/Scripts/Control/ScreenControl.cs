using UnityEngine;

/// <summary>
/// 화면 회전을 체크하여 현재의 화면 상태를 글로벌 이벤트로 Notify한다.
/// </summary>
public class ScreenControl : MonoBehaviour
{
    /// <summary>
    /// 현재 화면의 상태.
    /// </summary>
    private static SCREEN_ORIENTATION_TYPE currentOrientation;

    public static SCREEN_ORIENTATION_TYPE GetScreenOrientation
    {
        get { return currentOrientation; }
    }

    private void Awake()
    {
        currentOrientation = this.Convert(Screen.orientation);
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Screen.height > Screen.width)
        {
            if (currentOrientation != SCREEN_ORIENTATION_TYPE.Portrait)
            {
                currentOrientation = SCREEN_ORIENTATION_TYPE.Portrait;
                BroadcastTunnel<string, SCREEN_ORIENTATION_TYPE>.Notify("ScreenRotate", currentOrientation);
            }
        }
        else
        {
            if (currentOrientation != SCREEN_ORIENTATION_TYPE.Landscape)
            {
                currentOrientation = SCREEN_ORIENTATION_TYPE.Landscape;
                BroadcastTunnel<string, SCREEN_ORIENTATION_TYPE>.Notify("ScreenRotate", currentOrientation);
            }
        }
#else
        SCREEN_ORIENTATION_TYPE check = this.Convert(Screen.orientation);

        if (check != SCREEN_ORIENTATION_TYPE.UnKnowun && check != currentOrientation)
        {
            currentOrientation = check;
            BroadcastTunnel<string, SCREEN_ORIENTATION_TYPE>.Notify("ScreenRotate", currentOrientation);
        }
#endif  
    }

    /// <summary>
    /// 유니티에서 제공하는 화면 회전 상태값을 클라가 별도로 제어하는 상태값으로 전환
    /// </summary>
    /// <param name="so"></param>
    /// <returns></returns>
    private SCREEN_ORIENTATION_TYPE Convert(ScreenOrientation so)
    {
#if UNITY_EDITOR
        if (Screen.height > Screen.width)
            return SCREEN_ORIENTATION_TYPE.Portrait;
        else
            return SCREEN_ORIENTATION_TYPE.Landscape;
#else
        if (so == ScreenOrientation.Landscape || so == ScreenOrientation.LandscapeLeft || so == ScreenOrientation.LandscapeRight)
            return SCREEN_ORIENTATION_TYPE.Landscape;
        else if (so == ScreenOrientation.Portrait || so == ScreenOrientation.PortraitUpsideDown)
            return SCREEN_ORIENTATION_TYPE.Portrait;
        else
            return SCREEN_ORIENTATION_TYPE.UnKnowun;
#endif
    }
}
