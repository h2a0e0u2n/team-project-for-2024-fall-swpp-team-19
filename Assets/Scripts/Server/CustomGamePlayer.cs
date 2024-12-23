using Mirror;
using UnityEngine;

public class CustomGamePlayer : NetworkBehaviour
{
    [SyncVar] public bool isInMiniGame = false;
    public PlayerInputData InputData = new PlayerInputData();

    private void Update()
    {
        if (!isLocalPlayer || !isInMiniGame) return;

        PlayerInputData newInput = new PlayerInputData
        {
            MousePosition = Input.mousePosition,
            IsMouseClicked = Input.GetMouseButtonDown(0),
            IsMouseReleased = Input.GetMouseButtonUp(0),
            IsMovingUp = Input.GetKey(KeyCode.W),
            IsMovingDown = Input.GetKey(KeyCode.S),
            IsMovingLeft = Input.GetKey(KeyCode.A),
            IsMovingRight = Input.GetKey(KeyCode.D),
            IsJumping = Input.GetKey(KeyCode.Space),
            IsInteracting = Input.GetKeyDown(KeyCode.E),
            IsInteractionReleased = Input.GetKeyUp(KeyCode.E)
        };

        CmdUpdateInput(newInput);
    }

    [Command]
    private void CmdUpdateInput(PlayerInputData input)
    {
        InputData = input;
        Debug.Log($"[CustomGamePlayer] Server received new input from Player {netId}: " +
                  $"Left={input.IsMovingLeft}, Right={input.IsMovingRight}, Interact={input.IsInteracting}");
    }
}
