using UnityEngine;

public class PokemonAttackController : MonoBehaviour
{
    public Animator pokemonAnimator; // Assign this in the Inspector!

    // Option 1: Specific function for Attack1
    public void PerformAttack1()
    {
        if (pokemonAnimator != null)
        {
            // "Attack1" must match the trigger name in your Animator Controller
            pokemonAnimator.SetTrigger("Attack1");
            Debug.Log("Attack1 button pressed, trigger sent!");
        }
        else
        {
            Debug.LogError("Pokemon Animator not assigned to PokemonAttackController script!");
        }
    }

    // Option 2: Generic function (if you chose this in the button setup)
    public void PerformGenericAttack(string triggerName)
    {
        if (pokemonAnimator != null)
        {
            pokemonAnimator.SetTrigger(triggerName);
            Debug.Log(triggerName + " button pressed, trigger sent!");
        }
        else
        {
            Debug.LogError("Pokemon Animator not assigned to PokemonAttackController script!");
        }
    }

    // ... other functions like OnTargetFound, OnTargetLost etc.
}