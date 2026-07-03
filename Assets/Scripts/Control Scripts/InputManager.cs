using UnityEngine;

public class InputManager
{
    private Vector3 _aimTargetPosition;
    public Vector3 AimTargetPosition => _aimTargetPosition;

    private bool _isFireButtonPressed;
    public bool IsFireButtonPressed => _isFireButtonPressed;

    public InputManager()
    {
        Debug.Log("Custom InputManager initialized.");
    }

    public void UpdateInput()
    {
        _isFireButtonPressed = Input.GetMouseButton(0);

        if (_isFireButtonPressed)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
            {
                _aimTargetPosition = hit.point;
            }
            else if (Physics.Raycast(ray, out hit))
            {
                _aimTargetPosition = hit.point;
            }
        }
    }
}