$armOutputObj = $env:ServiceBusTemplateOutput | convertfrom-json
$evn:ServiceBusConnectionString = $armOutputObj.namespaceConnectionString.value