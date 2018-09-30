using UnityEngine;


public class LockExample : MonoBehaviour
{

    [SerializeField] private Locker targetLocker;

    // Use this for initialization
    void Start()
    {
        targetLocker.AddLock(this);

    }
}