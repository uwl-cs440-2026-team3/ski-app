#!/bin/sh

# Admin login
ADMIN_TOKEN=$(curl -s -k -X POST -d '{"email" : "jvanderzee@apache.org", "password" : "admin"}' https://localhost:1041/login | awk -F'"' '{print $4}')

# Skier registration
curl -s -k -X POST -d '{"email" : "liamchen@noreply.com", "name" : "Liam Chen", "password" : "notsecure"}' https://localhost:1041/register
echo ""
curl -s -k -X POST -d '{"email" : "miajohnson@noreply.com", "name" : "Mia Johnson", "password" : "notsecure"}' https://localhost:1041/register
echo ""
curl -s -k -X POST -d '{"email" : "noahramirez@noreply.com", "name" : "Noah Ramirez", "password" : "notsecure"}' https://localhost:1041/register
echo ""
curl -s -k -X POST -d '{"email" : "avawilson@noreply.com", "name" : "Ava Wilson", "password" : "notsecure"}' https://localhost:1041/register
echo ""

# Coach registration
curl -s -k -X POST -d '{"email" : "annaberg@noreply.com", "name" : "Anna Berg", "password" : "notsecure"}' -H "Authorization: Bearer $ADMIN_TOKEN" https://localhost:1041/registercoach
echo ""
curl -s -k -X POST -d '{"email" : "ericnovak@noreply.com", "name" : "Eric Novak", "password" : "notsecure"}' -H "Authorization: Bearer $ADMIN_TOKEN" https://localhost:1041/registercoach
echo ""

# Course creation
curl -s -k -X POST -d '{"name" : "North Slope"}' -H "Authorization: Bearer $ADMIN_TOKEN" https://localhost:1041/course
echo ""

# Team creation
curl -s -k -X POST -d '{"name" : "Glacier", "skier1_email" : "liamchen@noreply.com", "skier2_email" : "miajohnson@noreply.com", "coach_email" : "annaberg@noreply.com"}' -H "Authorization: Bearer $ADMIN_TOKEN" https://localhost:1041/team
echo ""
curl -s -k -X POST -d '{"name" : "Summit", "skier1_email" : "noahramirez@noreply.com", "skier2_email" : "avawilson@noreply.com", "coach_email" : "ericnovak@noreply.com"}' -H "Authorization: Bearer $ADMIN_TOKEN" https://localhost:1041/team
echo ""

# Race scheduling (fails because race is in the past)
curl -k -X POST -d '{"name" : "Race 1", "team_a" : "Glacier", "team_b" : "Summit", "course" : "North Slope", "start" : "2026-02-20T10:00", "duration" : "60"}' -H "Authorization: Bearer $ADMIN_TOKEN" https://localhost:1041/schedule
echo ""

# Coach login
COACH_TOKEN=$(curl -s -k -X POST -d '{"email" : "annaberg@noreply.com", "password" : "notsecure"}' https://localhost:1041/login | awk -F'"' '{print $4}')

# Coach view
curl -s -k -X GET -H "Authorization: Bearer $COACH_TOKEN" https://localhost:1041/getmyteam
echo ""
curl -s -k -X GET -H "Authorization: Bearer $COACH_TOKEN" https://localhost:1041/getmyraces
echo ""

# Skier login
SKIER_TOKEN=$(curl -s -k -X POST -d '{"email" : "noahramirez@noreply.com", "password" : "notsecure"}' https://localhost:1041/login | awk -F'"' '{print $4}')

# Skier view
curl -s -k -X GET -H "Authorization: Bearer $SKIER_TOKEN" https://localhost:1041/getmyteam
echo ""
curl -s -k -X GET -H "Authorization: Bearer $SKIER_TOKEN" https://localhost:1041/getmyraces
echo ""
