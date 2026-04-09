# CS440 Ski-App

A ski league management system for academic demonstration purposes.

## Description 

Text TODO.

## Roadmap

- SERVER (endpoint names largely as placeholder, can be changed, note if they get changed)
* [x] Endpoint for retrieving users. /getmembers
* [x] Team field for getmember endpoint
* [ ] Endpoint for retrieving teams. /getteams
* [ ] Endpoint for retrieving team for a user. /getmyteam
* [ ] Endpoint for retrieving courses. /getcourses
* [x] Endpoint for scheduling races. /schedule
* [x] Endpoint for retrieving races. /getraces
* [ ] Endpoint for retrieving races for a user. /getmyraces
* [ ] Endpoint for inserting race times. /postscore
* [ ] Endpoint for cancelling races. /cancel (LOW PRIORITY FOR DEMO2)
* [ ] Endpoint for rescheduling races. /reschedule (NOT FOR DEMO2)
* [ ] Send email reminders of upcoming races to appropriate users.

- CLIENT (endpoint names largely as placeholder, can be changed, note if they get changed)
* [ ] Finish user assignment forms when /getmember endpoint is created
* [ ] Finish and wire race schedule form when /geteams, /getcourse, and /schedulerace become avaliable.
* [ ] Create a race cancelling from when /getrace and /cancelrace become avaliable 
* [ ] Determine what the race time inserting form will even look like.
* [ ] Make the race time inserting form.
* [ ] Figure out how rescheduling will work.
* [ ] Create coach and skier forms when related endpoints become avaliable.
* [ ] Display conflict and other endpoint errors appropiately.
* [ ] Imploy validation and other constraints when decided upon.


- VALIDATION, CONSTRAINT, SECURITY
* [ ] Input constraints for all endpoints and perform validation on server-side.
* [ ] Possibly some redundant validation on client side as well for fast feedback and to save bandwidth.
* [ ] Expire session tokens after a set amount of time with no requests.
* [ ] SSL Cert

- MISC
* [ ] Add team endpoint to documentation.

## Usage

Text TODO.

## Building/Running

Text TODO.

## Notes

Text TODO.
