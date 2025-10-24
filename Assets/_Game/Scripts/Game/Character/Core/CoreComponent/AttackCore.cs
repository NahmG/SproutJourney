using Core;
using Core.Sensor;
using UnityEngine;

public class AttackCore : BaseCore
{
    [Header("Ref")]
    [SerializeField]
    Character _char;
    [SerializeField]
    SensorCore _sens;

    [Header("Set up")]
    [SerializeField]
    PoolType bulletType;
    [SerializeField]
    Transform shootPoint;
    [field: SerializeField]
    public AnimationEvent Event { get; private set; }

    protected bool _isAtkCooldown;
    public bool IsAtkCooldown => _isAtkCooldown;
    bool _isAttack;
    public bool IsAttack
    {
        get => _isAttack;
        set => _isAttack = value;
    }

    float timer;

    void Awake()
    {
        Event._OnActionExecute += UseSkill;
    }

    void OnDestroy()
    {
        Event._OnActionExecute -= UseSkill;
    }

    public override void Initialize(CoreSystem core)
    {
        base.Initialize(core);
        _isAtkCooldown = false;
        _isAttack = false;
    }

    public override void UpdateData()
    {
        base.UpdateData();
        UpdateAtkCooldown();
    }

    public void UseSkill()
    {

    }

    void UpdateAtkCooldown()
    {
        if (_isAtkCooldown)
        {
            // if (Time.time >= timer + _char.Stats.AtkCoolDown.Value)
            //     _isAtkCooldown = false;
        }
    }
}
