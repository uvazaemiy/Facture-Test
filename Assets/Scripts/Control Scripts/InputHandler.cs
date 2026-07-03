using UnityEngine;
using VContainer;

public class InputHandler : MonoBehaviour
{
    private InputManager _inputManager;

    [Inject]
    public void Construct(InputManager inputManager)
    {
        _inputManager = inputManager;
    }

    void Update()
    {
        _inputManager.UpdateInput();
    }
}