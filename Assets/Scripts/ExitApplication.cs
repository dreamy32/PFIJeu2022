using UnityEngine;
public class ExitApplication : MonoBehaviour
{
    //Fonctionne seulement avec un build d'application de bureau (Pas WebGL ou Éditeur)
    public void Exit()
    {
        Application.Quit();
    }
}
