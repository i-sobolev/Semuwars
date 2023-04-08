using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Character _character;

    private void Update()
    {
        float inputHorizontal = Input.GetAxisRaw("Horizontal");
        _character.Movement.Move(inputHorizontal);

        if (Input.GetKeyDown(KeyCode.W))
            _character.Movement.Jump();

        if (Input.GetMouseButtonDown(0))
            _character.Combat.MeleeAttack(_character);

        if (Input.GetMouseButtonDown(1))
            _character.Combat.ThrowKunai(_character);
    }
}
