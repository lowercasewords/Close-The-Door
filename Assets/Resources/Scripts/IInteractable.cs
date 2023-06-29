using System;
public interface IInteractable
{
    /// <summary>
    /// Called upon when interaction with the player has happened
    /// </summary>
    /// <returns>
    /// True if interaction was handled
    /// </returns>
    bool Interact();
}

