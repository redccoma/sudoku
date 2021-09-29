using UnityEngine;

public class MainScene : MonoBehaviour
{
    public MatrixControl matricControl;

    private void Start()
    {
        matricControl.CreateMatrix(MatrixControl.MatrixType.NINE_NINE);
    }
}