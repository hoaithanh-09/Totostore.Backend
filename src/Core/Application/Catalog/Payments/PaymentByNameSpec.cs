namespace Totostore.Backend.Application.Catalog.Payments;

public class PaymentByNameSpec : Specification<Payment>, ISingleResultSpecification
{
    public PaymentByNameSpec(string name) =>
        Query.Where(b => b.Name == name);
}