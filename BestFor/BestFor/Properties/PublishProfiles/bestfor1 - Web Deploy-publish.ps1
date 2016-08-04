[cmdletbinding(SupportsShouldProcess=$true)]
param($publishProperties=@{}, $packOutput, $pubProfilePath)

# to learn more about this file visit https://go.microsoft.com/fwlink/?LinkId=524327

try{
    if ($publishProperties['ProjectGuid'] -eq $null){
        $publishProperties['ProjectGuid'] = 'd6c11525-fdcd-416d-99e6-9ad2a0e5e910'
    }

    $publishModulePath = Join-Path (Split-Path $MyInvocation.MyCommand.Path) 'publish-module.psm1'
    Import-Module $publishModulePath -DisableNameChecking -Force

	# show all available powershell variables
	#Get-Variable | Out-String

	# copy specific files from project to output
	#$externalImagesSourcePath = 'C:\resources\external-images'
	#$externalImagesDestPath = (Join-Path "$packOutput\wwwroot" 'external-images')
	#if(-not (Test-Path $externalImagesDestPath)){
	#  -Item -Path $externalImagesDestPath -ItemType Directory
	#}
	#Get-ChildItem $externalImagesSourcePath -File | Copy-Item -Destination $externalImagesDestPath

	$items = $PWD.ToString() + "\ClearScriptV8-32.dll"
	Copy-Item -Path $items -Destination $packOutput
	$items = $PWD.ToString() + "\ClearScriptV8-64.dll"
	Copy-Item -Path $items -Destination $packOutput
	$items = $PWD.ToString() + "\v8-ia32.dll"
	Copy-Item -Path $items -Destination $packOutput
	$items = $PWD.ToString() + "\v8-x64.dll"
	Copy-Item -Path $items -Destination $packOutput

    # call Publish-AspNet to perform the publish operation
    Publish-AspNet -publishProperties $publishProperties -packOutput $packOutput -pubProfilePath $pubProfilePath
}
catch{
    "An error occurred during publish.`n{0}" -f $_.Exception.Message | Write-Error
}