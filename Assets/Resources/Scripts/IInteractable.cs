using UnityEngine;

/// <summary>
/// Interface for the object that could be interacted with by pressing the Interaction Key once
/// </summary>
public interface IInteractable
{
    /// <summary>
    /// Called upon when interaction with the player has happened
    /// </summary>
    /// <param name="sender">
    /// The player that made the action
    /// </param>
    /// <returns>
    /// True if interaction was handled
    /// </returns>
    bool Interact(GameObject sender);
}

