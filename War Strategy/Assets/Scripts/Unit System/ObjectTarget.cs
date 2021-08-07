using UnityEngine;

public enum ObjectType {Player, Enemy, Resources}
public class ObjectTarget : MonoBehaviour
{
    public ObjectType CurrentObjectType;
}
