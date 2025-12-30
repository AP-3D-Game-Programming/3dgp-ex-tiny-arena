using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    [Header("Spells")]
    public List<Spell> spells = new List<Spell>();

    public Spell CurrentSpell { get; private set; }

    [Header("Effects")]
    public ParticleSystem staffGlowParticles;  //glow FX op de staff
    public ParticleSystem spellTrailFX; //trail FX

    private int currentIndex = 0;
    public static SpellManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (spells.Count == 0)
        {
            Debug.LogWarning("Geen spells toegevoegd aan SpellManager!");
            return;
        }

        SelectSpell(0);
    }

    public void NextSpell()
    {
        currentIndex++;
        if (currentIndex >= spells.Count)
            currentIndex = 0;

        SelectSpell(currentIndex);
    }

    public void SelectSpell(int index)
    {
        currentIndex = index;
        CurrentSpell = spells[index];

        Debug.Log("Current spell: " + CurrentSpell.name);

        UpdateStaffColor();
    }

    private void UpdateStaffColor()
    {
        if (staffGlowParticles == null || CurrentSpell == null)
            return;

        var main = staffGlowParticles.main;
        main.startColor = CurrentSpell.spellColor;  //BELANGRIJK

        Debug.Log("Glow updated to: " + CurrentSpell.spellColor);
    }
}
