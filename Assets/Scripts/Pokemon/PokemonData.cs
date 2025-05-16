using UnityEngine;
using System.Collections.Generic;

// Make sure the PokemonType enum is defined (as shown above or in its own file)

[CreateAssetMenu(fileName = "NewPokemonData", menuName = "Pokemon/Pokemon Data")]
public class PokemonData : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] private string pokemonNameInternal;
    [TextArea(3, 10)]
    [SerializeField] private string descriptionInternal;

    [Header("AR and Model")]
    [SerializeField] private GameObject modelPrefabInternal; // Your 3D model with an Animator component
    [SerializeField] private Texture2D imageTargetTextureInternal; // PNG for AR tracking (Vuforia usually uses its own database system, this is for potential custom use or reference)

    [Header("Animations")]
    [SerializeField] private AnimationClip idleAnimationInternal;     // Animation clip for Idle
    [SerializeField] private AnimationClip attack1AnimationInternal;  // Animation clip for the first attack
    // You could add more attack animation slots here:
    // [SerializeField] private AnimationClip attack2AnimationInternal;

    [Header("Attack Details")]
    [SerializeField] private string attack1NameInternal; // Name of the first attack (e.g., "Tackle", "Thunderbolt")
    // Correspondingly for more attacks:
    // [SerializeField] private string attack2NameInternal;

    [Header("Typing")]
    [SerializeField] private PokemonType type1Internal;
    // [SerializeField] private PokemonType type2Internal; // If you want dual types

    [Header("Base Stats")]
    [SerializeField] private int maxHpInternal;
    [SerializeField] private int attackInternal;
    [SerializeField] private int defenseInternal;
    [SerializeField] private int spAttackInternal;
    [SerializeField] private int spDefenseInternal;
    [SerializeField] private int speedInternal; // Check this line exists

    [Header("Learnable Moves")] // Check this section exists
    [SerializeField] private List<LearnableMove> learnableMovesInternal; // Check this line exists

    // --- Public Properties to access private fields ---

    public string Name => pokemonNameInternal;
    public string Description => descriptionInternal;
    public GameObject PokemonModelPrefab => modelPrefabInternal;
    public Texture2D ImageTargetTexture => imageTargetTextureInternal; // Public accessor
    public AnimationClip IdleAnimation => idleAnimationInternal;       // Public accessor
    public AnimationClip Attack1Animation => attack1AnimationInternal;  // Public accessor
    public string Attack1Name => attack1NameInternal;                 // Public accessor
    public PokemonType Type1 => type1Internal;
    // public PokemonType Type2 => type2Internal;
    public int MaxHp => maxHpInternal;
    public int Attack => attackInternal;
    public int Defense => defenseInternal;
    public int SpecialAttack => spAttackInternal; // Renamed for clarity from spAttackInternal
    public int SpecialDefense => spDefenseInternal; // Renamed for clarity from spDefenseInternal
    public int Speed => speedInternal; // Check this line exists
    public List<LearnableMove> LearnableMoves => learnableMovesInternal; // Check this line exists
    // Note: For "Special Attack" and "Special Defense", I've kept the internal field names
    // as you had them but made the public properties slightly more descriptive.
}