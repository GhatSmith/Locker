public interface ILockerListener
{
    void OnLockChanged(Locker locker, LockerStates currentLockState);
}

[System.Flags] public enum LockerStates { Locked = 1, Unlocked = 2 }