

using UnityEngine;
using UnityEngine.Events;

public class RefreshableComponent : MonoBehaviour, I_Refreshable
{
	public Refresher.E_RefreshType m_refreshType;
	public UnityEvent m_onRefresh;
    public float m_gametime;
    public Refresher.E_RefreshType GetRefreshType()
    {
        return m_refreshType;
    }
    
    public void Refresh(float time)
    {
		Debug.Log("Refreshed");
		m_onRefresh.Invoke();
        m_gametime = time;
    }
}
