# General API Constraints

All requests use JSON in the request body. Response formats are documented with each individual endpoint.

There are no constraints on the length of any fields in a JSON request or response. This is not good for security and may be deprecated and removed in future versions.

All request headers and bodies must use ASCII and all response headers and bodies will use ASCII.

All endpoints may reply with 4xx or 5xx responses as applicable and conforming with RFC 9110. Only response codes with special meaning for a particular endpoint are specifically listed in the documentation for the individual endpoints which follows.

Some endpoints are labelled as requiring an access role. These endpoints must be authenticated by providing an `Authenticate` header in the request containing a basic-auth bearer token (see the `/login` endpoint).

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

The request will be rejected if the teams or course do not exist, either of the teams are already scheduled for a race within 30 minutes exclusive of this one, the course is already taken for a race within 30 minutes exclusive of this one, or the start time is in the past at the time of processing the request.

#### Response

* 201 Created - if the request succeeded
* 400 Bad Request - Invalid JSON or missing required fields
* 400 Bad Request - if the format of the start or duration field is invalid
* 400 Bad Request - if the start datetime is in the past
* 400 Bad Request - if team\_a and team\_b are the same team
* 409 Conflict - if any of the teams or courses conflict as described in the request section.
* 403 Forbidden - Missing or invalid authorization token

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
    "name" : race_name,
    "team_a" : team_name,
    "team_b" : team_name,
    "course" : course_name,
    "start" : datetime,
    "end" : datetime
  },
  ...
]
```

The response includes all races in the system at the time the request was processed which are scheduled in the future. The start and end fields will be in an unspecified date format suitable for displaying.
