using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickX : MonoBehaviour
{
    private CoffeeSalesman theCoffeeSalesman;
    private WineSalesman theWineSalesman;
    private GolfSalesman theGolfSalesman;
    private SmokeSalesman theSmokeSalesman;
    private ConvienceSalesman theConvienceSalesman;

    void Start()
    {
        theCoffeeSalesman = FindObjectOfType<CoffeeSalesman>();
        theSmokeSalesman = FindObjectOfType<SmokeSalesman>();
        theWineSalesman = FindObjectOfType<WineSalesman>();
        theGolfSalesman = FindObjectOfType<GolfSalesman>();
        theConvienceSalesman = FindObjectOfType<ConvienceSalesman>();
    }

    public void OnCoffeeQuit()
    {
        theCoffeeSalesman.CloseCoffeeStore();
    }

    public void OnWineQuit()
    {
        theWineSalesman.CloseWineStore();
    }

    public void OnGolfQuit()
    {
        theGolfSalesman.CloseGolfStore();
    }

    public void OnSmokeQuit()
    {
        theSmokeSalesman.CloseSmokeStore();
    }

    public void OnConvienceQuit()
    {
        theConvienceSalesman.CloseConvienceStore();
    }
}
