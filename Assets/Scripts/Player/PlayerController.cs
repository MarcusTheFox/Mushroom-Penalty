using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;


[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private IMovable movement;
    private MeleeAttack meleeAttack;
    private MagicAttack magicAttack;
    
    private Vector2 moveInput;
    private bool runInput;
    private bool meleeAttackInput;
    private bool magicAttackInput;
    private bool canMove = true;

    public GameObject EscMenu;
    private bool IsGamePaused;

    [SerializeField] private PlayerCharacter playerCharacter;
    private PlayerSaveInteractor interactor;
    private List<Enemy> enemies = new List<Enemy>();

    private void Awake()
    {
        var repository = new SaveRepository();
        interactor = new PlayerSaveInteractor(repository);
        enemies = new List<Enemy>(FindObjectsOfType<Enemy>());

        playerInput = GetComponent<PlayerInput>();
        movement = GetComponent<IMovable>();
        
        if (playerInput == null)
        {
            Debug.LogError("PlayerInput not found on Player", this);
            enabled = false;
            return;
        }
        
        if (movement == null)
        {
            Debug.LogError("No IMovable found on Player!", this);
            enabled = false;
            return;
        }
        
        meleeAttack = GetComponent<MeleeAttack>();
        magicAttack = GetComponent<MagicAttack>();

        if (meleeAttack == null)
        {
            Debug.LogWarning("MeleeAttack not fount on Player", this);
        }
        
        if (magicAttack == null)
        {
            Debug.LogWarning("MagicAttack not found on Player", this);
        }
    }

    public void OnMenu(InputValue value)
    {
        if (IsGamePaused)
        {
            OnMenuExit();
        }
        else
        {
            OnMenuEnter();
        }
    }

    public void OnMenuEnter()
    {
        EscMenu.SetActive(true);
        Time.timeScale = 0.0f;
        IsGamePaused=true;
        Debug.Log("Escape entered!");
    }
    public void OnMenuExit()
    {
        EscMenu.SetActive(false);
        Time.timeScale =1.0f;
        IsGamePaused=false;
        Debug.Log("Continue pressed!");
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnRun(InputValue value)
    {
        runInput = value.isPressed;
    }

    public void OnMeleeAttack(InputValue value)
    {
        meleeAttackInput = value.isPressed;
    }

    public void OnMagicAttack(InputValue value)
    {
        magicAttackInput = value.isPressed;
    }

    public void OnSave(InputValue value)
    {
        if (value.isPressed)
        {
            OnGameSave();
        }
    }

    public void OnGameSave()
    {
        Vector3 playerPos = playerCharacter.transform.position;
        float playerHp = playerCharacter.GetCurrentHealth();

        interactor.SaveGame(playerPos, playerHp, enemies);

        Debug.Log("Game saved!");
    }

    public void OnLoad(InputValue value)
    {
        if (value.isPressed)
        {
            OnGameLoad();
        }
    }

    public void OnGameLoad()
    {
        var data = interactor.LoadGame();
        if (data == null)
        {
            Debug.LogWarning("No save found!");
            return;
        }

        // Восстановление игрока
        playerCharacter.transform.position = new Vector3(data.playerPosition[0], data.playerPosition[1], data.playerPosition[2]);
        float deltaPlayerHp = data.playerHp - playerCharacter.GetCurrentHealth();
        if (deltaPlayerHp > 0)
            playerCharacter.HealthSystem.Heal(deltaPlayerHp);
        else if (deltaPlayerHp < 0)
            playerCharacter.HealthSystem.TakeDamage(-deltaPlayerHp);

        // Восстановление мобов
        int count = Mathf.Min(enemies.Count, data.enemies.Count);
        for (int i = 0; i < count; i++)
        {
            enemies[i].transform.position = new Vector3(data.enemies[i].position[0], data.enemies[i].position[1], data.enemies[i].position[2]);

            float deltaHp = data.enemies[i].hp - enemies[i].HealthSystem.CurrentHealth;
            if (deltaHp > 0)
                enemies[i].HealthSystem.Heal(deltaHp);
            else if (deltaHp < 0)
                enemies[i].HealthSystem.TakeDamage(-deltaHp);
        }

        Debug.Log("Game loaded!");
    }



    private void Update()
    {
        HandleMovement();
        HandleAttack();
    }

    private void HandleMovement()
    {
        if (moveInput.magnitude > 0 && canMove)
        {
            Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;
            movement.Move(moveDirection);

            if (movement is PlayerMovement playerMovement)
            {
                playerMovement.SetRunning(runInput);
            }
        }
        else
        {
            movement.Stop();
        }
    }

    private void HandleAttack()
    {
        if (meleeAttackInput)
        {
            if(meleeAttack != null)
                meleeAttack.PerformAttack();
            meleeAttackInput = false;
        }
        else if (magicAttackInput)
        {
            if(magicAttack != null)
                magicAttack.PerformAttack();
            magicAttackInput = false;
        }
    }

    public void EnableMovement()
    {
        canMove = true;
    }

    public void DisableMovement()
    {
        canMove = false;
    }

    public void DisableInput()
    {
        playerInput.enabled = false;
    }
}