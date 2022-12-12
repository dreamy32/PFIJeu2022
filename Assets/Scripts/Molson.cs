public class Molson : InteractableObject
{
    protected override void Awake()
    {
        base.Awake();
        SingleUsage = false; //�viter de call l'animator pour rien

    }
    protected override void OnInteract()
    {
        gameObject.SetActive(false);
        SanitySystem.ResetSanity();
    }
}
