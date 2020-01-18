using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchTeamPanel : MonoBehaviour
{
    public Text teamNameText;
    public GameObject heroSelectorContent;
    public GameObject heroDropdownPrefab;
    public MatchTeam MatchTeam { get; private set; }

    public void Init(MatchTeam matchTeam, int heroCount)
    {
        this.MatchTeam = matchTeam;
        this.teamNameText.text = this.MatchTeam.Name;
        this.MatchTeam.Heroes = new List<GameObject>(new GameObject[heroCount]);
        for (int i = 0; i < heroCount; i++)
        {
            var heroDropdownElement = Instantiate(heroDropdownPrefab, heroSelectorContent.transform);
            var matchTeamHeroDropdownPanel = heroDropdownElement.GetComponent<MatchTeamHeroDropdownPanel>();
            matchTeamHeroDropdownPanel.onSelectHero += OnChangeHero;
            matchTeamHeroDropdownPanel.Init(i);
        }
    }

    private void OnChangeHero(int dropdownIndex, GameObject hero)
    {
        this.MatchTeam.Heroes[dropdownIndex] = hero;
    }
}
