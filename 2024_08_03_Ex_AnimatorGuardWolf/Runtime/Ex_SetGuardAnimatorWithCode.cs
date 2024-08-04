using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ex_SetGuardAnimatorWithCode : MonoBehaviour
{

    public Animator m_animator;

    public UnityEvent m_onFireEvent;
    public UnityEvent m_onHitTakenEvent;
    public UnityEvent m_onIsOnGroundEvent;

    public void NotifyHitTaken()
    {
        m_onHitTakenEvent.Invoke();
    }
    public void NotifyFire()
    {
        m_onFireEvent.Invoke();
    }
    public void NotifyIsOnGround()
    {
        m_onIsOnGroundEvent.Invoke();
    }

    [ContextMenu("Reset to stand ground")]
    public void ResetToStandGround()
    {
        SetAsDeath(false);
        SetAsWalking(false);
        SetAsFireing(false);
        m_animator.ResetTrigger("TookHit");
    }


    [ContextMenu("Set As Death")]
    public void Kill()
    {
        SetAsDeath(true);
    }

    [ContextMenu("Set As Walkinng")]
    public void SetAsWalking()
    {
        SetAsWalking(true);
    }
    [ContextMenu("Set As fireing")]
    public void SetAsFireing()
    {
        SetAsFireing(true);
    }
    [ContextMenu("Set As Alive")]
    public void SetAsAlive()
    {
        SetAsDeath(false);
    }


    public void SetAsDeath(bool value)
    {
        m_animator.SetBool("IsDeath", value);
    }
    public void SetAsWalking(bool value)
    {
        m_animator.SetBool("IsWalking", value);
    }
    public void SetAsFireing(bool value)
    {
        m_animator.SetBool("IsFireing", value);
    }

    [ContextMenu("Hit the target")]
    public void TriggerDamageTaken()
    {
        m_animator.SetTrigger("TookHit");
    }
}
