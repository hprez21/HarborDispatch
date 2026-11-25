using HarborDispatch;

var mode = DemoMode.DepotReport;
const string depotToReport = "sOUTH";
const string trackingCode = "hd-104";

if (mode == DemoMode.Checks)
{
    return CharacterizationChecks.Run();
}

if (mode == DemoMode.TrackShipment)
{
    var shipment = ShipmentReport.FindByTrackingCode(DispatchData.Shipments, trackingCode);
    if (shipment is null)
    {
        Console.WriteLine("Shipment not found.");
        return 1;
    }

    Console.WriteLine($"{shipment.TrackingCode} | {shipment.Depot} | {StatusText.GetDescription(shipment.Status)}");
    return 0;
}

var rows = ShipmentReport.GetForDepot(DispatchData.Shipments, depotToReport);
Console.WriteLine($"Depot report: {depotToReport}");
foreach (var row in rows)
{
    Console.WriteLine($"{row.TrackingCode} | {row.Status} | {row.Service}");
}

return 0;

enum DemoMode
{
    DepotReport,
    TrackShipment,
    Checks
}
