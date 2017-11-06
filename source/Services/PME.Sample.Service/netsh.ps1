
    Set-StrictMode -Version latest

    # Always run the script with the current script location as the base path
    Set-Location (Split-Path $MyInvocation.MyCommand.Path)

    $port = 7000
    $guid = [guid]::NewGuid()
    
    $certThumbprint = gci -Path cert:\LocalMachine\My | 
        Where-Object { $_.Subject -like "CN=localhost" } |  
        Select -First 1 | % {
            $_.Thumbprint
        }

    if ( $certThumbprint -eq $null ) {
        throw "Unable to locate required certificate: localhost"
    }

    netsh http delete urlacl url="https://+:$Port/"
    netsh http delete sslcert ipport="0.0.0.0:$Port"
    
    # Map the HTTPS port to everyone
    netsh http add urlacl url="https://+:$Port/" user=Everyone listen=yes delegate=yes
        
    # Use the thumbprint to identify the cert with the communication port
    netsh http add sslcert ipport="0.0.0.0:$Port" certhash="$certThumbprint" appid="{$guid}"

    #
    netsh http show urlacl url="https://+:$Port/"
    netsh http show sslcert ipport=0.0.0.0:$Port
    
