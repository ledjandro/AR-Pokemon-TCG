using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text levelText;
    [SerializeField] HPBar hPBar;

    public void SetData(Pokemon pokemon){
        nameText.text = pokemon.BaseDefinition.Name;
        levelText.text="Lvl" + pokemon.Level;
        hPBar.SetHP((float) pokemon.HP/pokemon.MaxHp);
    }
}
