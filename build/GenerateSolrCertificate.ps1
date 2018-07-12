. $PSScriptRoot\..\settings.ps1

$SolrKeyFile = "$SolrRoot\server\etc\solr-ssl.keystore.jks"
if ((Test-Path($SolrKeyFile))) {
	$SolrUri = [System.Uri]$SolrUrl
	
	. $PSScriptRoot\Certificates\solr-ssl.ps1 -KeystoreFile "$SolrKeyFile" -SolrDomain $SolrUri.Host -Clobber
}
