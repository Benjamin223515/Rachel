using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHitDetector : MonoBehaviour
{
    public playerMovement pm;
    public List<Challenge> challengePool = new List<Challenge>();
    public Challenge activeChallenge;
    private int activeChallenges = 0;

    public List<MonoBehaviour> teacherAbilities = new List<MonoBehaviour>();

    public int activeAbilities = 0;

    public enum ChallengeStates
    {
        NONE, ONGOING, HIDDEN
    }

    public ChallengeStates State = ChallengeStates.NONE;

    //

    private void Start()
    {
        foreach(Challenge c in challengePool) c.Initialize(); 
    }

    bool done = false;
    void Update()
    {
        if (activeChallenge != null)
        {
            activeChallenge.Check(State, gameObject);
            if(activeChallenge.state == Challenge.cStates.ALERT)
            {
                    int a = Random.Range(0, activeAbilities);
                    if (teacherAbilities[a].GetType() == typeof(Scan)) StartCoroutine(((Scan)teacherAbilities[a]).scan(1 + activeAbilities, 1 + activeAbilities, 0.5f * activeAbilities, true)); 
            }
            if (activeChallenge.Finalized && !done)
            {
                done = true;
                switch (activeChallenge.state)
                {
                    case Challenge.cStates.ALERT:
                        CloutHandler.alterClout(activeChallenge.p_Failure);
                        break;

                    case Challenge.cStates.COMPLETED:
                        CloutHandler.alterClout(activeChallenge.p_Completed);
                        break;
                }
                activeChallenge = null;
            }
        }
    }

    //

    public void Hit()
    {
        if (activeChallenge != null) activeChallenge.Hit(State, pm.isHiding);
    }

    public void addChallenge(Challenge c)
    {
        activeChallenge = c;
        activeChallenge.Prompt();
        State = ChallengeStates.ONGOING;
        activeChallenges++;
        done = false;
    }
}
