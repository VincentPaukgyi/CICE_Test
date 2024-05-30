#Fetch Instance Id
$InstanceId = Get-EC2InstanceMetadata -Category InstanceId
#Stop IIS Service
Send-SSMCommand -DocumentName "AWS-RunPowerShellScript" -Parameter @{commands = "Stop-Website -Name TEST_SITE"} -Target @{Key="instanceids";Values=@($InstanceId)}
