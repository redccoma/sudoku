/// <summary>
/// 화면의 회전 상태
/// </summary>
public enum SCREEN_ORIENTATION_TYPE
{
    /// <summary>
    /// 알수없음.
    /// </summary>
    UnKnowun,
    /// <summary>
    /// 세로
    /// </summary>
    Portrait,
    /// <summary>
    /// 가로
    /// </summary>
    Landscape
}

/// <summary>
/// JSON 데이터에 들어갈 종류
/// </summary>
public enum JSON_TYPE
{
    /// <summary>
    /// 문제 (만들때 체크된 붉은색 폰트숫자로 유저가 입력해야 할 값)
    /// </summary>
    Question,
    /// <summary>
    /// 정답 (입력된 모든 숫자)
    /// </summary>
    Answer,
}

/// <summary>
/// 씬의 종류
/// </summary>
public enum SCENE_TYPE
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
    /// 로비 씬
    /// </summary>
    Lobby,
    /// <summary>
    /// 인게임 씬
    /// </summary>
    InGame
}

/// <summary>
/// JSON 원형 데이터를 만들때 사용하는 문제의 타입
/// 오직 CREATE DATA 씬에서만 사용한다.
/// </summary>
public enum CRAETE_STATE
{
    INVALID,
    /// <summary>
    /// 기존 문제 수정상태
    /// </summary>
    EDIT,
    /// <summary>
    /// 새로운 문제 만들기 상태
    /// </summary>
    NEW,
}

/// <summary>
/// 스도쿠의 타입
/// </summary>
public enum MATRIX_TYPE
{
    /// <summary>
    /// 9 * 9 스도쿠 타입
    /// </summary>
    NINE_NINE,
}