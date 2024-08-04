using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WolfGuardEnumState
{
    StandGround,
    Aiming,
    Fireing,
    WalkingUpLeft,
    WalkingDownLeft,
    WalkingUpRight,
    WalkingDownRight,
    TakingHit,
    DyingStart,
    DyingMiddle,
    DyingEndDead,
    Empty
}
public class Ex_StateEnumToTileOffset : MonoBehaviour
{
    public Ex_SetTileOffsetOfTextureXY m_tileOffsetSetter;

    public bool m_useUpdateRefresh = false;

    private void Reset()
    {
        m_tileOffsetSetter = GetComponent<Ex_SetTileOffsetOfTextureXY>();
    }
    public WolfGuardEnumState m_currentState;

    private void OnValidate()
    {
        SetWithEnum(m_currentState);
    }
    public void Update()
    {
        if (m_useUpdateRefresh)
           SetWithEnum(m_currentState);
    }

    public void SetWithEnum(WolfGuardEnumState guard) { 
    
        if( m_tileOffsetSetter == null)
        {
            return;
         }
        m_currentState = guard;
        switch (guard)
        {
            case WolfGuardEnumState.StandGround:
                m_tileOffsetSetter.SetWith(0, 2);
                break;
            case WolfGuardEnumState.Aiming:
                m_tileOffsetSetter.SetWith(1, 2);
                break;
            case WolfGuardEnumState.Fireing:
                m_tileOffsetSetter.SetWith(2, 2);
                break;
            case WolfGuardEnumState.Empty:
                m_tileOffsetSetter.SetWith(3, 2);
                break;
            case WolfGuardEnumState.WalkingUpLeft:
                m_tileOffsetSetter.SetWith(0, 1);
                break;
            case WolfGuardEnumState.WalkingDownLeft:
                m_tileOffsetSetter.SetWith(1, 1);
                break;
            case WolfGuardEnumState.WalkingUpRight:
                m_tileOffsetSetter.SetWith(2, 1);
                break;
            case WolfGuardEnumState.WalkingDownRight:
                m_tileOffsetSetter.SetWith(3, 1);
                break;
            case WolfGuardEnumState.TakingHit:
                m_tileOffsetSetter.SetWith(0, 0);
                break;
            case WolfGuardEnumState.DyingStart:
                m_tileOffsetSetter.SetWith(1, 0);
                break;
            case WolfGuardEnumState.DyingMiddle:
                m_tileOffsetSetter.SetWith(2, 0);
                break;
            case WolfGuardEnumState.DyingEndDead:
                m_tileOffsetSetter.SetWith(3, 0);
                break;
            default:
                break;
        }
    }
    
}
