using UnityEngine; // Needed for [SerializeField]
using System;      // Needed for [System.Serializable]

// Make sure MoveBase class/script exists and is accessible
// Example: public class MoveBase : ScriptableObject { ... }

[System.Serializable] // This makes it visible in the Inspector when used in a List<>
public struct LearnableMove // Using a struct is common here, but can be a class
{
    [SerializeField] private MoveBase moveBase; // Assign the MoveBase ScriptableObject (e.g., Tackle, Ember assets) here
    [SerializeField] private int levelLearned;  // Level the move is learned

    // Public properties to access the private fields safely
    public MoveBase Base => moveBase;
    public int Level => levelLearned;
}