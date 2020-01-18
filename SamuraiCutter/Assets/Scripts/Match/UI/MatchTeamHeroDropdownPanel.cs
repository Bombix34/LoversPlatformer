using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MatchTeamHeroDropdownPanel : MonoBehaviour
{
    private int dropdownIndex;
    private Dropdown dropdown;
    private List<GameObject> heroes;
    public Action<int, GameObject> onSelectHero;


    public void Init(int dropdownIndex)
    {
        this.dropdownIndex = dropdownIndex;
        this.dropdown = GetComponent<Dropdown>();
        this.heroes = Resources.LoadAll<GameObject>("Prefabs/Heroes").ToList();

        var optionsData = this.heroes.Select(hero => new Dropdown.OptionData(hero.name, hero.GetComponentInChildren<SpriteRenderer>().sprite)).ToList();
        this.dropdown.AddOptions(optionsData);
        if (onSelectHero != null)
        {
            this.onSelectHero(dropdownIndex, this.heroes[0]);
        }
    }

    public void OnValueChanged(int index)
    {
        if(onSelectHero != null)
        {
            this.onSelectHero(dropdownIndex, this.heroes[index]);
        }
    }
}
