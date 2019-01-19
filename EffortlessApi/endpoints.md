# Documentation for Endpoints

This documentation contains some of the most important endpoints used in the EMS app. There are more endpoints available on the API, though only the following ones have been used in the actual application.

## Appointments

Appointments are appointments for when a substitute (user with primary role `PrimaryRoleType.Substitute`). The `Owner` field is determining which substitute the appointment belongs to. If that field is `null`, the appointment is available to take/claim.

An appointment belongs to a `Work

### List Appointments

If a user with their primary role set to `PrimaryRoleType.Booker` (0) access this endpoint, all appointments for all substitutes will return.

If a user with their primary role set to `PrimaryRoleType.Substitute` (2) access this endpoint, only that user's appointments will return.

```http
GET /api/appointment
Status: 200 OK, 401 Unauthorized
Header: { "Authorization": "Bearer [token]"}
```

```json
[
    {
        "id": 122,
        "start": "2018-12-19T22:15:00",
        "stop": "2018-12-20T10:30:00",
        "break": 44,
        "workPeriod": {
            "id": 5,
            "name": "Teaching Assistant - January",
            "department": {
                "id": 5,
                "name": "IT Minds København",
                "pno": 1020627324,
                "address": {
                    "id": 13,
                    "street": "Finlandsgade 27C, st. tv.",
                    "no": -1,
                    "floor": null,
                    "side": null,
                    "city": "Aarhus N",
                    "state": null,
                    "zipCode": 8200,
                    "country": "Danmark"
                }
            },
            "appointments": [],
            "agreement": null,
            "start": "2018-01-21T00:00:00",
            "lastAppointmentDate": "2018-01-21T00:00:00",
            "active": true
        },
        "breakTimeSpan": "00:00:00.0000044",
        "approvedByOwner": false,
        "approvedByOwnerDate": "0001-01-01T00:00:00",
        "approvedByUserId": 0,
        "approvedBy": null,
        "approvedDate": "0001-01-01T00:00:00",
        "createdByUserId": 0,
        "createdBy": null,
        "createdDate": "0001-01-01T00:00:00",
        "earnings": 0
    }
]
```

### List All Available Appointments

All appointments where the `Owner` field is set to `null` is considered _available_ and can be taken/claimed by users with primary role `PrimaryRoleType.Substitute`.

```http
GET api/appointment/available
Status: 200 OK, 401 Unauthorized
Header: {"Authorization": "Bearer [token]"}
```

```json
[
    {
        "id": 122,
        "start": "2018-12-19T22:15:00",
        "stop": "2018-12-20T10:30:00",
        "break": 44,
        "workPeriod": {
            "id": 5,
            "name": "Teaching Assistant - January",
            "department": {
                "id": 5,
                "name": "IT Minds København",
                "pno": 1020627324,
                "address": {
                    "id": 13,
                    "street": "Finlandsgade 27C, st. tv.",
                    "no": -1,
                    "floor": null,
                    "side": null,
                    "city": "Aarhus N",
                    "state": null,
                    "zipCode": 8200,
                    "country": "Danmark"
                }
            },
            "appointments": [],
            "agreement": null,
            "start": "2018-01-21T00:00:00",
            "lastAppointmentDate": "2018-01-21T00:00:00",
            "active": true
        },
        "breakTimeSpan": "00:00:00.0000044",
        "approvedByOwner": false,
        "approvedByOwnerDate": "0001-01-01T00:00:00",
        "approvedByUserId": 0,
        "approvedBy": null,
        "approvedDate": "0001-01-01T00:00:00",
        "createdByUserId": 0,
        "createdBy": null,
        "createdDate": "0001-01-01T00:00:00",
        "earnings": 0
    }
]
```
