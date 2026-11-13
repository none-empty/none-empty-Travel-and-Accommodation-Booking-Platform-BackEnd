# Reservation availability

Reservation confirmation accepts dates, payment method, remarks, and room IDs with guest counts. It no longer accepts a hotel ID. The server derives the hotel from the selected rooms, rejects missing rooms and mixed hotels, and uses that hotel for pricing and confirmation emails.

Room creation, room updates, and room responses use `isBookable` instead of `status`. This flag controls whether a room is enabled for bookings; creating a reservation does not change it. The room search endpoint is an administrative text search, not a date availability search. Searching `true` or `false` matches the bookable flag.

Confirmation locks the selected room rows inside the transaction, verifies room ownership and capacity, then checks their reservation windows before inserting anything. `Scheduled` and `Checked_In` reservations block inventory. The overlap condition is `existing.CheckInDate < requested.CheckOutDate && existing.CheckOutDate > requested.CheckInDate`, allowing checkout and check-in on the same date. All future booking and rescheduling paths must acquire the same room locks before checking and writing reservations.

Any overlap rejects the whole booking with HTTP 409 and Problem Details containing a `conflicts` array:

```json
{
  "status": 409,
  "detail": "Some selected rooms have conflicting reservations.",
  "conflicts": [
    {
      "roomId": "c51c5d87-0149-44d9-ab02-581ba24ea8ce",
      "windows": [
        { "checkInDate": "2026-12-10T00:00:00", "checkOutDate": "2026-12-14T00:00:00" }
      ]
    }
  ]
}
```

Windows describe existing blocking reservations, sorted by check-in date and deduplicated per room. Guest information is excluded. Disabled rooms also reject the booking with HTTP 409.

Migration `20261113120000_ReplaceRoomStatusWithIsBookable` converts `Unavailable` to false and both `Available` and `In_Use` to true before dropping the old column. Existing reservations determine occupied dates after migration. Rolling back maps true to `Available` and false to `Unavailable`; it cannot reconstruct the old `In_Use` values. Apply the migration before running the updated API. Automatic startup migration runs only in Development when `Database:ApplyMigrationsOnStartup` is enabled.
