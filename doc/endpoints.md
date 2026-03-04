# General API Constraints

All requests use JSON in the request body. Response formats are documented with each individual endpoint.

There are no constraints on the length of any fields in a JSON request or response. This is not good for security and may be deprecated and removed in future versions.

All request headers and bodies must use ASCII and all response headers and bodies will use ASCII.

All endpoints may reply with 4xx or 5xx responses as applicable and conforming with RFC 9110. Only response codes with special meaning for a particular endpoint are specifically listed in the documentation for the individual endpoints which follows.

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

If a login succeeds, the response includes the following JSON response:

```json
{
  "token" : token,
  "role" : role
}
```

The token field represents a bearer token which the client may use in subsequent requests to authorize certain endpoints. The role field will be one of "skier", "coach", or "admin", indicating the role of the logged-in user.

The response will be 403 Forbidden if the email has not been registered or the password did not match. Note that it can be determined from `/register` whether an email exists, so the 403 response for an unknown email should not be considered to provide much security.
