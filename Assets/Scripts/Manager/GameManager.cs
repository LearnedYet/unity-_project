using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : Singlenton<GameManager>
{
    public CharacterStats playerStats;
    private CinemachineFreeLook followCamera;
    List<IEngGameObserver> engGameObservers = new List<IEngGameObserver>();
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    public void RigisterPlayer(CharacterStats player)
    {
        playerStats = player;
        followCamera = FindObjectOfType<CinemachineFreeLook>();
        if(followCamera!=null)
        {
            followCamera.Follow = playerStats.transform;
            followCamera.LookAt = playerStats.transform;
        }
    }

    public void AddObserver(IEngGameObserver observer)
    {
        engGameObservers.Add(observer);        
    }
    public void RemoveObserver(IEngGameObserver observer)
    {
        engGameObservers.Remove(observer);
    }
    public void NotifyObserver()
    {
        foreach(var observer in engGameObservers)
        {
           
            observer.EndNotify();
        }
    }
    public Transform GetEntrance()
    {
        foreach(var item in FindObjectsOfType<TransitionDestination>())
        {
            if (item.destinationTag == TransitionDestination.DestinationTag.ENTER)
                return item.transform;
        }
        return null;
    }
}
