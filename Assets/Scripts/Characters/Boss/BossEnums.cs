using UnityEngine;

// BossEnums.cs
using UnityEngine;

public enum StateType
{
    Idle,
    Aggro,
    Attack,
    StrongAttack
}

public enum ElementType
{
    Ice,
    Fire,
    Earth,
    Aether
}

[System.Serializable]
public struct ElementSettings
{
    public ElementType element;
    public Color color;
}
