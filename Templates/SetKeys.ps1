Write-Host "I am here xx"
Write-Host $env:ServiceBusTemplateOutput
$armOutputObj = $env:ServiceBusTemplateOutput | convertfrom-json
$env:ServiceBusConnectionString = $armOutputObj.namespaceConnectionString.value
Write-Host "I am here xy"
Write-Host "$env:ServiceBusConnectionString"
Write-Host $env:ServiceBusConnectionString