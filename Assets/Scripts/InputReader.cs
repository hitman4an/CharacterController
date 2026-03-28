using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Vertical = "Vertical";
    private const string Horizontal = "Horizontal";

    public float GetVertical()
    {
        return Input.GetAxis(Vertical);
    }

    public float GetHorizontal()
    {
        return Input.GetAxis(Horizontal);
    }
}