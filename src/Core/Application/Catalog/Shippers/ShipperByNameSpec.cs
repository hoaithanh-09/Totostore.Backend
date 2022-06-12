namespace Totostore.Backend.Application.Catalog.Shippers;

public class ShipperByNameSpec : Specification<Shipper>, ISingleResultSpecification
{
    public ShipperByNameSpec(string name) =>
        Query.Where(b => b.Name == name);
}