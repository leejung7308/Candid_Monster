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
    private OnlineMarket theOnlineMarket;
    private ComputerClick theComputerClick;
    private StorageClick theStorageClick;

    void Start()
    {
        theCoffeeSalesman = FindObjectOfType<CoffeeSalesman>();
        theSmokeSalesman = FindObjectOfType<SmokeSalesman>();
        theWineSalesman = FindObjectOfType<WineSalesman>();
        theGolfSalesman = FindObjectOfType<GolfSalesman>();
        theConvienceSalesman = FindObjectOfType<ConvienceSalesman>();
        theOnlineMarket = FindObjectOfType<OnlineMarket>();
        theComputerClick = FindObjectOfType<ComputerClick>();
        theStorageClick = FindObjectOfType<StorageClick>();
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

    public void OnMarketQuit()
    {
        theOnlineMarket.CloseOnlineMarket();
        theComputerClick.CloseComputer();
    }

    public void OnStorageQuit()
    {
        theStorageClick.CloseStorage();
    }
}
