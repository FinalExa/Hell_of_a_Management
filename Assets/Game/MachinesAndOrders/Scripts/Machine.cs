using System;
using System.Collections.Generic;
using UnityEngine;
public class Machine : MonoBehaviour, ICanUseIngredients, ICanBeInteracted
{
    public static Action deactivateRecipeInstruction;
    public static Action deactivateProduceOrderInstruction;
    [HideInInspector] public List<OrdersData.OrderIngredients> recipe;
    [HideInInspector] public GameObject Self { get; set; }
    [HideInInspector] public bool IsInsidePlayerRange { get; set; }
    [SerializeField] int recipeMaxLimit;
    [SerializeField] private GameObject thisOrder;
    [SerializeField] GameObject orderOutputPosition;
    private HighlightableMachine highlightableMachine;
    private IHaveIngredientLights thisLightObject;
    [SerializeField] private bool isTutorial;
    private bool tutorialRecipeInstructionDone;
    private bool tutorialProduceOrderInstructionDone;

    private void Awake()
    {
        highlightableMachine = this.gameObject.GetComponent<HighlightableMachine>();
        thisLightObject = this.gameObject.GetComponent<IHaveIngredientLights>();
        deactivateRecipeInstruction += DeactivateRecipeInstruction;
        deactivateProduceOrderInstruction += DeactivateProduceOrderInstruction;
    }
    private void Start()
    {
        tutorialRecipeInstructionDone = false;
        tutorialProduceOrderInstructionDone = false;
        Self = this.gameObject;
        highlightableMachine.miniDialogueWithText.SetupPosition();
    }

    public void RecipeFill(OrdersData.OrderIngredients ingredientType, SoulController source)
    {
        if (source.soulReferences.soulThrowableObject.isFlying && recipe.Count < recipeMaxLimit)
        {
            PlayMachineSound();
            recipe.Add(ingredientType);
            source.gameObject.SetActive(false);
            source.transform.localPosition = Vector3.zero;
            thisLightObject.ActivateLight(this);
            if (recipe.Count == recipeMaxLimit) ProduceOrder();
            if (isTutorial && !tutorialRecipeInstructionDone)
            {
                Tutorial.instance.CheckIndex();
                deactivateRecipeInstruction();
            }
        }
    }

    private void PlayMachineSound()
    {
        if (recipe.Count != recipeMaxLimit - 1) AudioManager.instance.Play("Machine_Soul_1_2");
        else PlayDependingOnType();
    }

    private void PlayDependingOnType()
    {
        if (thisOrder.name == "Dish") AudioManager.instance.Play("Dish_LastSoul");
        else AudioManager.instance.Play("Drink_LastSoul");
    }
    public void ProduceOrder()
    {
        PlayDependingOnType();
        GameObject obj = Instantiate(thisOrder, orderOutputPosition.transform);
        obj.GetComponent<Order>().SetupOrderIngredients(recipe);
        recipe.Clear();
        thisLightObject.ResetAllLights();
        highlightableMachine.miniDialogueWithText.DeactivateDialogue();
        if (isTutorial && !tutorialProduceOrderInstructionDone)
        {
            Tutorial.instance.ShowTutorialScreen();
            deactivateProduceOrderInstruction();
        }
    }

    public void Interaction()
    {
        if (recipe.Count > 0) ProduceOrder();
    }

    private void DeactivateRecipeInstruction()
    {
        tutorialRecipeInstructionDone = true;
    }
    private void DeactivateProduceOrderInstruction()
    {
        tutorialProduceOrderInstructionDone = true;
    }
}
