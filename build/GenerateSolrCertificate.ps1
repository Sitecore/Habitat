. $PSScriptRoot\..\settings.ps1

#File is automatically created
$SolrKeyFile = "$SolrRoot\server\etc\solr-ssl.keystore.jks"

#Get Solr configurations 
$SolrUri = [System.Uri]$SolrUrl

#Show Solr configurations
echo $SolrUri

#Generate keystore
. $PSScriptRoot\Certificates\solr-ssl.ps1 -KeystoreFile "$SolrKeyFile" -SolrDomain $SolrUri.Host -Clobber
