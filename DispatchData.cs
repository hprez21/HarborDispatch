using System.ComponentModel;

namespace HarborDispatch;

public enum ShipmentStatus
{
    [Description("Ready for pickup")]
    Queued,

    [Description("Out for delivery")]
    InTransit,

    [Description("Delivered")]
    Delivered,

    Unassigned
}

public enum ServiceLevel
{
    [Description("Standard service")]
    Standard,

    [Description("Priority service")]
    Priority
}

public sealed record Shipment(string TrackingCode, string Depot, ShipmentStatus Status, ServiceLevel Service);

public sealed record ShipmentRow(string TrackingCode, string Status, string Service);

public static class DispatchData
{
    public static IReadOnlyList<Shipment> Shipments { get; } =
    [
        new("HD-104", "South", ShipmentStatus.Queued, ServiceLevel.Priority),
        new("HD-203", "North", ShipmentStatus.Delivered, ServiceLevel.Standard),
        new("HD-101", "SOUTH", ShipmentStatus.InTransit, ServiceLevel.Standard),
        new("HD-104", "South", ShipmentStatus.Delivered, ServiceLevel.Standard)
    ];
}
