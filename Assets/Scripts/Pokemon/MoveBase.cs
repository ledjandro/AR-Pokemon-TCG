using UnityEngine;

// You'll likely have a PokemonType enum defined elsewhere (e.g., Fire, Water, Grass)
// If not, you'll need to define it:
/*
public enum PokemonType
{
    None, Normal, Fire, Water, Grass, Electric, // etc.
}
*/

public enum MoveCategory
{
    Physical,
    Special,
    Status
}

[CreateAssetMenu(fileName = "NewMove", menuName = "Pokemon/Create New Move")]
public class MoveBase : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] private string moveName;
    [TextArea]
    [SerializeField] private string description;

    [Header("Combat Properties")]
    [SerializeField] private PokemonType type;
    [SerializeField] private MoveCategory category;
    [SerializeField] private int power;
    [SerializeField] private int accuracy; // 0-100, 0 for always hits (e.g. Swift) or non-damaging moves
    [SerializeField] private int pp; // Max Power Points

    [Header("Effects (Optional - for more advanced systems)")]
    [SerializeField] private MoveEffect effects; // You'd define MoveEffect class/struct if needed
    [SerializeField] private MoveTarget target; // You'd define MoveTarget enum (e.g., Foe, Self)

    // Public properties to access the data
    public string Name => moveName;
    public string Description => description;
    public PokemonType Type => type;
    public MoveCategory Category => category;
    public int Power => power;
    public int Accuracy => accuracy;
    public int PP => pp;
    public MoveEffect Effects => effects; // Only if you implement MoveEffect
    public MoveTarget Target => target;   // Only if you implement MoveTarget

    // You might add methods here later for move effects if they are simple enough
    // public virtual void ApplyEffects(Pokemon user, Pokemon target) { }
}

// --- Optional (for more advanced systems) ---
[System.Serializable]
public class MoveEffect // Example structure for move effects
{
    // Define fields for status conditions, stat changes, etc.
    // public List<StatBoost> Boosts;
    // public StatusConditionID Status;
    // public VolatileStatusID VolatileStatus;
}

public enum MoveTarget // Example enum for move targets
{
    Foe,
    Self,
    AllFoes,
    AllAllies,
    Ally,
    FoeOrAlly,
    UserAndAllies
}

/* Example for StatBoost if you go that deep:
[System.Serializable]
public class StatBoost
{
    public Stat stat; // Enum for Atk, Def, Spd, etc.
    public int boost;  // How many stages to boost/lower
}
public enum Stat { Attack, Defense, Speed, SpecialAttack, SpecialDefense, Accuracy, Evasion }
*/