function New-RootCertificate {
    $rootCert = New-SelfSignedCertificate -certstorelocation $XConnectCertStore -dnsname "Self-signed Certificate Authority" -FriendlyName "Self-signed Certificate Authority" -TextExtension @("2.5.29.37={text}1.3.6.1.5.5.7.3.2") -NotAfter (Get-Date).AddYears(10) -KeyUsage CertSign -KeyProtection None -Provider 'Microsoft Enhanced RSA and AES Cryptographic Provider'
    try {
        $tempFile = New-TemporaryFile
        try {
            $pwd = ConvertTo-SecureString -String "secret" -Force -AsPlainText
            Export-PfxCertificate -cert $rootCert -FilePath $tempFile.FullName -Password $pwd
            return Import-PfxCertificate -FilePath $tempFile.FullName -Password $pwd -CertStoreLocation $XConnectCertStore
        } finally {
            Remove-Item $tempFile.FullName
        }
    } finally {
        Remove-Item $rootCert.PSPath
    }
}

function New-ClientCertificate {
    $rootCert = New-RootCertificate
    return New-SelfSignedCertificate -Subject "CN=$XConnectSiteName" -TextExtension @("2.5.29.37={text}1.3.6.1.5.5.7.3.2","2.5.29.17={text}upn=$XConnectSiteName") -KeyUsage DigitalSignature -CertStoreLocation "Cert:\LocalMachine\My" -NotAfter (Get-Date).AddYears(10) -FriendlyName $XConnectSiteName -Signer $rootCert -KeyExportPolicy Exportable -KeyProtection None -Provider 'Microsoft Enhanced RSA and AES Cryptographic Provider'
}

