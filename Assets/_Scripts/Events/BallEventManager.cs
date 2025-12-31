using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Events;

public static class BallEventManager
{
    static PlayerController invokerBallFired;
    static BottomWallDeath invokerBWD;
    static UnityAction listenerBWD;

    static UnityAction listenerBallFired;

    public static void AddBWDInvoker(BottomWallDeath script)
    {
        invokerBWD = script;

        if (listenerBWD!=null)
        {
            invokerBWD.AddBottomWallDeathListener(listenerBWD);
        }
    }

    public static void AddBWDListener(UnityAction handler)
    {
        listenerBWD = handler;
        if(invokerBWD!=null)
        {
            invokerBWD.AddBottomWallDeathListener(listenerBWD);
        }
    }

    public static void AddBallFiredInvoker(PlayerController script)
    {
        invokerBallFired = script;
        if(listenerBallFired!=null)
        {
            invokerBallFired.AddBallFiredListener(listenerBallFired);
        }
    }

    public static void AddBallFiredListener(UnityAction handler)
    {
        listenerBallFired = handler;
        if(invokerBallFired!=null)
        {
            invokerBallFired.AddBallFiredListener(listenerBallFired);
        }
    }
}
