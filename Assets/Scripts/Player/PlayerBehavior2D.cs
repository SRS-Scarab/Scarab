#nullable enable
using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerBehavior2D : StateMachine, IFragmentSaveable
{
    private const string ID = "player-behavior";

    [SerializeField]
    private InputSubsystem inputSubsystem = null!;

    private Actions Actions => actionsVar.Provide();

    [SerializeField]
    private ActionsVariable actionsVar = null!;

    [SerializeField]
    private InventorySubsystem hotbarSubsystem = null!;

    [SerializeField]
    private InventoryVariable hotbarInventory = null!;

    [SerializeField]
    private CameraVariable camVar = null!;
    
    [SerializeField]
    private SaveablePrefabList prefabList = null!;

    [SerializeField]
    private CombatEntity entity = null!;

    [SerializeField]
    private new Rigidbody2D rigidbody = null!;

    [SerializeField]
    private AttackInfo basicAttack;

    [SerializeField]
    private AttackInfo specialAttack;

    [SerializeField]
    private float actionCooldown = 0.5f;

    [SerializeField]
    private float specialCooldown = 3;

    private float MoveSpeed => Actions.Gameplay.Sprint.IsPressed() ? sprintSpeed : walkSpeed;

    [SerializeField]
    private float walkSpeed = 5;

    [SerializeField]
    private float sprintSpeed = 10;

    [SerializeField]
    private float dashForce = 10;

    [SerializeField]
    private float dashDecay = 3;

    [SerializeField]
    private float actionCooldownRemaining;

    [SerializeField]
    private Image? actionCooldownIcon;

    [SerializeField]
    private float specialCooldownRemaining;

    [SerializeField]
    private Image? specialCooldownIcon;

    [SerializeField]
    private Vector2 moveDirection = Vector2.right;

    [SerializeField]
    private float selectedIndex;

    protected override void Start()
    {
        base.Start();
        entity = GetComponent<CombatEntity>();
        rigidbody = GetComponent<Rigidbody2D>();

        AssertDependencies();

        hotbarSubsystem.OnSlotSelected(hotbarInventory.Provide(), (int)selectedIndex);
    }

    protected override void Update()
    {
        base.Update();

        var num = hotbarInventory.Provide().GetMaxSlots();
        var delta = actionsVar.Provide().Gameplay.HotbarScroll.ReadValue<float>() / 10;
        selectedIndex = (selectedIndex + delta + num) % num;
        hotbarSubsystem.OnSlotSelected(hotbarInventory.Provide(), (int)selectedIndex);

        if (actionsVar.Provide().Gameplay.Load.WasPerformedThisFrame())
        {
            var fileInfo = new FileInfo(Application.persistentDataPath + "/save.txt");
            if (fileInfo.Exists)
            {
                var serialized = File.ReadAllText(fileInfo.FullName);
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    PreserveReferencesHandling = PreserveReferencesHandling.All
                };
                var data = JsonConvert.DeserializeObject<SaveDataBase>(serialized, settings);
                SaveUtility.Load(data!, prefabList);
            }
        }
    }

    protected override State GetInitialState()
    {
        return new MoveState(this);
    }

    private void OnAction()
    {
        if (actionCooldownRemaining <= 0)
        {
            var hotbar = hotbarInventory.Provide();
            Assert.AreEqual(hotbar, hotbarSubsystem.GetSelectedInventory());
            var stack = hotbar.GetStack(hotbarSubsystem.GetSelectedIndex());

            if (stack.itemType == null)
            {
                var cam = camVar.Provide();
                Assert.IsNotNull(cam);

                var mousePos = cam!.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                var playerPos = entity.transform.position;
                var angle = Vector2.SignedAngle(Vector2.right, mousePos - playerPos);
                if (basicAttack.TryAttack(entity, playerPos, angle))
                {
                    actionCooldownRemaining = actionCooldown;
                }
            }
            else
            {
                stack.itemType.OnItemUse(entity, hotbar, hotbarSubsystem.GetSelectedIndex());
                actionCooldownRemaining = actionCooldown;
            }
        }
    }

    private void OnSpecial()
    {
        if (specialCooldownRemaining <= 0)
        {
            var cam = camVar.Provide();
            Assert.IsNotNull(cam);

            var mousePos = cam!.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            var playerPos = entity.transform.position;
            var angle = Vector2.SignedAngle(Vector2.right, mousePos - playerPos);
            if (specialAttack.TryAttack(entity, playerPos, angle))
            {
                specialCooldownRemaining = specialCooldown;
            }
        }
    }

    private void AssertDependencies()
    {
        Assert.IsNotNull(inputSubsystem);
        Assert.IsNotNull(actionsVar);
        Assert.IsNotNull(hotbarSubsystem);
        Assert.IsNotNull(hotbarInventory);
        Assert.IsNotNull(camVar);
        Assert.IsNotNull(entity);
        Assert.IsNotNull(rigidbody);
        Assert.IsNotNull(actionCooldownIcon);
        Assert.IsNotNull(specialCooldownIcon);
    }

    [Serializable]
    private class MoveState : State<PlayerBehavior2D>
    {
        private readonly InputAction _moveAction;
        private readonly InputAction _actionAction;
        private readonly InputAction _specialAction;
        private readonly InputAction _dashAction;

        public MoveState(PlayerBehavior2D stateMachine) : base(stateMachine)
        {
            _moveAction = StateMachine.Actions.Gameplay.Move;
            _actionAction = StateMachine.Actions.Gameplay.Action;
            _specialAction = StateMachine.Actions.Gameplay.Special;
            _dashAction = StateMachine.Actions.Gameplay.Dash;
        }

        public override void OnTick()
        {
            StateMachine.actionCooldownRemaining -= Time.deltaTime;
            StateMachine.specialCooldownRemaining -= Time.deltaTime;
            if (StateMachine.specialCooldownIcon != null)
                StateMachine.specialCooldownIcon.fillAmount =
                    StateMachine.specialCooldownRemaining / StateMachine.specialCooldown;
            if (StateMachine.actionCooldownIcon != null)
                StateMachine.actionCooldownIcon.fillAmount =
                    StateMachine.actionCooldownRemaining / StateMachine.actionCooldown;

            StateMachine.inputSubsystem.PollMouseStatus();

            var input = _moveAction.ReadValue<Vector2>().normalized;
            StateMachine.rigidbody.velocity = input * StateMachine.MoveSpeed;
            if (input != Vector2.zero) StateMachine.moveDirection = input;

            if (_actionAction.WasPerformedThisFrame())
            {
                if (!StateMachine.inputSubsystem.IsConsumedByInterface())
                {
                    StateMachine.OnAction();
                }
            }

            if (_specialAction.WasPerformedThisFrame())
            {
                StateMachine.OnSpecial();
            }

            if (_dashAction.WasPerformedThisFrame())
            {
                StateMachine.Current = new DashState(StateMachine);
            }
        }
    }

    [Serializable]
    private class DashState : State<PlayerBehavior2D>
    {
        [SerializeField]
        private float prevDrag;

        public DashState(PlayerBehavior2D stateMachine) : base(stateMachine)
        {
        }

        public override void OnEnter()
        {
            prevDrag = StateMachine.rigidbody.drag;
            StateMachine.rigidbody.drag = StateMachine.dashDecay;
            StateMachine.rigidbody.AddForce(StateMachine.moveDirection * StateMachine.dashForce, ForceMode2D.Impulse);
        }

        public override void OnExit()
        {
            StateMachine.rigidbody.drag = prevDrag;
        }

        public override void OnTick()
        {
            if (StateMachine.rigidbody.velocity.magnitude <= StateMachine.MoveSpeed)
            {
                StateMachine.Current = new MoveState(StateMachine);
            }
        }
    }

    public string GetId()
    {
        return ID;
    }

    public SaveFragmentBase Save()
    {
        return new PlayerFragmentV0(actionCooldownRemaining, specialCooldownRemaining, selectedIndex, GetId());
    }

    public void Load(SaveFragmentBase fragment)
    {
        var latest = (PlayerFragmentV0)fragment.GetLatest();
        actionCooldownRemaining = latest.ActionCooldown;
        specialCooldownRemaining = latest.SpecialCooldown;
        selectedIndex = latest.SelectedIndex;
    }
}