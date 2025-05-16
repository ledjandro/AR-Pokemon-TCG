using UnityEngine;
using System.Collections.Generic; // This line is correct and necessary for List<>

// System.Collections can be added if you plan to use non-generic collections or Coroutines.

public class Pokemon
{
    public PokemonData BaseDefinition {get; set;} // Refers to the ScriptableObject
    public int Level {get; set;}                  // Internal field for level

    // --- Public Properties to access information ---
    public int HP { get; set; } // Current HP of the Pokemon

    public List<Move> Moves { get; set; } // The Pokemon's moves
    public PokemonData BaseStats => BaseDefinition; // Expose the base definition
               // Expose the level

    // Expose the name directly from the base definition
    public string Name => BaseDefinition.Name;

    /// <summary>
    /// Constructor for creating a Pokemon instance.
    /// </summary>
    /// <param name="pBase">The ScriptableObject defining the base stats and info of this Pokemon.</param>
    /// <param name="pLevel">The current level of this Pokemon.</param>
    public Pokemon(PokemonData pBase, int pLevel)
    {
        BaseDefinition = pBase;
        Level = pLevel;

        // Initialize HP to the calculated MaxHp at the current level
        // Corrected: Assigning the calculated MaxHp (int) to current HP (int)
        HP = MaxHp; 

        // Generate moves based on level
        // This assumes _baseDefinition has a 'LearnableMoves' collection
        // and each item in it has 'Level' and 'Base' (which is a MoveBase)
        Moves = new List<Move>();
        if (BaseDefinition.LearnableMoves != null) // Good practice to check for null
        {
            foreach (var learnableMoveInfo in BaseDefinition.LearnableMoves)
            {
                // Corrected: Use the class field '_level' for comparison
                if (learnableMoveInfo.Level <= Level) 
                {
                    Moves.Add(new Move(learnableMoveInfo.Base)); // Assumes Move constructor takes MoveBase
                }

                // Limit to a maximum of 4 moves
                if (Moves.Count >= 4)
                {
                    break;
                }
            }
        }
        else
        {
            Debug.LogWarning($"Pokemon '{Name}' has no LearnableMoves defined in its PokemonData.", BaseDefinition);
        }
    }

    // --- Calculated Stats Properties ---
    // These properties calculate the stat based on the base stat and level.

    public int MaxHp
    {
        // HP often has a different formula in Pokemon games.
        // Example: return Mathf.FloorToInt(((_baseDefinition.MaxHp * 2 * _level) / 100f) + _level + 10);
        // Using the consistent formula for now:
        get { return Mathf.FloorToInt((BaseDefinition.MaxHp * Level) / 100f) + 10; }
    }

    public int Attack
    {
        get { return Mathf.FloorToInt((BaseDefinition.Attack * Level) / 100f) + 5; }
    }

    public int Defense
    {
        get { return Mathf.FloorToInt((BaseDefinition.Defense * Level) / 100f) + 5; }
    }

    public int SpecialAttack // Special Attack
    {
        get { return Mathf.FloorToInt((BaseDefinition.SpecialAttack * Level) / 100f) + 5; }
    }

    public int SpecialDefense // Special Defense
    {
        get { return Mathf.FloorToInt((BaseDefinition.SpecialDefense * Level) / 100f) + 5; }
    }

    public int Speed
    {
        get { return Mathf.FloorToInt((BaseDefinition.Speed * Level) / 100f) + 5; }
    }
} 