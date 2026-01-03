using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;

    private PlayerMotor motor;
    private PlayerLook look;
    private PlayerShoot shoot;
    private SpellManager spellManager;

    private void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        shoot = GetComponent<PlayerShoot>();
        spellManager = GetComponent<SpellManager>();

        // Movement
        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.Sprint.performed += ctx => motor.Sprint();
        onFoot.Sprint.canceled += ctx => motor.Walk();

        // Shooting
        onFoot.Shoot.performed += ctx => shoot.StartFiring();
        onFoot.Shoot.canceled += ctx => shoot.StopFiring();

        // Switch spells
        onFoot.ChangeWeapon.performed += ctx => spellManager.NextSpell();
    }
}
