$location = 'australiaeast'
$rgName = 'AZ204'

#New-AzResourceGroup -Name $rgName -Location $location

New-AzVm `
    -ResourceGroupName $rgName `
    -Name "myVM" `
    -Location $location `
    -VirtualNetworkName "myVnet" `
    -SubnetName "mySubnet" `
    -SecurityGroupName "myNetworkSecurityGroup" `
    -PublicIpAddressName "myPublicIpAddress" `
    -OpenPorts 80,3389

$ip = Get-AzPublicIpAddress -ResourceGroupName $rgName | Select "IpAddress"

mstsc /v:($ip.IpAddress)

#Install-WindowsFeature -name Web-Server IncludeManagementTools
