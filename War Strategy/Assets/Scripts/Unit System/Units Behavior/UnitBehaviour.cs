using UnityEngine;

public enum BehaviourType {Attacking, Workring}
public class UnitBehaviour : MonoBehaviour
{
    public BehaviourType CurrentBehaviourType;

    private AttackBehaviour _attackBehaviour;
    private WorkingBehaviour _workingBehaviour;

    private void Start()
    {
        _attackBehaviour = GetComponent<AttackBehaviour>();
        _workingBehaviour = GetComponent<WorkingBehaviour>();
    }

    public void CurrentBehaviour(Transform target)
    {
        if (CurrentBehaviourType == BehaviourType.Attacking)
        {
            _attackBehaviour.AttackThisTarget(target);
        }
        else if (CurrentBehaviourType == BehaviourType.Workring)
        {
            _workingBehaviour.CollectThisResourceTarget(target);
        }
    }
}
