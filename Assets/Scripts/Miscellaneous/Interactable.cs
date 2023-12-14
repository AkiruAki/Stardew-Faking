using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlantsSpaceName;
using UnityEngine.Events;

namespace Interaction
{
    public enum TypeInteraction
    {
        None,
        BucketFill,
        Hoe,
        Seeds
    }
    public class Interactable : MonoBehaviour
    {
        [SerializeField]
        [Range(1, 3)]
        int QuantityInteractions;

        // Add later a GUI change depending of range quantityinteractions
        public UnityEvent OnDoAfterInteractionOne;
        [SerializeField]
        TypeInteraction interactionOne;

        public UnityEvent OnDoAfterInteractionTwo;
        [SerializeField]
        TypeInteraction interactionTwo;

        public UnityEvent OnDoAfterInteractionThree;
        [SerializeField]
        TypeInteraction interactionThree;

        public bool DoInteraction(TypeInteraction _interaction)
        {
            for (int i = 0; i < QuantityInteractions; i++)
            {
                switch (i)
                {
                    case 0:
                        if (interactionOne == _interaction)
                        {
                            Debug.Log("Yuhuu, interaction correct");
                            OnDoAfterInteractionOne?.Invoke();
                            return true;
                        }
                        break;
                    case 1:
                        if (interactionTwo == _interaction)
                        {
                            Debug.Log("Yuhuu, interaction correct");
                            OnDoAfterInteractionTwo?.Invoke();
                            return true;
                        }
                        break;
                    case 2:
                        if (interactionThree == _interaction)
                        {
                            Debug.Log("Yuhuu, interaction correct");
                            OnDoAfterInteractionThree?.Invoke();
                            return true;
                        }
                        break;
                }
            }

            return false;
        }
    }
}

