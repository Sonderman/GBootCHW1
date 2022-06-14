using UnityEngine.Events;

public class EventManager: MonoSingleton<EventManager>
{
    public UnityAction OnCollectValuables;
    public UnityAction OnHealthChanged;
}