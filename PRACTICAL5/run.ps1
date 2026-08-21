# ==============================================================================
# Run Practical 5 (ASP.NET Web Forms) in Antigravity IDE / Terminal
# ==============================================================================

$projectDir = Join-Path $PSScriptRoot "AcademicLeaveManagement\AcademicLeaveManagement"
$solutionPath = Join-Path $PSScriptRoot "AcademicLeaveManagement\AcademicLeaveManagement.sln"
$msBuildPath = "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe"
$iisExpressPath = "C:\Program Files\IIS Express\iisexpress.exe"
$port = 5055

Write-Host "================================================================" -ForegroundColor Cyan
Write-Host "  Practical 5: Academic Calendar and Leave Management System    " -ForegroundColor Cyan
Write-Host "================================================================" -ForegroundColor Cyan

# 1. Build the solution using MSBuild
Write-Host "`n[1/2] Building ASP.NET Web Forms Solution..." -ForegroundColor Yellow
if (Test-Path $msBuildPath) {
    & "$msBuildPath" "$solutionPath" /t:Build /p:Configuration=Debug /v:m
    if ($LASTEXITCODE -ne 0) {
        Write-Host "Build failed! Please inspect errors above." -ForegroundColor Red
        exit $LASTEXITCODE
    }
    Write-Host "Build succeeded (0 errors, 0 warnings)." -ForegroundColor Green
} else {
    Write-Host "MSBuild not found at: $msBuildPath" -ForegroundColor Red
    exit 1
}

# 2. Launch IIS Express
Write-Host "`n[2/2] Starting IIS Express Web Server on Port $port..." -ForegroundColor Yellow
Write-Host "URL: http://localhost:$port/Login.aspx" -ForegroundColor Cyan
Write-Host "Credentials -> Username: student | Password: 12345`n" -ForegroundColor Green
Write-Host "Press Ctrl+C in this terminal to stop the server.`n" -ForegroundColor Gray

if (Test-Path $iisExpressPath) {
    & "$iisExpressPath" "/path:$projectDir" "/port:$port"
} else {
    Write-Host "IIS Express not found at: $iisExpressPath" -ForegroundColor Red
    exit 1
}
