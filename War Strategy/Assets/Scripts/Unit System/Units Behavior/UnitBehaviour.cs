using UnityEngine;

public enum BehaviourType {Attacking, Workring}
public class UnitBehaviour : MonoBehaviour
{
    [SerializeField] private BehaviourType CurrentBehaviourType;

    private AttackBehaviour _attackBehaviour;

    private void Start()
    {
        _attackBehaviour = GetComponent<AttackBehaviour>();
    }

    public void CurrentBehaviour(Transform battleTarget)
    {
        if (CurrentBehaviourType == BehaviourType.Attacking)
        {
            _attackBehaviour.AttackThisTarget(battleTarget);
        }
        else if (CurrentBehaviourType == BehaviourType.Workring)
        {
            return;
        }
    }
}
