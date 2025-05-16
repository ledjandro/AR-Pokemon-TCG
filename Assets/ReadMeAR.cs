// //# README: Adding a New AR Pokémon (e.g., Darmanitan)

// This guide outlines the steps to add a new Pokémon to your AR project, from importing its 3D model and animations to configuring it to work with the `ARPokemonInstance` script and Vuforia.

// ## Prerequisites:

// * A 3D model of the Pokémon (e.g., in FBX, OBJ format).
// * Animation files for the Pokémon (e.g., Idle, Attack1). These might be separate files or embedded within the model FBX.
// * An image of the Pokémon card to be used as an AR Image Target.
// * Your Unity project with Vuforia SDK, `PokemonData.cs`, and `ARPokemonInstance.cs` scripts already set up.
// * A base `Animator Controller` that your `ARPokemonInstance` script uses for its `AnimatorOverrideController` (this controller should have states with placeholder animation clips).

// ## Steps:

// ### 1. Prepare and Import Assets

// 1.  **Organize Assets:**
//     * Create a new folder in your Unity Project window for this Pokémon (e.g., `Assets/Models/Darmanitan/`).
//     * Place your Darmanitan 3D model file (e.g., `Darmanitan.fbx`) and any separate animation files into this folder.
// 2.  **Import Model into Unity:**
//     * Drag the model file from your computer's file explorer into the `Darmanitan` folder in your Unity Project window.
//     * Select the imported model file in the Project window. In the **Inspector**:
//         * **Rig Tab:**
//             * Set **Animation Type** to **Generic** (or **Humanoid** if your model is humanoid and you intend to configure its Avatar). Based on our previous discussions, you're likely using Generic.
//             * Ensure **Avatar Definition** is "Create From This Model" (usually default).
//             * Click **Apply**.
//         * **Animation Tab (if animations are embedded in the model FBX):**
//             * You'll see a list of animation clips. Identify your Idle and Attack animations.
//             * You can rename them here if needed (e.g., `Darmanitan_Idle`, `Darmanitan_Attack1`).
//             * For each important clip (Idle, Attack1):
//                 * Select the clip.
//                 * **Loop Time:** Check this for your Idle animation. Uncheck it for attack animations.
//                 * **Root Motion (Important for Positioning):**
//                     * If you suspect the animation might have unwanted root movement (especially vertical movement causing sinking/floating), look for "Root Transform Position (Y)" settings. If available (more common for Humanoid, but check Generic options), try "Bake Into Pose" or ensure it's based on "Original" or "Feet".
//                     * If these options aren't granular enough (common for Generic), you will address this by editing animation curves later (Step 3).
//                 * Click **Apply**.
// 3.  **Import Separate Animation Files (if any):**
//     * If your animations are in separate FBX files, import them similarly. Their "Rig" tab should usually be set to match the main model's rig type.

// ### 2. Create the Pokémon Model Prefab

// 1.  **Instantiate Model in Scene:** Drag the imported Darmanitan model file from the Project window into your Scene Hierarchy.
// 2.  **Initial Transform Adjustments (T-Pose):**
//     * With the model instance selected in the Hierarchy, ensure its Transform `Position` is `(0,0,0)`, `Rotation` is `(0,0,0)`, and `Scale` is appropriate (e.g., `(1,1,1)` or a suitable size for your game).
// 3.  **Add Animator Component:**
//     * With the model instance still selected, in the Inspector, click **"Add Component"**.
//     * Search for and add an **`Animator`** component.
// 4.  **Assign Base Animator Controller:**
//     * In the `Animator` component, find the **"Controller"** slot.
//     * Drag your **base `RuntimeAnimatorController`** (the one that has placeholder states/clips for Idle and Attack, e.g., `PokemonBaseAnimatorController` or `SneaselTestController` if you used that as a base) from your Project window into this "Controller" slot.
// 5.  **Position for AR (Crucial for Correct Snapping):**
//     * This step ensures Darmanitan's feet are on its origin (0,0,0) point *within the prefab*.
//     * If the model's pivot point isn't at its feet:
//         * You might need to create an empty GameObject as a parent. Set this parent's position to (0,0,0).
//         * Make the Darmanitan model a child of this empty parent.
//         * Select the **Darmanitan model child** and adjust its **local `Position Y`** so its feet are resting at the origin (Y=0) of its parent. Adjust X and Z as needed for centering.
//     * The goal is that the *root GameObject of this prefab*, when placed at (0,0,0), should have Darmanitan standing correctly.
// 6.  **Create Prefab:**
//     * Drag the configured Darmanitan GameObject (the root one, with the Animator and correct local positioning) **from the Hierarchy window into your Project window** (e.g., into `Assets/Prefabs/Pokemon/`).
//     * Name it appropriately (e.g., `Darmanitan_Prefab`).
//     * You can now delete the instance from the Hierarchy.

// ### 3. Prepare Animation Clips (Especially for Generic Rigs)

// This step is crucial if your animations have unwanted root motion (like moving the whole model up/down) that couldn't be fixed by import settings alone.

// 1.  **Duplicate Clips from FBX (if embedded):**
//     * In the Project window, find your imported Darmanitan FBX file.
//     * Expand it to see the animation clips (e.g., `Darmanitan_Idle`, `Darmanitan_Attack1`).
//     * Select the Idle clip, then press **Ctrl+D** (or Cmd+D on Mac) to duplicate it. This creates an editable `.anim` file (e.g., `Darmanitan_Idle_Edited.anim`).
//     * Do the same for your Attack1 animation clip (e.g., `Darmanitan_Attack1_Edited.anim`).
//     * These `_Edited.anim` files are what you will use in your `PokemonData` asset.
// 2.  **Edit Duplicated Clips (Remove Unwanted Positional Data):**
//     * Select your `Darmanitan_Prefab` in the Project window and drag an instance of it into the scene temporarily.
//     * Select this instance in the Hierarchy.
//     * Open the **Animation window** (Window -> Animation -> Animation).
//     * From the clip dropdown in the Animation window, select your `Darmanitan_Idle_Edited.anim`.
//     * **Remove Root Position Curves:** Look for properties like `[RootObjectName] : Position X`, `Position Y`, `Position Z`. If these exist and are causing the model to shift off the card, select each property, right-click, and choose **"Remove Property"**.
//     * Do the same for your `Darmanitan_Attack1_Edited.anim`. The attack animation should primarily contain bone rotations for the attack *gesture*, not move the entire model's root position (unless it's a lunge that you specifically want and have accounted for).
//     * **Ensure Idle Loops:** Select `Darmanitan_Idle_Edited.anim` in the Project window. In the Inspector, ensure **"Loop Time"** is checked.
//     * **Apply Overrides to Prefab (if you edited directly on a prefab instance):** If you made changes to animation curves while the prefab instance was selected, select the instance in the Hierarchy, and at the top of the Inspector, click "Overrides" -> "Apply All" to save these changes back to the `Darmanitan_Prefab`. Then delete the temporary instance from the scene. (This step is more for when you edit `.anim` files that are directly part of a prefab variant, less so for FBX-imported clips that you've duplicated).

// ### 4. Configure the Base Animator Controller (If Not Already Generic Enough)

// Your `ARPokemonInstance` script uses an `AnimatorOverrideController`. This means your base Animator Controller (e.g., `PokemonBaseAnimatorController`) needs states that will have their "Motion" (animation clip) overridden.

// 1.  **Open your Base Animator Controller** (e.g., `PokemonBaseAnimatorController`).
// 2.  **Ensure States Exist:**
//     * Have an **Idle State** (e.g., named `Idle_Placeholder_State`). This should be your default state (orange, with "Entry" pointing to it).
//     * Have an **Attack State** (e.g., named `Attack_Placeholder_State`).
// 3.  **Assign Placeholder Clips to States:**
//     * Select the `Idle_Placeholder_State`. In the Inspector, for its "Motion" field, assign a generic or placeholder idle animation clip. **Note down the exact name of this clip asset** (e.g., `Generic_Idle_Placeholder.anim`).
//     * Select the `Attack_Placeholder_State`. In its "Motion" field, assign a generic or placeholder attack animation clip. **Note down the exact name of this clip asset** (e.g., `Generic_Attack_Placeholder.anim`).
//     * *These placeholder clip names are what you will use as keys in your `ARPokemonInstance` script for the override controller.*
// 4.  **Parameters:** Ensure you have a **Trigger** parameter (e.g., named `Attack1`) for initiating the attack.
// 5.  **Transitions:**
//     * `Idle_Placeholder_State` -> `Attack_Placeholder_State`: Conditioned by the `Attack1` trigger. "Has Exit Time" should be **unchecked**.
//     * `Attack_Placeholder_State` -> `Idle_Placeholder_State`: "Has Exit Time" should be **checked**, "Exit Time" around `0.9`-`1.0`. No conditions.

// ### 5. Create and Configure `PokemonData` Asset for Darmanitan

// 1.  **Create Asset:** In your Project window (e.g., in `Assets/Data/PokemonData/`), right-click -> Create -> Pokemon -> Pokemon Data (or whatever your menu path is).
// 2.  **Name it:** `DarmanitanData`.
// 3.  **Fill in Inspector Fields:** Select `DarmanitanData.asset`.
//     * **Pokemon Name Internal:** "Darmanitan"
//     * **Description Internal:** (Add a description)
//     * **Model Prefab Internal:** Drag your `Darmanitan_Prefab` (from Step 2) here.
//     * **Image Target Texture Internal:** (Optional, for reference) Drag the Darmanitan card image here.
//     * **Idle Animation Internal:** Drag your `Darmanitan_Idle_Edited.anim` (from Step 3) here.
//     * **Attack 1 Animation Internal:** Drag your `Darmanitan_Attack1_Edited.anim` (from Step 3) here.
//     * **Attack 1 Name Internal:** "Flare Blitz" (or Darmanitan's actual attack name).
//     * Fill in Typing, Stats, etc.

// ### 6. Set Up Vuforia Image Target

// 1.  **Add to Vuforia Database:**
//     * Go to the Vuforia Developer Portal.
//     * Add your Darmanitan card image to your target database.
//     * Download the updated database (`.unitypackage`) and import it into your Unity project.
// 2.  **Create Image Target in Scene:**
//     * In your Unity scene, GameObject -> Vuforia Engine -> Image Target.
//     * Select the new Image Target.
//     * In the `Image Target Behaviour` component:
//         * Set **Type** to "From Database".
//         * Select your **Database**.
//         * Select the **Darmanitan Image Target** from the dropdown.
// 3.  **Attach `ARPokemonInstance` Script:**
//     * With the Darmanitan Image Target selected, click "Add Component" in the Inspector.
//     * Add your `ARPokemonInstance.cs` script.
// 4.  **Configure `ARPokemonInstance`:**
//     * **Pokemon Data:** Drag your `DarmanitanData.asset` (from Step 5) into this slot.
//     * **Attack Button:** Drag your UI Attack Button from the Hierarchy here.
//     * **Attack Button Text:** Drag the TextMeshProUGUI component of your attack button's text here.
// 5.  **Wire Up Vuforia Events:**
//     * On the Darmanitan Image Target, find the `DefaultTrackableEventHandler` component.
//     * In its `On Target Found ()` event, add a call to `ARPokemonInstance.OnTargetFound`.
//     * In its `On Target Lost ()` event, add a call to `ARPokemonInstance.OnTargetLost`.

// ### 7. Update `ARPokemonInstance.cs` Override Keys (Crucial!)

// 1.  Open your `ARPokemonInstance.cs` script.
// 2.  In the `SetupPokemon()` method, find these lines:
//     ```csharp
//     overrideController["Placeholder_Idle_Clip_Name_In_Controller"] = pokemonData.IdleAnimation;
//     overrideController["Placeholder_Attack_Clip_Name_In_Controller"] = pokemonData.Attack1Animation;
//     ```
// 3.  **Replace** `"Placeholder_Idle_Clip_Name_In_Controller"` with the **exact name of the placeholder animation clip you assigned to your Idle state in the base Animator Controller** (Step 4.3).
// 4.  **Replace** `"Placeholder_Attack_Clip_Name_In_Controller"` with the **exact name of the placeholder animation clip you assigned to your Attack state in the base Animator Controller** (Step 4.3).

// ### 8. Test

// * Build and run your AR application (or test in Play Mode with a webcam if your setup supports it).
// * Scan the Darmanitan card.
// * Verify:
//     * Darmanitan model appears correctly positioned.
//     * Idle animation plays and loops correctly.
//     * The attack button shows "Flare Blitz" (or the name you set).
//     * Pressing the button triggers the attack animation visually.

// This detailed process should help you successfully add Darmanitan and any future Pokémon to your AR project!
