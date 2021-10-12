using System;
using System.IO;

using UnityEngine;

using Newtonsoft.Json;

public class DataManager : Singleton<DataManager>
{
    /// <summary>
    /// 클라이언트에서 사용될 메인 데이터
    /// </summary>
    private static SudokuModel SUDOKU_DATA;
    /// <summary>
    /// 문제 + 유저입력 데이터
    /// </summary>
    private static UserModel USER_DATA;
    /// <summary>
    /// 런타임으로 변경되는 데이터이므로, Resources가 아닌 아래의 패스에 별도로 저장한다.
    /// </summary>
    private string USER_DATA_PATH;
    /// <summary>
    /// 클라이언트 내부에 저장된 문제 및 정답 JSON PATH
    /// </summary>
    private string CLIENT_DATA_PATH;

    /// <summary>
    /// 현재 선택된 스테이지
    /// </summary>
    private int selectedQuestionNumber;
    public int GetSelectedNumber
    {
        get
        {
            return selectedQuestionNumber;
        }
    }

    public override void Awake()
    {
        base.Awake();

        USER_DATA_PATH = Application.persistentDataPath + "/md/data.json";
        CLIENT_DATA_PATH = "JsonData/data";
    }

    /// <summary>
    /// 초기화 및 사용자 데이터 로드.
    /// </summary>
    /// <param name="complate">초기화 성공/실패여부 콜백</param>
    public void Init(Action<bool> complate)
    {
        if (!LoadClientData())
        {
            complate?.Invoke(false);
            return;
        }
        else
        {
            if (!LoadUserData())
            {
                complate?.Invoke(false);
                return;
            }
        }

        complate?.Invoke(true);
    }

    /// <summary>
    /// 스테이지 전체 번호 얻기
    /// </summary>
    /// <returns></returns>
    public int[] GetStages()
    {
        int[] result = new int[SUDOKU_DATA.data.Keys.Count];

        int index = 0;
        foreach (int item in SUDOKU_DATA.data.Keys)
            result[index++] = item;

        return result;
    }

    /// <summary>
    /// 로비에서 스테이지 버튼 클릭시
    /// </summary>
    public void GoStage(int qustionNumber)
    {
        selectedQuestionNumber = qustionNumber;
        SceneManager.Instance.ChangeScene(SCENE_TYPE.InGame);
    }

    /// <summary>
    /// 특정 스테이지의 문제 데이터 얻기
    /// </summary>
    /// <param name="stageNumber"></param>
    public Question GetStageData(int stageNumber)
    {
        return SUDOKU_DATA.GetQuestion(stageNumber);
    }

    /// <summary>
    /// 특정 스테이지의 유저 입력 데이터 얻기
    /// </summary>
    /// <param name="stageNumber"></param>
    /// <returns></returns>
    public UserInputData GetUserData(int stageNumber)
    {
        return USER_DATA.GetData(stageNumber);
    }

    #region PRIVATE 
    /// <summary>
    /// 클라이언트 데이터 로드
    /// </summary>
    private bool LoadClientData()
    {
        try
        {
            TextAsset _textAsset = Resources.Load<TextAsset>(CLIENT_DATA_PATH);
            SudokuModel _model = JsonConvert.DeserializeObject<SudokuModel>(_textAsset.text);
            SUDOKU_DATA = _model;

            return true;
        }
        catch(SystemException e)
        {
            Debug.Log(e.ToString());

            return false;
        }
    }

    /// <summary>
    /// 유저 데이터 로드
    /// </summary>
    private bool LoadUserData()
    {
        if (File.Exists(USER_DATA_PATH))
        {
            try
            {
                string json = File.ReadAllText(USER_DATA_PATH);
                USER_DATA = JsonConvert.DeserializeObject<UserModel>(json);

                return true;
            }
            catch (SystemException e)
            {
                Debug.Log(e.ToString());
                // TODO
                // 에러처리는 추후에..

                return false;
            }
        }
        else
        {
            // 파일이 없는 경우 UserData 초기화하여 인스턴스 생성.
            if (SUDOKU_DATA != null)
            {
                USER_DATA = new UserModel(SUDOKU_DATA);
                return true;
            }
            else
            {
                // TODO
                // 에러처리는 추후에..

                return false;
            }
        }
    }
    #endregion
}
