using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Editor에서만 실행될 스크립트
/// Window OS 환경만 고려하여 작업되었음. OSX 고려안됨
/// 이 스크립트는 배포용이 아닌 json을 만들기 위한 목적만 가지고 있으므로
/// 스크립트 관리 규약과는 상관없이 왠만한 기능은 이쪽에서 구현한다.
/// </summary>
public class CreateDataControl : MonoBehaviour
{   
    private bool isLoadedJson = false;  // 문제 수정 및 생성전 무조건 json을 로드하게끔한다.

    private string SAVE_PATH = string.Empty;
    private string LOAD_PATH = "JsonData/data";

    public InputField editQuestionNumberField;  // 기존 문제 번호 인풋필드
    public InputField newQuestionNumberField;   // 신규 문제 번호 인풋필드

    public GameObject prefab;       // 스도쿠 메인 패널에 위치할 각 아이템의 프리팹 원형
    public Transform prefabParent;  // 위 프리팹을 생성할 아이템의 부모

    private SudokuModel model;  // 전체 스도쿠 데이터
    private InputItem[,] items; // 현재 패널에 생성된 인풋아이템의 프리팹 모음배열
    private int selectedNumber; // 현재 패널의 문제번호

    private CRAETE_STATE currentState = CRAETE_STATE.INVALID; // 현재 상태

    private void Awake()
    {
        SAVE_PATH = Application.dataPath + "/Resources/JsonData/data.json";
    }

    private void Start()
    {
        CreateInputFields();
        SetInputFieldsInitialize();
    }

    /// <summary>
    /// 9 * 9 스도쿠 고정. 추후 기능 확장(6 * 6등)될 가능성 있음.
    /// </summary>
    private void CreateInputFields()
    {
        items = new InputItem[9 ,9];

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                GameObject _instance = GameObject.Instantiate(prefab, prefabParent) as GameObject;
                _instance.name = string.Format("{0}_{1}", i, j);

                // 생성한 인스턴스들 저장.
                items[i, j] = _instance.GetComponent<InputItem>();
            }
        }
    }

    /// <summary>
    /// 현재 생성된 스도쿠 패널의 값 초기화
    /// </summary>
    private void SetInputFieldsInitialize()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                items[i, j].SetInit();
            }
        }
    }

    /// <summary>
    /// 현재 생성된 input Field에 데이터를 셋팅한다.
    /// </summary>
    /// <param name="questionNumber">문제 번호</param>
    /// <returns>셋팅 성공여부</returns>
    private bool SetInputFields(int questionNumber)
    {
        if (model == null)
        {
            Debug.Log(string.Format("<color=red>{0}</color>", "json이 로드되지 않았습니다. 새로운 문제를 만들거나, json을 로드해주세요."));
        }
        else
        {
            if (!model.IsContainKey(questionNumber))
            {
                Debug.Log(string.Format("<color=red>{0}</color>", "생성된 문제가 없습니다. 새로 만들어주세요."));
            }
            else
            {
                for (short i = 0; i < 9; i++)
                {
                    for (short j = 0; j < 9; j++)
                    {
                        Question _q = model.GetQuestion(questionNumber);

                        int curNumber = _q.a[i, j];
                        bool isQuestion = _q.IsQuestionItem(i, j);

                        items[i, j].SetData(curNumber, isQuestion);
                    }
                }

                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 파일이 없는 경우 반환값은 null
    /// </summary>
    /// <returns></returns>
    public void Load()
    {
        if (File.Exists(SAVE_PATH))
        {
            TextAsset _textAsset = Resources.Load<TextAsset>(LOAD_PATH);
            SudokuModel _model = JsonConvert.DeserializeObject<SudokuModel>(_textAsset.text);
            model = _model;

            Debug.Log(string.Format("<color=white>{0}</color>", "데이터 로드 성공"));
        }
        else
        {
            Debug.Log(string.Format("<color=red>{0}</color>", "데이터 로드 실패. JSON 파일 없음"));
            model = new SudokuModel();
        }

        isLoadedJson = true;
    }

    /// <summary>
    /// 현재의 데이터 그대로 저장
    /// </summary>
    /// <param name="model"></param>
    public void Save()
    {
#if UNITY_EDITOR
        if (!isLoadedJson)
        {
            Debug.Log(string.Format("<color=red>{0}</color>", "전체 데이터 로드부터 시작해주세요."));
            return;
        }

        if (model != null)
        {
            string _json = JsonConvert.SerializeObject(model);

            try
            {
                // 기존 파일은 지운다.
                if (File.Exists(SAVE_PATH))
                    File.Delete(SAVE_PATH);

                File.WriteAllText(SAVE_PATH, _json);
            }
            catch (System.SystemException e)
            {
                Debug.Log(string.Format("<color=red>{0}</color>", e.ToString()));
            }

            UnityEditor.AssetDatabase.Refresh();

            Debug.Log(string.Format("<color=white>{0}</color>", "내 마음속에 저★장★ 완료!"));
        }
        else
        {
            Debug.Log(string.Format("<color=white>{0}</color>", "로드된 데이터가 없습니다"));
        }
#endif
    }

    /// <summary>
    /// 기존 문제 로드 버튼 연결함수
    /// </summary>
    public void OnClick_EditLoad()
    {
        if (!isLoadedJson)
        {
            Debug.Log(string.Format("<color=red>{0}</color>", "전체 데이터 로드부터 시작해주세요."));
            return;
        }

        int number = 0;
        bool isParse = int.TryParse(editQuestionNumberField.text, out number);

        if(isParse)
        {
            if(SetInputFields(number))
            {
                // 셋팅 성공 했으므로 현재 문제번호 저장.
                selectedNumber = number;
                currentState = CRAETE_STATE.EDIT;
            }
            else
            {
                // 셋팅 실패 했으므로 초기화
                selectedNumber = 0;
                SetInputFieldsInitialize();
                currentState = CRAETE_STATE.INVALID;
            }
        }
    }

    /// <summary>
    /// 기존 문제 저장 버튼 연결함수
    /// </summary>
    public void OnClick_EditSave()
    {
        if (!isLoadedJson)
        {
            Debug.Log(string.Format("<color=red>{0}</color>", "전체 데이터 로드부터 시작해주세요."));
            return;
        }

        if (currentState == CRAETE_STATE.EDIT && selectedNumber > 0)
        {
            for (short i = 0; i < 9; i++)
            {
                for (short j = 0; j < 9; j++)
                {
                    int curNumber = items[i, j].GetData();
                    bool isQuestion = items[i, j].IsQuestion;

                    if (isQuestion)
                        model.data[selectedNumber].SetData(JSON_TYPE.Question, i, j, curNumber);
                    else
                        model.data[selectedNumber].ResetQuestionData(i, j);

                    model.data[selectedNumber].SetData(JSON_TYPE.Answer, i, j, curNumber);
                }
            }

            // Json 반영
            Save();
        }
        else
        {
            Debug.Log(string.Format("<color=red>{0}</color>", "문제상태가 아니므로 저장불가."));
            currentState = CRAETE_STATE.INVALID;
        }
    }

    /// <summary>
    /// 신규 문제 만들기 버튼 연결 함수
    /// </summary>
    public void OnClick_NewQuestionCreate()
    {
        if (!isLoadedJson)
        {
            Debug.Log(string.Format("<color=red>{0}</color>", "전체 데이터 로드부터 시작해주세요."));
            return;
        }

        if (string.IsNullOrEmpty(newQuestionNumberField.text))
        {
            Debug.Log(string.Format("<color=red>{0}</color>", "문제 번호를 입력하세요."));
            return;
        }

        int number = 0;
        bool isParse = int.TryParse(newQuestionNumberField.text, out number);

        if (isParse)
        {
            selectedNumber = number;
            currentState = CRAETE_STATE.NEW;
            Debug.Log(string.Format("<color=white>{0}</color>", "새로운 문제 만들기 준비 완료."));
        }
        else
        {
            Debug.Log(string.Format("<color=red>{0}</color>", "문제 번호를 정확히 입력해주세요."));
            selectedNumber = 0;
            currentState = CRAETE_STATE.INVALID;
        }

        SetInputFieldsInitialize();
    }

    /// <summary>
    /// 신규 문제 저장 버튼 연결함수
    /// </summary>
    public void OnClick_NewQuestionSave()
    {
        if (!isLoadedJson)
        {
            Debug.Log(string.Format("<color=red>{0}</color>", "전체 데이터 로드부터 시작해주세요."));
            return;
        }

        if (currentState == CRAETE_STATE.NEW && selectedNumber > 0)
        {
            Question _q = new Question(9);

            for (short i = 0; i < 9; i++)
            {
                for (short j = 0; j < 9; j++)
                {
                    int curNum = items[i,j].GetData();
                    bool isQuestion = items[i, j].IsQuestion;

                    if (isQuestion)
                        _q.SetData(JSON_TYPE.Question, i, j, curNum);

                    _q.SetData(JSON_TYPE.Answer, i, j, curNum);
                }
            }

            model.AddData(selectedNumber, _q);
        }
        else
        {
            Debug.Log(string.Format("<color=red>{0}</color>", "문제상태가 아니므로 저장불가."));

            selectedNumber = 0;
            currentState = CRAETE_STATE.INVALID;
        }
    }
}