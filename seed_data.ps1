$baseUrl = "https://student-course-registration-system-89fo.onrender.com/api"

function Post-Data {
    param (
        [string]$endpoint,
        [hashtable]$body
    )
    $url = "$baseUrl/$endpoint"
    try {
        $response = Invoke-RestMethod -Uri $url -Method Post -Body ($body | ConvertTo-Json) -ContentType "application/json"
        Write-Host "Created $endpoint - ID: $($response.id)" -ForegroundColor Green
        return $response
    }
    catch {
        Write-Host "Failed to create $endpoint : $_" -ForegroundColor Red
        return $null
    }
}

Write-Host "Starting Data Seeding..." -ForegroundColor Cyan

# 1. Create Instructors
$inst1 = Post-Data "Instructors" @{ name = "Dr. Rajesh Kumar"; department = "Computer Science" }
$inst2 = Post-Data "Instructors" @{ name = "Prof. Maya Patel"; department = "Physics" }
$inst3 = Post-Data "Instructors" @{ name = "Dr. Vikram Singh"; department = "Mathematics" }

if (-not $inst1) { Write-Error "Failed to create initial instructor. Aborting."; exit }

# 2. Create Students
$stu1 = Post-Data "Students" @{ name = "Amit Sharma"; email = "amit.sharma@example.com" }
$stu2 = Post-Data "Students" @{ name = "Priya Singh"; email = "priya.singh@example.com" }
$stu3 = Post-Data "Students" @{ name = "Rohan Verma"; email = "rohan.verma@example.com" }

# 3. Create Courses (Linking to Instructors)
$course1 = Post-Data "Courses" @{ title = "Algorithms 101"; credits = 4; instructorId = $inst1.id }
$course2 = Post-Data "Courses" @{ title = "Quantum Physics"; credits = 3; instructorId = $inst2.id }
$course3 = Post-Data "Courses" @{ title = "Linear Algebra"; credits = 3; instructorId = $inst3.id }

# 4. Create Enrollments (Linking Students to Courses)
if ($stu1 -and $course1) { Post-Data "Enrollments" @{ studentId = $stu1.id; courseId = $course1.id } }
if ($stu1 -and $course2) { Post-Data "Enrollments" @{ studentId = $stu1.id; courseId = $course2.id } }
if ($stu2 -and $course2) { Post-Data "Enrollments" @{ studentId = $stu2.id; courseId = $course2.id } }
if ($stu3 -and $course3) { Post-Data "Enrollments" @{ studentId = $stu3.id; courseId = $course3.id } }

Write-Host "Seeding Complete!" -ForegroundColor Cyan
