using UnityEngine;

/// <summary>
/// Interface for the object that could be interacted with by pressing the Interaction Key once
/// </summary>
public interface IInteractable
{
    /// <summary>
    /// Called upon when something started to interact with the
    /// object implementing this interface
    /// </summary>
    /// <param name="sender">
    /// The gameoObject that caused the interaction with object implementing
    /// this interface
    /// </param>
    /// <returns>
    /// True if interaction was handled
    /// </returns>
    bool Interact(GameObject sender);
}

