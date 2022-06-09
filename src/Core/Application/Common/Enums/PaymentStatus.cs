namespace Totostore.Backend.Application.Common.Enums;

public enum PaymentStatus
{
    UnpaidInvoice,//chua thanh toan
    PayAllMoney, //da thanh toan
    PaymentByInstalment,  //tra gop
    Canceled //huy
}