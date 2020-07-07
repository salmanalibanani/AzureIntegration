$armOutputObj = $env:ServiceBusTemplateOutput | convertfrom-json
$env:ServiceBusConnectionString = $armOutputObj.namespaceConnectionString.value