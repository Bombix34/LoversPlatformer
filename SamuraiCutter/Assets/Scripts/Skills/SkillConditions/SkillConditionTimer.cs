using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SkillConditionTimer : SkillConditionAbstract
{
    public int Milliseconds { get; set; }

    public SkillConditionTimer(int milliseconds)
    {
        this.Milliseconds = milliseconds;
    }

    public override void Start()
    {
        Task.Factory.StartNew(() =>
        {
            System.Threading.Thread.Sleep(Milliseconds);
            Debug.Log("he");
            this.Activate();
        });
    }
}
