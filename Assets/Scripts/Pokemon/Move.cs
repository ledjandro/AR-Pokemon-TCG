using UnityEngine;

public class Move
{
    public MoveBase Base { get; private set; } // Reference to the ScriptableObject defining the move
    public int PP { get; set; }              // Current PP for this specific move instance

    /// <summary>
    /// Constructor for creating an instance of a learned move.
    /// </summary>
    /// <param name="pBase">The ScriptableObject defining the base properties of this move.</param>
    public Move(MoveBase pBase)
    {
        Base = pBase;
        PP = pBase.PP; // Initialize current PP to the move's max PP
    }

    // --- Convenience Properties (delegating to MoveBase) ---
    public string Name => Base.Name;
    public string Description => Base.Description;
    public PokemonType Type => Base.Type;
    public MoveCategory Category => Base.Category;
    public int Power => Base.Power;
    public int Accuracy => Base.Accuracy;
    // Max PP is Base.PP, current PP is this.PP

    // You could add methods here related to using the move in battle, e.g.,
    // public bool UseMove()
    // {
    //     if (PP > 0)
    //     {
    //         PP--;
    //         return true; // Move was used
    //     }
    //     return false; // Not enough PP
    // }
}