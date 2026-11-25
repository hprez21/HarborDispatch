namespace HarborDispatch;

public static class ShipmentReport
{
    public static IReadOnlyList<ShipmentRow> GetForDepot(IEnumerable<Shipment> shipments, string depot)
    {
        var result = new List<ShipmentRow>();

        // Traverse in input order and retain every matching shipment.
        foreach (var shipment in shipments)
        {
            if (string.Equals(shipment.Depot, depot, StringComparison.OrdinalIgnoreCase))
            {
                result.Add(new ShipmentRow(
                    shipment.TrackingCode,
                    StatusText.GetDescription(shipment.Status),
                    StatusText.GetDescription(shipment.Service)));
            }
        }

        return result;
    }

    public static Shipment? FindByTrackingCode(IEnumerable<Shipment> shipments, string trackingCode)
    {
        foreach (var shipment in shipments)
        {
            if (string.Equals(shipment.TrackingCode, trackingCode, StringComparison.OrdinalIgnoreCase))
            {
                return shipment;
            }
        }

        return null;
    }
}
