using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Destructible2D;

[CreateAssetMenu(menuName = "OVERWORMS/Skills/Rocket Launcher")]
public class RocketSkill : SkillManager
{
    public GameObject m_indicatorPrefab;
    public float m_indicatorScale = 1.0f;

    private GameObject m_indicatorInstance;

    [Space(10)]

    public GameObject m_projectilePrefab;
    public float m_projectileSpeed = 10.0f;
    public float m_projectileSpread;

    private float m_inputAmount = 100f;

    private Vector2 m_dirVector;

    public override void UseSkill()
    {
    }

    public override void UpdateSkill()
    {
        m_dirVector = (m_hero.Inputs.GetMovementInput()+m_hero.transform.position)-(m_hero.transform.position);
        UpdateIndicator();
    }

    private void UpdateIndicator()
    {
        if (m_indicatorInstance == null)
        {
            m_indicatorInstance = Instantiate(m_indicatorPrefab);
        }
        var startPos = new Vector2(m_hero.transform.position.x + 0.5f, m_hero.transform.position.y); 
        var currentPos = (startPos + (m_dirVector * m_inputAmount));
        var scale = Vector3.Distance(currentPos, startPos) * m_indicatorScale;
        float angle = Mathf.Atan2(m_dirVector.y, m_dirVector.x) * Mathf.Rad2Deg;

        // Transform the indicator so it lines up with the slice
        m_indicatorInstance.transform.position = startPos;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        m_indicatorInstance.transform.rotation=Quaternion.Slerp(m_indicatorInstance.transform.rotation,rotation, 10f * Time.deltaTime);
        m_indicatorInstance.transform.localScale = new Vector3(2, 2, 2);
    }

}
