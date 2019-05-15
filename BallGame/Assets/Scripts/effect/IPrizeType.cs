using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class IPrizeType : MonoBehaviour 
{
    public abstract void ShowPrize(int _id);

    public void OnDelete(BaseEventData data)
    {
        Destroy(gameObject, 0.1f);
    }
}
