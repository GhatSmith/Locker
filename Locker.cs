using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(menuName = "ScriptableObjects/Locker")]
public class Locker : ScriptableObject, ISerializationCallbackReceiver
{
    public bool IsLocked { get { return lockers.Count > 0; } }


    /// <summary> The list of listeners notified when lock has been changed. </summary>
    private readonly List<ILockerListener> onLockChangedListeners = new List<ILockerListener>();
    public List<ILockerListener> OnLockChangedListeners { get { return onLockChangedListeners; } }


    private List<Object> lockers = new List<Object>();

    public LockerStates CurrentLockState { get { return lockers.Count > 0 ? LockerStates.Locked : LockerStates.Unlocked; } }
    private LockerStates previousLockState;


    private void OnEnable()
    {
        previousLockState = CurrentLockState;
    }

    public void AddLock(Object locker)
    {
        if (lockers.Contains(locker)) return;

        lockers.Insert(0, locker);
        if (lockers.Count == 1) OnLockChanged();
    }

    public void RemoveLock(Object locker)
    {
        if (lockers.Contains(locker))
        {
            lockers.Remove(locker);
            if (lockers.Count == 0) OnLockChanged();
        }
    }

    private void OnLockChanged()
    {
        previousLockState = CurrentLockState;

        for (int i = OnLockChangedListeners.Count - 1; i >= 0; i--)
        {
            OnLockChangedListeners[i].OnLockChanged(this, CurrentLockState);
        }
    }

    public override string ToString() { return name; }
    public static implicit operator bool(Locker lockableBool) { return !lockableBool.IsLocked; }

    void ISerializationCallbackReceiver.OnBeforeSerialize() { }
    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        onLockChangedListeners.Clear();
        lockers.Clear();
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        if (UnityEditor.EditorApplication.isPlaying)
        {
            if (previousLockState != CurrentLockState) OnLockChanged();
        }
    }
#endif
}