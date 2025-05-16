// ARPokemonInstance.cs (Attach this to your ImageTarget prefab)
using UnityEngine;
using UnityEngine.UI; // For Button
using TMPro;          // Make sure to add this for TextMeshPro

public class ARPokemonInstance : MonoBehaviour
{
    public PokemonData pokemonData; // Assign your specific PokemonData asset here in the Inspector
    public Button attackButton;     // Drag your UI Button GameObject here
    // public Text attackButtonText;   // Comment out or remove the old Text variable
    public TextMeshProUGUI attackButtonText; // Use this for TextMeshPro

    private GameObject currentModelInstance;
    private Animator modelAnimator;

    void Start()
    {
        if (pokemonData == null)
        {
            Debug.LogError("PokemonData not assigned to ARPokemonInstance on " + gameObject.name + ". Please assign it in the Inspector.");
            if (attackButton != null) attackButton.interactable = false;
            this.enabled = false; 
            return;
        }

        SetupPokemon();
        SetupUI();
    }

    void SetupPokemon()
    {
        if (pokemonData.PokemonModelPrefab == null)
        {
            Debug.LogError("Model Prefab is not set for Pokemon: " + pokemonData.Name + " in PokemonData. Cannot instantiate model.");
            return;
        }

        currentModelInstance = Instantiate(pokemonData.PokemonModelPrefab, transform);
        currentModelInstance.transform.localPosition = Vector3.zero; 
        currentModelInstance.transform.localRotation = Quaternion.identity; 
        
        modelAnimator = currentModelInstance.GetComponent<Animator>();

        if (modelAnimator == null)
        {
            Debug.LogError("Instantiated Pokemon model (" + currentModelInstance.name + ") is missing an Animator component. Pokemon: " + pokemonData.Name);
            return; 
        }

        if (modelAnimator.runtimeAnimatorController == null)
        {
            Debug.LogError("Animator on " + currentModelInstance.name + " does not have a base RuntimeAnimatorController assigned. Cannot use AnimatorOverrideController. Pokemon: " + pokemonData.Name);
            return;
        }

        if (pokemonData.IdleAnimation != null || pokemonData.Attack1Animation != null)
        {
            AnimatorOverrideController overrideController = new AnimatorOverrideController(modelAnimator.runtimeAnimatorController);
            modelAnimator.runtimeAnimatorController = overrideController;

            if (pokemonData.IdleAnimation != null)
            {
                // IMPORTANT: Replace "Placeholder_Idle_Clip_Name_In_Controller" with the *actual name*
                // of the animation clip that is in the "Motion" field of your Idle state
                // in your base Animator Controller.
                overrideController["Placeholder_Idle_Clip_Name_In_Controller"] = pokemonData.IdleAnimation;
                Debug.Log("Overriding Idle animation for " + pokemonData.Name);
            }
            else
            {
                Debug.LogWarning("PokemonData for " + pokemonData.Name + " is missing an IdleAnimation, but an override was attempted.");
            }

            if (pokemonData.Attack1Animation != null)
            {
                // IMPORTANT: Replace "Placeholder_Attack_Clip_Name_In_Controller" with the *actual name*
                // of the animation clip that is in the "Motion" field of your Attack state
                // in your base Animator Controller.
                overrideController["Placeholder_Attack_Clip_Name_In_Controller"] = pokemonData.Attack1Animation;
                Debug.Log("Overriding Attack1 animation for " + pokemonData.Name);
            }
            else
            {
                 Debug.LogWarning("PokemonData for " + pokemonData.Name + " is missing an Attack1Animation, but an override was attempted.");
            }
        }
        else
        {
            Debug.Log("No Idle or Attack1 animation specified in PokemonData for " + pokemonData.Name + " to override.");
        }
    }

    void SetupUI()
    {
        if (attackButton == null)
        {
            Debug.LogWarning("Attack Button GameObject is not assigned in the Inspector for " + gameObject.name);
            // Optionally disable the script or parts of it if the button is critical
            // this.enabled = false; 
            return;
        }

        if (pokemonData != null && !string.IsNullOrEmpty(pokemonData.Attack1Name))
        {
            if (attackButtonText != null)
            {
                attackButtonText.text = pokemonData.Attack1Name; // This line updates the text
                Debug.Log("Setting attack button text to: " + pokemonData.Attack1Name);
            }
            else
            {
                Debug.LogWarning("Attack Button Text (TextMeshProUGUI) component is not assigned in the Inspector for " + gameObject.name + ", but Attack1Name exists in PokemonData.");
            }

            attackButton.onClick.RemoveAllListeners(); 
            attackButton.onClick.AddListener(PerformAttack1);
            attackButton.gameObject.SetActive(true); 
            attackButton.interactable = true;
        }
        else
        {
            attackButton.gameObject.SetActive(false); 
            if (pokemonData == null)
            {
                 Debug.LogWarning("PokemonData is null, cannot set up Attack Button for " + gameObject.name);
            }
            else if (string.IsNullOrEmpty(pokemonData.Attack1Name))
            {
                 Debug.LogWarning("Attack1Name is not set in PokemonData for " + pokemonData.Name + ". Hiding attack button.");
            }
        }
    }

    void PerformAttack1()
    {
        if (modelAnimator == null)
        {
            Debug.LogError("Cannot perform attack: Model Animator is missing for " + (pokemonData != null ? pokemonData.Name : "Unknown Pokemon") + ". Was SetupPokemon successful?");
            return;
        }
        if (pokemonData == null || pokemonData.Attack1Animation == null)
        {
            Debug.LogError("Cannot perform attack: PokemonData or Attack1Animation is missing for " + (pokemonData != null ? pokemonData.Name : "Unknown Pokemon"));
            return;
        }

        modelAnimator.SetTrigger("Attack1"); 
        Debug.Log((pokemonData.Name != null ? pokemonData.Name : "Pokemon") + " performs " + (pokemonData.Attack1Name != null ? pokemonData.Attack1Name : "Attack1"));
    }

    public void OnTargetFound()
    {
        Debug.Log("Target found: " + (pokemonData != null ? pokemonData.Name : gameObject.name));
        if (currentModelInstance != null)
        {
            currentModelInstance.SetActive(true);
            if (modelAnimator != null)
            {
                // Example: Force play Idle state. Replace "Placeholder_Idle_Clip_Name_In_Controller"
                // with the actual name of your Idle state in the Animator Controller if needed.
                // modelAnimator.Play("Placeholder_Idle_Clip_Name_In_Controller", 0, 0f); 
            }
        }
        
        if (attackButton != null)
        {
            bool canAttack = pokemonData != null && !string.IsNullOrEmpty(pokemonData.Attack1Name) && pokemonData.Attack1Animation != null;
            attackButton.gameObject.SetActive(canAttack);
            attackButton.interactable = canAttack;
        }
    }

    public void OnTargetLost()
    {
        Debug.Log("Target lost: " + (pokemonData != null ? pokemonData.Name : gameObject.name));
        if (currentModelInstance != null)
        {
            currentModelInstance.SetActive(false);
        }
        if (attackButton != null)
        {
            attackButton.gameObject.SetActive(false);
        }
    }
}
