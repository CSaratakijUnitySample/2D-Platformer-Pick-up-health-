using UnityEngine;

public class Status : MonoBehaviour
{
    [SerializeField]
    int current;

    [SerializeField]
    int max;


    public delegate void _Func(int value);
    public event _Func OnValueChanged;

    public int Current { get { return current; } }
    public int Max { get { return max; } }


    public void FullRestore()
    {
        current = max;
        BroadcastEvent();
    }

    public void Clear()
    {
        current = 0;
        BroadcastEvent();
    }

    public void Restore(int value)
    {
        current = ((current + value) > max) ? max : (current + value);
        BroadcastEvent();
    }

    public void Remove(int value)
    {
        current = ((current - value) < 0) ? 0 : (current - value);
        BroadcastEvent();
    }

    void BroadcastEvent()
    {
        if (OnValueChanged != null) {
            OnValueChanged(current);
        }
    }
}
