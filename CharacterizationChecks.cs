namespace HarborDispatch;

internal static class CharacterizationChecks
{
    public static int Run()
    {
        var shipments = DispatchData.Shipments;
        ShipmentRow[] expectedSouthRows =
        [
            new("HD-104", "Ready for pickup", "Priority service"),
            new("HD-101", "Out for delivery", "Standard service"),
            new("HD-104", "Delivered", "Standard service")
        ];

        var editableShipments = shipments.ToList();
        var snapshot = ShipmentReport.GetForDepot(editableShipments, "South");
        editableShipments.Clear();

        (string Name, bool Passed)[] checks =
        [
            ("Null enum returns empty text", StatusText.GetDescription(null) == string.Empty),
            ("Shipment description matches the attribute", StatusText.GetDescription(ShipmentStatus.Queued) == "Ready for pickup"),
            ("Service description matches the attribute", StatusText.GetDescription(ServiceLevel.Priority) == "Priority service"),
            ("Enum without a description uses its name", StatusText.GetDescription(ShipmentStatus.Unassigned) == "Unassigned"),
            ("Undefined enum uses its numeric text", StatusText.GetDescription((ShipmentStatus)999) == "999"),
            ("Other enum types use their name", StatusText.GetDescription(DayOfWeek.Friday) == "Friday"),
            ("Report keeps order, duplicates, and labels", ShipmentReport.GetForDepot(shipments, "sOUTH").SequenceEqual(expectedSouthRows)),
            ("Unknown depot has no rows", ShipmentReport.GetForDepot(shipments, "West").Count == 0),
            ("Empty source has no rows", ShipmentReport.GetForDepot([], "South").Count == 0),
            ("Tracking returns the first match by reference", ReferenceEquals(ShipmentReport.FindByTrackingCode(shipments, "hd-104"), shipments[0])),
            ("Unknown tracking code returns null", ShipmentReport.FindByTrackingCode(shipments, "HD-000") is null),
            ("Empty source returns null for tracking", ShipmentReport.FindByTrackingCode([], "HD-104") is null),
            ("Report remains a snapshot", snapshot.SequenceEqual(expectedSouthRows))
        ];

        foreach (var (name, passed) in checks)
        {
            Console.WriteLine($"{(passed ? "PASS" : "FAIL")} {name}");
        }

        var passCount = checks.Count(check => check.Passed);
        Console.WriteLine($"Passed {passCount} of {checks.Length} cases.");
        return passCount == checks.Length ? 0 : 1;
    }
}
