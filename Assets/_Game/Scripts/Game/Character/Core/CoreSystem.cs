using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Core
{
    using Sensor;
    using Navigation;
    using Movement;
    using Display;
    using Sirenix.OdinInspector;

    public abstract class CoreSystem : SerializedMonoBehaviour
    {
        public CharacterStats Stats { get; private set; }
        [field: SerializeField]
        public Character CHARACTER { get; private set; }

        [SerializeField]
        Dictionary<CORE_TYPE, BaseCore> coreDict;

        #region GET_CORE

        enum CORE_TYPE
        {
            DISPLAY,
            MOVEMENT,
            NAVIGATION,
            SENSOR,
            ATTACK
        }

        //cache
        AttackCore _atk;
        DisplayCore _display;
        MovementCore _move;
        NavigationCore _nav;
        SensorCore _sens;
        //property
        public AttackCore ATTACK => GetCore(CORE_TYPE.ATTACK, ref _atk);
        public DisplayCore DISPLAY => GetCore(CORE_TYPE.DISPLAY, ref _display);
        public MovementCore MOVEMENT => GetCore(CORE_TYPE.MOVEMENT, ref _move);
        public NavigationCore NAVIGATION => GetCore(CORE_TYPE.NAVIGATION, ref _nav);
        public SensorCore SENSOR => GetCore(CORE_TYPE.SENSOR, ref _sens);
        #endregion

        public StateMachine StateMachine { get; private set; }
        public bool isInit;

        public virtual void Initialize(CharacterStats stats)
        {
            Stats = stats;

            foreach (var id in coreDict.Keys)
            {
                coreDict[id].Initialize(this);
            }

            StateMachine = new StateMachine();
            isInit = true;
        }

        public virtual void Run() { }

        public virtual void UpdateData()
        {
            if (!isInit) return;

            foreach (var id in coreDict.Keys)
            {
                coreDict[id].UpdateData();
            }
            StateMachine.Update();
        }

        public virtual void FixedUpdate()
        {
            if (!isInit) return;

            foreach (var id in coreDict.Keys)
            {
                coreDict[id].FixedUpdateData();
            }
            StateMachine.FixedUpdate();
        }

        public void OnDeath()
        {
            StateMachine.ChangeState(STATE.DEAD);
        }

        T GetCore<T>(CORE_TYPE id, ref T cache) where T : BaseCore
        {
            return cache ??= coreDict[id] as T;
        }
    }
}
