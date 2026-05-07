# General API Constraints

All requests use JSON in the request body. Response formats are documented with each individual endpoint.

There are no constraints on the length of any fields in a JSON request or response. This is not good for security and may be deprecated and removed in future versions.

All request headers and bodies must use ASCII and all response headers and bodies will use ASCII.

All endpoints may reply with 4xx or 5xx responses as applicable and conforming with RFC 9110. Only response codes with special meaning for a particular endpoint are specifically listed in the documentation for the individual endpoints which follows.

Some endpoints are labelled as requiring an access role. These endpoints must be authenticated by providing an `Authenticate` header in the request containing a basic-auth bearer token (see the `/login` endpoint).

The following constraints apply to all request fields (they are not strictly guaranteed in responses). The server may respond with 400 Bad Request if any of these constraints are violated.

- All name fields (user names, team names, etc.) have a maximum length of 64 characters.
- Passwords have a minimum length of 8 characters, and a maximum length of 128 characters.
- The local part of an email (the part before the @) has a maximum length of 64 characters.
- The domain part of an email (the part after the @) has a maximum length of 255 characters.

### /register

#### Request

```json
{
  "email" : email,
  "name" : name,
  "password" : password
}
```

Registers a user with the specified email, name, and password. The email must be a well-formed email address; the behavior for ill-formed email addresses is undefined to reserve backwards compatability for when we implement error handling.

#### Response
* 201 Created - if the registration succeeded
* 409 Conflict - if the email has already been registered

The registration will fail if the email in the request has already been used to register a user (with any role). The name is not required to be unique.

### /login

#### Request

```json
{
  "email" : email,
  "password" : password
}
```

Logs in the user with the specified email if that user exists and the password is correct.

#### Response

* 200 OK - if the login was successful
* 403 Forbidden - if any part of the credentials did not match

If a login succeeds, the response body consists of the following JSON response:

```json
{
  "token" : token,
  "role" : role
}
```

The token field represents a bearer token which the client may use in subsequent requests to authorize certain endpoints. The role field will be one of "skier", "coach", or "admin", indicating the role of the logged-in user.

The response will be 403 Forbidden if the email has not been registered or the password did not match. Note that it can be determined from `/register` whether an email exists, so the 403 response for an unknown email should not be considered to provide much security.

### /registercoach

#### Request

```json
{
  "email" : email,
  "name" : name,
  "password" : password
}
```

Registers a coach with the specified email, name, and password. The email must be a well-formed email address; the behavior for ill-formed email addresses is undefined to reserve backwards compatability for when we implement error handling.

#### Response
* 201 Created - if the registration succeeded
* 409 Conflict - if the email has already been registered
* 403 Forbidden - Missing or invalid authorization token
* 400 Bad Request - Invalid JSON or missing required fields

The registration will fail if the email in the request has already been used to register a user (with any role). The name is not required to be unique.

### /schedule

#### Request

Requires access level: admin

```json
{
  "name" : race_name,
  "team_a" : team_name,
  "team_b" : team_name,
  "course" : course_name,
  "start" : datetime,
  "duration" : minutes,
}
```

Schedules a race between the specified teams on the specified course. The beginning of the race is given by the start field and must be in the format YYYY-MM-DDTHH:MM (T is a literal T character as required by ISO\_8601). The duration of the race is given as a nonnegative number of minutes.

The request will be rejected if the teams or course do not exist, either of the teams are already scheduled for a race within 30 minutes exclusive of this one, the course is already taken for a race within 30 minutes exclusive of this one, the start time is in the past at the time of processing the request, or another currently-active race already has the same name. Canceled races do not count toward the name-uniqueness check, so the name of a canceled race may be reused.

#### Response

* 201 Created - if the request succeeded
* 400 Bad Request - Invalid JSON or missing required fields
* 400 Bad Request - if the format of the start or duration field is invalid
* 400 Bad Request - if the start datetime is in the past
* 400 Bad Request - if team\_a and team\_b are the same team
* 409 Conflict - if any of the teams or courses conflict as described in the request section, or if another active race already has the requested name
* 403 Forbidden - Missing or invalid authorization token

### /cancelrace

#### Request

Requires access level: admin

```json
{
  "name" : race_name
}
```

Cancels a previously scheduled race, identified by name. The race row is preserved in the database with its status set to `CANCELED`, so the cancellation can be referenced later (for audits, notifications, or reporting). Canceled races are excluded from `/getraces` and `/getmyraces` results.

Because `/schedule` enforces that at most one race may be active with any given name at a time, identifying a race by name is unambiguous. If only canceled races exist with the requested name, the server treats this as if no such race exists and returns 404.

When a cancellation succeeds, all skiers and coaches assigned to either of the competing teams are notified through the server's configured notification mechanism. A notification delivery failure does not affect the HTTP response: the cancellation is still considered successful and a 200 response is returned.

#### Response

* 200 OK - if the race was canceled successfully
* 400 Bad Request - Invalid JSON or missing required fields
* 403 Forbidden - Missing or invalid authorization token, or caller is not an admin
* 404 Not Found - if no active race exists with the specified name

### /getmembers

#### Request

Requires access level: admin

Requests a list of all current users.

#### Response
* 200 OK - if the request succeeds
* 403 Forbidden - if the user requesting is not logged in as an admin

If the request succeeds, the response body consists of the following JSON response:

```json
[
  {
    "email" : email,
    "name" : name,
    "role" : role,
    "team" : team
  },
  ...
]
```

The response includes all users in the system at the time the request was processed who have the specified role. The email field can be used as a unique identifier (multiple users may have the same name). The users are guaranteed to be sorted by names in ascending, case-insensitive lexicographical order. The role field will be one of "skier", "coach", or "admin". The team field will contain the name of the team the member is assigned to, or the empty string if the member is not assigned to a team.

### /getteams

#### Request

Requires access level: admin

Requests a list of all current teams.

#### Response
* 200 OK - if the request succeeds
* 403 Forbidden - if the user requesting is not logged in as an admin

```json
[
  team_name,
  ...
]
```

The response includes all teams in the system at the time the request was processed.

### /getcourses

#### Request

Requires access level: admin

Requests a list of all courses.

#### Response
* 200 OK - if the request succeeds
* 403 Forbidden - if the user requesting is not logged in as an admin

```json
[
  course_name,
  ...
]
```

The response includes all courses in the system at the time the request was processed.

### /getraces

#### Request

Requires access level: admin

Requests a list of all non-archived races (all races that do not have results recorded yet).

#### Response
* 200 OK - if the request succeeds
* 403 Forbidden - if the user requesting is not logged in as an admin
If the request succeeds, the response body consists of the following JSON response:

```json
[
  {
    "raceid" : raceid,
    "name" : race_name,
    "teamA" : team_name,
    "teamB" : team_name,
    "course" : course_name,
    "start" : datetime,
    "end" : datetime
  },
  ...
]
```

The response includes all races in the system at the time the request was processed which are scheduled in the future and have not been canceled. The `raceid` field is the integer primary key of the race in the database; it is required when calling `/cancelrace`. The `start` and `end` fields will be in an unspecified date format suitable for displaying.

### /getmyteam

#### Request

Requires access level: skier

Requests the information for the team that the skier or coach is a member of.

#### Response
* 200 OK - if the request succeeds
* 403 Forbidden - if the user requesting is not logged in
* 404 Not Found - if the user is not on a team

If the request succeeds, the response body consists of the following JSON response:

```json
{
  "name" : team_name,
  "skiers" : [name, ...]
  "coach" : coach,
}
```

### /getmyraces

#### Request

Requires access level: skier

Requests all upcoming races that the skier or coach will be a participant in.

#### Response
* 200 OK - if the request succeeds
* 403 Forbidden - if the user requesting is not logged in

If the request succeeds, the response body consists of the following JSON response:

```json
[
  {
    "name" : race_name,
    "teamA" : team_name,
    "teamB" : team_name,
    "course" : course_name,
    "start" : datetime,
    "end" : datetime
  },
  ...
]
```

The response includes all future races the skier or coach is a participant in at the time the request is processed. The `start` and `end` fields will be in an unspecified date format suitable for displaying. Note that this response intentionally omits the `raceid` field that `/getraces` returns, since skiers and coaches do not have permission to cancel races. Races are returned in ascending order by start datetime.

### /mynotifications

#### Request

Requires access level: skier (any logged-in user)

Requests the calling user's notifications. Each user only ever sees their own notifications; the response is filtered server-side based on the bearer token.

#### Response

* 200 OK - if the request succeeds
* 403 Forbidden - if the user is not logged in

If the request succeeds, the response body is a JSON array, newest first:

```json
[
  {
    "notificationid" : notificationid,
    "type" : type,
    "message" : message,
    "created_at" : timestamp,
    "read_at" : timestamp
  },
  ...
]
```

The `type` field is one of `RACE_CANCELED`, `RACE_RESCHEDULED`, or `RACE_REMINDER`. The `message` field contains a human-readable summary suitable for direct display to the user. The `created_at` field is an ISO 8601 timestamp indicating when the notification was generated. The `read_at` field is `null` for unread notifications, or an ISO 8601 timestamp indicating when the user marked it read.

### /marknotificationread

#### Request

Requires access level: skier (any logged-in user)

```json
{
  "notificationid" : notificationid
}
```

Marks a single notification as read. Users can only mark their own notifications; attempting to mark another user's notification returns 404 (the server does not distinguish "not yours" from "doesn't exist" in the response).

The `notificationid` field is a string containing the integer ID of the notification, as returned by `/mynotifications`.

#### Response

* 200 OK - if the notification was marked read
* 400 Bad Request - Invalid JSON, missing fields, or non-integer notificationid
* 403 Forbidden - if the user is not logged in
* 404 Not Found - if no notification with the given id belongs to the calling user