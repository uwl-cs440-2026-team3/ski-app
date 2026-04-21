#!/bin/sh

curl -k -X POST -d '{"name" : "red", "skier1_email" : "", "skier2_email" : "", "coach_email" : ""}' https://localhost:1041/team
echo ""
curl -k -X POST -d '{"name" : "blue"}' https://localhost:1041/team
echo ""
curl -k -X POST -d '{"name" : "green"}' https://localhost:1041/team
echo ""
curl -k -X POST -d '{"name" : "white"}' https://localhost:1041/team
echo ""
curl -k -X POST -d '{"name" : "hill"}' https://localhost:1041/course
echo ""
curl -k -X POST -d '{"name" : "plain"}' https://localhost:1041/course
echo ""

curl -k -X POST -d '{"name" : "Race 2", "team_a" : "blue", "team_b" : "white", "course" : "plain", "start" : "2028-01-01T12:00", "duration" : "5"}' https://localhost:1041/schedule
echo ""

curl -k -X GET https://localhost:1041/getraces
echo ""
