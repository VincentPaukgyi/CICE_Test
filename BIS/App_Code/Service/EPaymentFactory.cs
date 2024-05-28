public class EPaymentFactory
{
    
    public IEPayment CreateEPayment(string ePaymentType)
    {

        switch (ePaymentType)
        {
            case BISConstants.MoMoneyPay:
                return new MoMoney();
            case BISConstants.APlus:
                return new APlus();
            default:
                return null;
        }
    }
}