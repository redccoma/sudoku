using System.IO;
using Newtonsoft.Json;

#if UNITY_EDITOR
public static class CreateDataManager
{
    private const string SUDOKU_JSON_FILE_NAME = "data.json";

    public static void Load()
    {

    }

    public static void Save(SudokuModel model)
    {
        string path = string.Format("Assets/Resources/GameJSONData/{0}", SUDOKU_JSON_FILE_NAME);

        string _json = JsonConvert.SerializeObject(model);

        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.Write(_json);
            }
        }

        UnityEditor.AssetDatabase.Refresh();
    }
}
#endif