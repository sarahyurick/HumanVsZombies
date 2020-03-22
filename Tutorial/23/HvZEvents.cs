using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class HvZEvents
{
    public static HitEvent zombieHit = new HitEvent();
    // HvZEvents.zombieHit
    // GameLogic's Awake function would contain HvZEvents.zombieHit.AddListener(ZombieHitResponse);
    /*
     * In GameLogic:
     * void ZombieHitResponse(HitEvent data) {
     *   Debug.Log(data.victim);
     * }
     */
}

public class HitEvent : UnityEvent<HitEventData> { }

public class HitEventData
{
    public GameObject shooter;
    public GameObject victim;
    public GameObject arrow;

    public HitEventData(GameObject shooter, GameObject victim, GameObject arrow)
    {
        this.shooter = shooter;
        this.victim = victim;
        this.arrow = arrow;
    }
}
