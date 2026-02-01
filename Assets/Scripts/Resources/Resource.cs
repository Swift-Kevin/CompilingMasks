using UnityEngine;
using System;

[Serializable]
public class Resource
{
    public event Action<float, float> OnChanged;
    public event Action OnDepleted;

    [SerializeField] private float curValue;

    public float CurrentValue => curValue;

    public void Initialize(float startValue = 0f)
    {
        curValue = Mathf.Max(0f, startValue);
    }

    public void Increase(float amount)
    {
        if (amount <= 0f)
            return;

        SetValue(curValue + amount);
    }

    public void Decrease(float amount)
    {
        if (amount <= 0f)
            return;

        SetValue(Mathf.Max(0f, curValue - amount));
    }

    public bool Spend(float amount)
    {
        if (amount <= 0f || curValue < amount)
            return false;

        Decrease(amount);
        return true;
    }

    public void SetValue(float value)
    {
        float oldValue = curValue;
        curValue = Mathf.Max(0f, value);

        if (!Mathf.Approximately(oldValue, curValue))
        {
            OnChanged?.Invoke(oldValue, curValue);

            if (Mathf.Approximately(curValue, 0f))
                OnDepleted?.Invoke();
        }
    }

    public bool IsEmpty()
    {
        return Mathf.Approximately(curValue, 0f);
    }
}