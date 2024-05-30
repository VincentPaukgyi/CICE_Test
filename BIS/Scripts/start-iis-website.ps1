#Fetch Instance Id
$InstanceId = Get-EC2InstanceMetadata -Category InstanceId
#Start IIS Service
Send-SSMCommand -DocumentName "AWS-RunPowerShellScript" -Parameter @{commands = "Start-Website -Name TEST_SITE"} -Target @{Key="instanceids";Values=@($InstanceId)}
