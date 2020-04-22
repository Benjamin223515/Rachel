using UnityEngine;
using UnityEngine.UI;

public abstract class Challenge : MonoBehaviour
{
    /// <summary>
    /// Raw variables
    /// </summary>
    private bool finalized; 

    private int fPoints = 0;
    private int hPoints = 0;
    private int cPoints = 0;

    private string textSender = "";
    private string textMessage = "";

    private cStates cState = cStates.NONE;
    /// <summary>
    /// Protected variables
    /// </summary>
    public enum cStates { COMPLETED, ONGOING, ALERT, NONE }

    public bool Finalized
    {
        get { return finalized; }
        set { finalized = value; }
    }

    public int p_Failure
    {
        get { return fPoints; }
        set { fPoints = value; }
    }
    protected int p_Hidden
    {
        get { return hPoints; }
        set { hPoints = value; }
    }
    public int p_Completed
    {
        get { return cPoints; }
        set { cPoints = value; }
    }

    protected string t_Sender
    {
        get { return textSender; }
        set { textSender = value; }
    }
    protected string t_Message
    {
        get { return textMessage; }
        set { textMessage = value; }
    }

    public cStates state
    {
        get { return cState; }
        set { cState = value; }
    }

    /// <summary>
    /// METHODS
    /// </summary>
    protected abstract void Main();
    public abstract void Prompt(/*image sender, text sndr, text message*/);
    public virtual void Check(AbilityHitDetector.ChallengeStates state, GameObject player) { }
    public virtual void CheckEvent(Collider col, AbilityHitDetector.ChallengeStates state, GameObject player) { }
    public virtual void CheckEvent(Collision col, AbilityHitDetector.ChallengeStates state, GameObject player) { }
    public abstract void Hit(AbilityHitDetector.ChallengeStates state, bool hiding);
    public abstract void Initialize();

    public cStates getState()
    {
        return cState;
    }
    public bool setState(cStates newState)
    {
        if (cState == newState) return false;
        else
        {
            cState = newState;
            return true;
        }
    }
}