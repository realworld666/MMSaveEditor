

public class BonusWithdrawWidget : MonoBehaviour
{
    [SerializeField]
    private WithdrawPotEntry[] pots = new WithdrawPotEntry[3];
    [SerializeField]
    private Button confirmWithdraw;
    private WithdrawPotEntry mSelectedPot;

    private void Awake()
    {
        this.confirmWithdraw.onClick.AddListener(new UnityAction(this.OnConfirm));
    }

    public void Open()
    {
        this.gameObject.SetActive(true);
        this.confirmWithdraw.gameObject.SetActive(true);
        for (int index = 0; index < this.pots.Length; ++index)
            this.pots[index].Setup(this, Game.instance.player.team.financeController.finance);
        this.DeselectAll();
    }

    private void Update()
    {
        this.confirmWithdraw.interactable = (Object)this.mSelectedPot != (Object)null;
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
        this.confirmWithdraw.gameObject.SetActive(false);
    }

    public void Select(WithdrawPotEntry inPot)
    {
        this.DeselectAll();
        this.mSelectedPot = inPot;
    }

    private void DeselectAll()
    {
        this.mSelectedPot = (WithdrawPotEntry)null;
        for (int index = 0; index < this.pots.Length; ++index)
            this.pots[index].Deselect();
    }

    private void OnConfirm()
    {
        this.mSelectedPot.SendTransaction();
        UIManager.instance.dialogBoxManager.GetDialog<BonusPopup>().Hide();
    }
}
