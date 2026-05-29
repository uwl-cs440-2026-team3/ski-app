# Admin login
ADMIN_TOKEN=$(curl -s -k -X POST -d '{"email" : "jvanderzee@apache.org", "password" : "adminpassword"}' https://localhost:1041/login | awk -F'"' '{print $4}')

# Race scheduling (fails because race is in the past)
curl -k -X POST -d '{"name" : "Race 1", "team_a" : "Glacier", "team_b" : "Summit", "course" : "North Slope", "start" : "2026-04-10T10:00", "duration" : "60"}' -H "Authorization: Bearer $ADMIN_TOKEN" https://localhost:1041/schedule
echo ""

# Course creation
curl -s -k -X POST -d '{"name" : "East Ridge"}' -H "Authorization: Bearer $ADMIN_TOKEN" https://localhost:1041/course
echo ""

# Skier registration
curl -s -k -X POST -d '{"email" : "danakeller@noreply.com", "name" : "Dana Keller", "password" : "notsecure"}' https://localhost:1041/register
echo ""
curl -s -k -X POST -d '{"email" : "lukeriley@noreply.com", "name" : "Luke Riley", "password" : "notsecure"}' https://localhost:1041/register
echo ""
curl -s -k -X POST -d '{"email" : "jamiejohnson@noreply.com", "name" : "Jamie Johnson", "password" : "notsecure"}' https://localhost:1041/register
echo ""
curl -s -k -X POST -d '{"email" : "carolpark@noreply.com", "name" : "Carol Park", "password" : "notsecure"}' https://localhost:1041/register
echo ""

# Coach registration
curl -s -k -X POST -d '{"email" : "alexmorgan@noreply.com", "name" : "Alex Morgan", "password" : "notsecure"}' -H "Authorization: Bearer $ADMIN_TOKEN" https://localhost:1041/registercoach
echo ""
curl -s -k -X POST -d '{"email" : "ainianderson@noreply.com", "name" : "Aini Anderson", "password" : "notsecure"}' -H "Authorization: Bearer $ADMIN_TOKEN" https://localhost:1041/registercoach
echo ""

# Team creation
curl -s -k -X POST -d '{"name" : "Racers", "skier1_email" : "danakeller@noreply.com", "skier2_email" : "lukeriley@noreply.com", "coach_email" : "alexmorgan@noreply.com"}' -H "Authorization: Bearer $ADMIN_TOKEN" https://localhost:1041/team
echo ""
curl -s -k -X POST -d '{"name" : "Flyers", "skier1_email" : "jamiejohnson@noreply.com", "skier2_email" : "carolpark@noreply.com", "coach_email" : "ainianderson@noreply.com"}' -H "Authorization: Bearer $ADMIN_TOKEN" https://localhost:1041/team
echo ""

# enter race 1 times

# cancel race two