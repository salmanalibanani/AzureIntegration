$armOutputObj = $env:ServiceBusTemplateOutput | convertfrom-json
$connectionString = $armOutputObj.namespaceConnectionString.value
"##vso[task.setvariable variable=ServiceBusConnectionString;]$connectionString"