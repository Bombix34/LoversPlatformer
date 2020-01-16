using UnityEngine;

public class HeroStatsManager : MonoBehaviour
{
    private HeroManager m_manager;

    private int m_lifePoint;
    public bool Alive { 
        get 
        {
            return this.LifePoint > 0;
        } 
    }
    public int LifePoint 
    {
        get
        {
            return m_lifePoint;
        }
        private set
        {
            if(value < 0)
            {
                m_lifePoint = 0;
                return;
            }

            if(value > m_manager.Settings.lifePoint)
            {
                m_lifePoint = m_manager.Settings.lifePoint;
                return;
            }

            m_lifePoint = value;
        }
    }
    private void Awake()
    {
        m_manager = GetComponent<HeroManager>();
        this.m_lifePoint = m_manager.Settings.lifePoint;
    }

    public int AddLifePoint(int lifePoint)
    {
        if(lifePoint < 0)
        {
            throw new System.ArgumentException("The parameter value cannot be < to 0", nameof(lifePoint));
        }
        return this.LifePoint += lifePoint;
    }
    public int RemoveLifePoint(int lifePoint)
    {
        if (lifePoint < 0)
        {
            throw new System.ArgumentException("The parameter value cannot be < to 0", nameof(lifePoint));
        }
        return this.LifePoint -= lifePoint;
    }


}