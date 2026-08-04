using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    public class CompositeInteraction : MonoBehaviour, IInteractable
    {
        [SerializeField] private List<GameObject> interactableGameObjects;

        public void Interact()
        {
            foreach (var interactableGameObject in interactableGameObjects)
            {
                interactableGameObject.TryGetComponent(out IInteractable interactable);
                interactable?.Interact();
            }
        }
    }
}