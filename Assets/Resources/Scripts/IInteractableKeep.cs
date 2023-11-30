using System;
using UnityEngine;

public interface IInteractableKeep
{
    /// <summary>
    /// Triggers <b>continuous</b> interaction with the
    /// object implementing this interface.
    /// </summary>
    /// <param name="sender">
    /// The gameoObject that caused the interaction with object implementing
    /// this interface. This type of interaction has to be either explicitely stopped or
    /// would finished by the time limit
    /// </param>
    /// /// <param name="maxTimesec">
    /// The maximum time in seconds after which continuous interaction would be stopped
    /// </param>
    void KeepInteract(GameObject sender, float maxTimesec);

    /// <summary>
    /// Stops the <b>continuous</b> interaction
    /// </summary>
    void StopInteract();
}

